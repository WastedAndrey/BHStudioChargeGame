using UnityEngine;

namespace ChargeGame
{
    [System.Serializable]
    public class DefaultState : StateBase
    {
        public DefaultState(ServerUnit unit) : base(unit)
        {
            _unit = unit;
        }

        public override void Enter()
        {
            _unit.SetColor(Color.green);
            _unit.SetConstrains(RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ);
        }

        public override void Update(float deltaTime)
        {
            if (_unit.IsOnGround && _unit.Stunned == false)
            {
                Vector3 newVelocity = _unit.Rotation * _unit.Input.MovementInput.normalized * _unit.Settings.MovementSpeed;
                newVelocity.y = _unit.Velocity.y;
                _unit.Velocity = newVelocity;
            }

            _unit.AngularVelocity = Vector3.up * _unit.Input.RotationInput.x * _unit.Settings.RotationSpeed;

            if (_unit.IsOnGround && _unit.Input.FireInput && _unit.AbilityCooldown <= 0)
            {
                _unit.AbilityCooldown = _unit.Settings.ChargeCooldown;
                _unit.SetState(new ChargeState(_unit));
            }
        }

        public override void Exit()
        { }

        public override void OnCollisionEnter(Collision collision, TagComponent collisionTagComponent)
        { }

        public override void OnTriggerEnter(Collider collider, TagComponent collisionTagComponent)
        { }

        public override bool TryGetDamage(ServerUnit damageSource)
        {
            Vector3 onDamageImpulse = damageSource.Rotation * _unit.Settings.OnDamageImpulse;
            _unit.Velocity = onDamageImpulse;
            _unit.GetStun(_unit.Settings.OnDamageStun);
            _unit.AddFails(1);
            _unit.SetState(new ImmunityState(_unit));
            return true;
        }
    }
}