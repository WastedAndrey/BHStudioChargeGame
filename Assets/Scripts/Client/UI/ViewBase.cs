
using System;
using UnityEngine;

namespace ChargeGame
{
    public abstract class ViewBase : MonoBehaviour, UIElement
    {
        public Action<UIElement> Closed { get; set; }

        public void CloseElement()
        {
            CloseElementInternal();
            Closed?.Invoke(this);
            Destroy(this.gameObject);
        }

        protected virtual void CloseElementInternal() { }

        public void SetParent(Transform transform, bool worldPositionStays)
        {
            this.transform.SetParent(transform, false);
        }
    }
}