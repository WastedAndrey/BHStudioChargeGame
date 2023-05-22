
using System;
using UnityEngine;

namespace ChargeGame
{
    public interface UIElement
    {
        Action<UIElement> Closed { get; set; }
        void CloseElement();
        void SetParent(Transform transform, bool worldPositionStays = false);
    }
}