
using System;

namespace ChargeGame
{
    public interface UIElement
    {
        Action<UIElement> Closed { get; set; }
        void CloseElement();
    }
}