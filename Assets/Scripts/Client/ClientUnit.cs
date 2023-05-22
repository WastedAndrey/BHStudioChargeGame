using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargeGame
{
    public class ClientUnit : NetworkBehaviour
    {
        [Header("Global links")]
        [SerializeField]
        private ClientContext _clientContext;
        [SerializeField]
        private ClientSettings _clientSettings;
        [SerializeField]
        private GlobalSettings _globalSettings;
        [Header("Component links")]
        [SerializeField]
        private List<Renderer> _renderers;
        [SerializeField]
        private Transform _cameraPoint;
      

        [Header("SyncVars")]
        [SyncVar(hook = nameof(SetName))]
        [SerializeField]
        private string _playerName;
        [SyncVar(hook = nameof(SetPlayerId))]
        [SerializeField]
        private int _playerId;
        [SyncVar(hook = nameof(SetColor))]
        [SerializeField]
        private Color32 _color;
        [SyncVar(hook = nameof(SetScore))]
        [SerializeField]
        private short _score = 0;
        [SyncVar(hook = nameof(SetFails))]
        [SerializeField]
        private short _fails = 0;

        private ServerUnit _unit;
        private Material[] _cachedMaterials;

        public Action<string> NameChanged;
        public Action<int> IdChanged;
        public Action<Color32> ColorChanged;
        public Action<short> ScoreChanged;
        public Action<short> FailsChanged;
        public Action<ClientUnit> Destroyed;

        public string PlayerName { get => _playerName;}
        public int PlayerId { get => _playerId; }
        public Color32 Color { get => _color; }
        public short Score { get => _score; }
        public short Fails { get => _fails; }
        public Transform CameraPoint { get => _cameraPoint; }


        public Color32 GetColor()
        {
            return _color;
        }

        public void SetUnit(ServerUnit unit)
        {
            _unit = unit;
            UpdateValues();

            if (this.enabled)
                SubscribeOnUnitActions();
        }

        private void UpdateValues()
        {
            SetName(_playerName, _unit.PlayerName);
            SetPlayerId(_playerId, _unit.PlayerId);
            SetColor(_color, _unit.Color);
            SetScore(_score, _unit.Score);
            SetFails(_fails, _unit.Fails);
        }

        private void CreateCachedMaterials()
        {
            _cachedMaterials = new Material[_renderers.Count];
            for (int i = 0; i < _renderers.Count; i++)
            {
                _cachedMaterials[i] = _renderers[i].material;
            }
        }
        private void DestroyCachedMaterials()
        {
            for (int i = 0; i < _cachedMaterials.Length; i++)
            {
                Destroy(_cachedMaterials[i]);
            }
        }
        private void Start()
        {
            if (isOwned)
                _clientContext.SetPlayerUnit(this);

            _clientContext.AddUnit(this);
        }

        private void OnEnable()
        {
            SubscribeOnUnitActions();
        }

        private void OnDisable()
        {
            UnsubscribeOnUnitActions();
        }

        private void OnDestroy()
        {
            DestroyCachedMaterials();
            Destroyed?.Invoke(this);
        }

        private void Update()
        {
            if (isOwned)
            {
                UpdateCameraPointVerticalRotation();
            }
        }

        private void UpdateCameraPointVerticalRotation()
        {
            float angle = Input.GetAxisRaw("Mouse Y") * _globalSettings.RotationSpeed;
            Vector3 rotation = _cameraPoint.transform.localRotation.eulerAngles;
            rotation.x -= angle;
            rotation.x = Mathf.Clamp(rotation.x, _clientSettings.CameraVerticalRotationDelta.x, _clientSettings.CameraVerticalRotationDelta.y);
            _cameraPoint.localRotation = Quaternion.Euler(rotation);
        }

        private void SubscribeOnUnitActions()
        {
            if (_unit != null)
            {
                _unit.UpdateDone += OnUnitUpdate;
                _unit.FixedUpdateDone += OnUnitFixUpdate;
            }
        }

        private void UnsubscribeOnUnitActions()
        {
            if (_unit != null)
            {
                _unit.UpdateDone -= OnUnitUpdate;
                _unit.FixedUpdateDone -= OnUnitFixUpdate;
            }
        }

        private void OnUnitUpdate()
        {
            _color = _unit.Color;
            _score = _unit.Score;
            _fails = _unit.Fails;
        }

        private void OnUnitFixUpdate()
        {
            this.transform.position = _unit.transform.position;
            this.transform.rotation = _unit.transform.rotation;
        }

        private void SetName(string oldName, string newName)
        {
            _playerName = newName;
            NameChanged?.Invoke(newName);
        }
        private void SetPlayerId(int oldId, int newId)
        {
            _playerId = newId;
            IdChanged?.Invoke(newId);
        }
        private void SetColor(Color32 oldColor, Color32 newColor)
        {
            if (_cachedMaterials==null)
            {
                CreateCachedMaterials();
            }

            for (int i = 0; i < _cachedMaterials.Length; i++)
            {
                _cachedMaterials[i].color = newColor;
            }
            ColorChanged?.Invoke(newColor);
        }
        private void SetScore(short oldScore, short newScore)
        {
            _score = newScore;
            ScoreChanged?.Invoke(newScore);
        }
        private void SetFails(short oldFails, short newFails)
        {
            _fails = newFails;
            FailsChanged?.Invoke(newFails);
        }
    }
}
