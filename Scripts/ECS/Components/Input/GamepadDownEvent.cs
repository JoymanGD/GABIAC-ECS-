using System;
using MonoGame.Extended.Input.InputListeners;

namespace Gabiac.Scripts.ECS.Components.Input
{
    public struct GamepadDownEvent:InputEvent
    {
        public EventArgs EventArgs { get; set; }

        public GamepadDownEvent(GamePadEventArgs _eventArgs){
            EventArgs = _eventArgs;
        }
    }
}