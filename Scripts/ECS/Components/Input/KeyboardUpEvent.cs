using System;
using MonoGame.Extended.Input.InputListeners;

namespace Gabiac.Scripts.ECS.Components.Input
{
    public struct KeyboardUpEvent:InputEvent
    {
        public EventArgs EventArgs { get; set; }

        public KeyboardUpEvent(KeyboardEventArgs _eventArgs){
            EventArgs = _eventArgs;
        }
    }
}