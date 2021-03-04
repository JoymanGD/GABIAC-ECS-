using System;
using MonoGame.Extended.Input.InputListeners;

namespace Gabiac.Scripts.ECS.Components.Input
{
    public struct TouchEvent:InputEvent
    {
        public EventArgs EventArgs { get; set; }

        public TouchEvent(GamePadEventArgs _eventArgs){
            EventArgs = _eventArgs;
        }
    }
}