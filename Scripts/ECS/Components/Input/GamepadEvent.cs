using System;
using MonoGame.Extended.Input.InputListeners;

namespace Gabiac.Scripts.ECS.Components.Input
{
    public struct GamepadEvent:InputEvent
    {
        public EventArgs EventArgs { get; set; }

        public GamepadEvent(GamePadEventArgs _eventArgs){
            EventArgs = _eventArgs;
        }
    }
}