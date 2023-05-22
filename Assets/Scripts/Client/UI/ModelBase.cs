
namespace ChargeGame
{
    public abstract class ModelBase
    {
        public void Enable()
        {
            EnableInternal();
            Subscribe();
        }
        protected virtual void EnableInternal() { }

        public void Disable()
        {
            DisableInternal();
            Unsubscribe();
        }
        protected virtual void DisableInternal() { }

        protected abstract void Subscribe();
        protected abstract void Unsubscribe();
    }
}