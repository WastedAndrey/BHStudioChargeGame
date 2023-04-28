using UnityEngine;

namespace ChargeGame
{
    [CreateAssetMenu(fileName = "GlobalSettings", menuName = "ScriptableObjects/GlobalSettings")]
    public class GlobalSettings : ScriptableObject
    {
        [SerializeField]
        private float _movementSpeed = 3;
        [SerializeField]
        private float _rotationSpeed = 3;
        [SerializeField]
        private float _chargeCooldown = 3;
        [SerializeField]
        private float _chargeTime = 0.5f;
        [SerializeField]
        private float _chargeMovementSpeed = 15;
        [SerializeField]
        private float _immunityTime = 3;
        [SerializeField]
        private float _immunityMovementSpeed = 5;
        [SerializeField]
        private Vector3 _onDamageImpulse = new Vector3(0, 3, 3);
        [SerializeField]
        private float _onDamageStun = 0.5f;
        [SerializeField]
        private float _scoreForWin = 5;
        [SerializeField]
        private float _newRoundAwaitTime = 5;

        public float MovementSpeed { get => _movementSpeed; private set => _movementSpeed = value; }
        public float RotationSpeed { get => _rotationSpeed; private set => _rotationSpeed = value; }
        public float ChargeCooldown { get => _chargeCooldown; private set => _chargeCooldown = value; }
        public float ChargeTime { get => _chargeTime; private set => _chargeTime = value; }
        public float ChargeMovementSpeed { get => _chargeMovementSpeed; private set => _chargeMovementSpeed = value; }
        public float ImmunityTime { get => _immunityTime; private set => _immunityTime = value; }
        public float ImmunityMovementSpeed { get => _immunityMovementSpeed; private set => _immunityMovementSpeed = value; }
        public Vector3 OnDamageImpulse { get => _onDamageImpulse; private set => _onDamageImpulse = value; }
        public float OnDamageStun { get => _onDamageStun; private set => _onDamageStun = value; }
        public float ScoreForWin { get => _scoreForWin; private set => _scoreForWin = value; }
        public float NewRoundAwaitTime { get => _newRoundAwaitTime; private set => _newRoundAwaitTime = value; }
    }
}