using System;
using MonoGame.Extended.Input.InputListeners;

namespace Gabiac.Scripts.ECS.Components.Input
{
    public struct MouseUpEvent:InputEvent
    {
        public EventArgs EventArgs { get; set; }

        public MouseUpEvent(MouseEventArgs _eventArgs){
            EventArgs = _eventArgs;
        }
    }
}