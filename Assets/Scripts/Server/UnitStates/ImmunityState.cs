using UnityEngine;

namespace ChargeGame
{
    [System.Serializable]
    public class ImmunityState : StateBase
    {
        public ImmunityState(ServerUnit unit) : base(unit)
        {
            _unit = unit;
        }


        public override void Enter()
        {
            _unit.SetColor(Color.blue);
            _unit.SetConstrains(RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ);
        }

        public override void Update(float deltaTime)
        {
            if (_unit.IsOnGround && _unit.Stunned == false)
            {
                Vector3 newVelocity = _unit.Rotation * _unit.Input.MovementInput.normalized * _unit.Settings.ImmunityMovementSpeed;
                newVelocity.y = _unit.Velocity.y;
                _unit.Velocity = newVelocity;
            }


            _unit.AngularVelocity = Vector3.up * _unit.Input.RotationInput.x * _unit.Settings.RotationSpeed;

            if (_unit.StateTime > _unit.Settings.ImmunityTime)
            {
                _unit.SetState(new DefaultState(_unit));
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
            return false;
        }
    }
}
