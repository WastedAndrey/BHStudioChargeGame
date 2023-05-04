
using System;
using UnityEngine;

namespace ChargeGame
{
    public class UIElementBase : MonoBehaviour, UIElement
    {
        public Action<UIElement> Closed { get; set; }

        public void CloseElement()
        {
            CloseElementInternal();
            Closed?.Invoke(this);
            Destroy(this.gameObject);
        }

        protected virtual void CloseElementInternal() { }
    }
}