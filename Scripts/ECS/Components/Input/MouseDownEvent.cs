using System;
using MonoGame.Extended.Input.InputListeners;

namespace Gabiac.Scripts.ECS.Components.Input
{
    public struct MouseDownEvent:InputEvent
    {
        public EventArgs EventArgs { get; set; }

        public MouseDownEvent(MouseEventArgs _eventArgs){
            EventArgs = _eventArgs;
        }
    }
}