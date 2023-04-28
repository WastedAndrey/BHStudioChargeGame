using UnityEngine;

namespace ChargeGame
{
    [System.Serializable]
    public class ChargeState : StateBase
    {
        public ChargeState(ServerUnit unit) : base(unit)
        {
            _unit = unit;
        }


        public override void Enter()
        {
            _unit.ResetCollidersAndTriggers();

            Vector3 newVelocity = _unit.Rotation * Vector3.forward * _unit.Settings.ChargeMovementSpeed;
            newVelocity.y = _unit.Velocity.y;
            _unit.Velocity = newVelocity;

            _unit.AngularVelocity = Vector3.zero;
            _unit.SetColor(Color.red);
            _unit.SetConstrains(RigidbodyConstraints.FreezeRotation);
        }

        public override void Update(float deltaTime)
        {
            if (_unit.StateTime > _unit.Settings.ChargeTime)
            {
                _unit.SetState(new DefaultState(_unit));
            }
        }
        public override void Exit()
        { }

        public override void OnCollisionEnter(Collision collision, TagComponent collisionTagComponent)
        {
            if (collisionTagComponent.Tag == TagComponent.ObjectTag.Wall)
            {
                Vector3 newVelocity = Vector3.zero;
                newVelocity.y = _unit.Velocity.y;
                _unit.Velocity = newVelocity;
            }
        }

        public override void OnTriggerEnter(Collider collider, TagComponent collisionTagComponent)
        {
            if (collisionTagComponent.Tag == TagComponent.ObjectTag.Player)
            {
                bool damageDone = collisionTagComponent.GetComponent<ServerUnit>().TryGetDamage(_unit);
                if (damageDone)
                    _unit.AddScore(1);
            }
        }

        public override bool TryGetDamage(ServerUnit damageSource)
        {
            return false;
        }
    }
}