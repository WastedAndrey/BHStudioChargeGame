using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    GameDefault,
    AbilityCast,
    Invulnarable
}

namespace ChargeGame
{
    public class ServerUnit : MonoBehaviour
    {
        [Header("Global links")]
        [SerializeField]
        private GlobalSettings _settings;
        [Header("Component links")]
        [SerializeField]
        private Transform _transform;
        [SerializeField]
        private Rigidbody _rigidBody;
        [SerializeField]
        private Collider _damageTrigger;
        [SerializeField]
        private List<Renderer> _renderers;
        [SerializeField]
        private Transform _cameraPoint;
        [SerializeField]
        private IPlayerInput _input;

        [Header("Debug")]
        [SerializeField]
        private string _playerName;
        [SerializeField]
        private int _playerID;
        [SerializeField]
        private string _stateName; // for debug purposes only
        [SerializeField]
        private float _stateTime = 0;
        [SerializeField]
        private float _abilityCooldown = 0;
        [SerializeField]
        private bool _isOnGround = false;
        [SerializeField]
        private float _stunTime = 0;
        [SerializeField]
        private Color32 _color = new Color32(255,255,255,255);
        [SerializeField]
        private short _score = 0;
        [SerializeField]
        private short _fails = 0;

        // Private fields
        private StateBase _state;

        // Actions
        public Action InitDone;
        public Action UpdateDone;
        public Action FixedUpdateDone;

        public Action<ServerUnit, Color32> ColorChanged;
        public Action<ServerUnit, short> ScoreChanged;
        public Action<ServerUnit, short> FailsChanged;
        public Action<ServerUnit> Destroyed;

        public string PlayerName { get => _playerName; }
        public int PlayerId { get => _playerID; }
        public float StateTime { get => _stateTime; }
        public float AbilityCooldown { get => _abilityCooldown; set => _abilityCooldown = value; }
        public Vector3 Velocity { get => _rigidBody.velocity; set { _rigidBody.velocity = value; } }
        public Vector3 AngularVelocity { get => _rigidBody.angularVelocity; set { _rigidBody.angularVelocity = value; } }
        public Vector3 Position { get => _transform.position; }
        public Quaternion Rotation { get => _transform.rotation; }
        public bool IsOnGround { get => _isOnGround; }
        public float StunTime { get => _stunTime; }
        public Color32 Color { get => _color; }
        public bool Stunned { get => _stunTime > 0; }
        public GlobalSettings Settings { get => _settings; }
        public IPlayerInput Input { get => _input; }
        public short Score { get => _score; }
        public short Fails { get => _fails; }
        public Transform CameraPoint { get => _cameraPoint; }



        public void Init(string name, int id, IPlayerInput playerInput)
        {
            _playerName = name;
            _playerID = id;
            _input = playerInput;

            _state = new DefaultState(this);
            _state.Enter();

#if UNITY_EDITOR
            _stateName = _state.GetType().ToString();
#endif
        }

        private void Update()
        {
            _stateTime += Time.deltaTime;
            if (_abilityCooldown > 0)
                _abilityCooldown -= Time.deltaTime;
            if (_stunTime > 0)
                _stunTime -= Time.deltaTime;

            _state.Update(Time.deltaTime);

            UpdateDone?.Invoke();
        }

        private void FixedUpdate()
        {
            _isOnGround = false;

            FixedUpdateDone?.Invoke();
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }

        public void SetState(StateBase state)
        {
            _state.Exit();
            _state = state;
            _state.Enter();
            _stateTime = 0;

#if UNITY_EDITOR
            _stateName = state.GetType().ToString();
#endif
        }

        public void SetColor(Color32 color)
        {
            _color = color;
            foreach (var renderer in _renderers)
            {
                renderer.material.SetColor("_Color", color);
            }
            ColorChanged?.Invoke(this, color);
        }

        public void SetConstrains(RigidbodyConstraints constraints)
        {
            _rigidBody.constraints = constraints;
        }

        public bool TryGetDamage(ServerUnit damageSource)
        {
            return _state.TryGetDamage(damageSource);
        }

        public void GetStun(float time)
        {
            _stunTime = _stunTime >= time ? _stunTime : time;
        }

        public void AddScore(short score)
        {
            _score += score;
            ScoreChanged?.Invoke(this, _score);
        }
        public void AddFails(short fails)
        {
            _fails += fails;
            FailsChanged?.Invoke(this, _fails);
        }

        public void ResetScoreAndFails()
        {
            _score = 0;
            _fails = 0;
            ScoreChanged?.Invoke(this, _score);
            ScoreChanged?.Invoke(this, _fails);
        }

        private void OnCollisionEnter(Collision collision)
        {
            TagComponent collisionTagComponent = collision.collider.attachedRigidbody.GetComponent<TagComponent>();
            _state.OnCollisionEnter(collision, collisionTagComponent);
        }

        private void OnCollisionStay(Collision collision)
        {
            TagComponent collisionTagComponent = collision.collider.attachedRigidbody.GetComponent<TagComponent>();
            if (collisionTagComponent.Tag == TagComponent.ObjectTag.Ground)
            {
                _isOnGround = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            TagComponent collisionTagComponent = other.attachedRigidbody.GetComponent<TagComponent>();
            _state.OnTriggerEnter(other, collisionTagComponent);
        }

        /// <summary>
        /// Is needed for reseting OnCollisionEnter & OnTriggerEnter on charge use, so it can work when objects are already collided before charge use
        /// </summary>
        public void ResetCollidersAndTriggers()
        {
            bool tempBool;
            tempBool = _damageTrigger.enabled;
            _damageTrigger.enabled = false;
            _damageTrigger.enabled = tempBool;
        }
    }
}
