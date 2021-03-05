using System;
using MonoGame.Extended.Input.InputListeners;

namespace Gabiac.Scripts.ECS.Components.Input
{
    public struct MouseMoveEvent:InputEvent
    {
        public EventArgs EventArgs { get; set; }

        public MouseMoveEvent(MouseEventArgs _eventArgs){
            EventArgs = _eventArgs;
        }
    }
}