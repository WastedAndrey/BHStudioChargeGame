using UnityEngine;

namespace ChargeGame
{
    [System.Serializable]
    public abstract class StateBase
    {
        protected ServerUnit _unit;

        public StateBase(ServerUnit unit)
        {
            this._unit = unit;
        }

        public abstract void Enter();
        public abstract void Update(float deltaTime);
        public abstract void Exit();
        public abstract void OnCollisionEnter(Collision collision, TagComponent collisionTagComponent);
        public abstract void OnTriggerEnter(Collider collider, TagComponent collisionTagComponent);
        public abstract bool TryGetDamage(ServerUnit damageSource);
    }
}
