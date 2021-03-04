using System;
using MonoGame.Extended.Input.InputListeners;

namespace Gabiac.Scripts.ECS.Components.Input
{
    public struct KeyboardDownEvent:InputEvent
    {
        public EventArgs EventArgs { get; set; }

        public KeyboardDownEvent(KeyboardEventArgs _eventArgs){
            EventArgs = _eventArgs;
        }
    }
}