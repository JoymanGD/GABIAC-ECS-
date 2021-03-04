using System;
using MonoGame.Extended.Input.InputListeners;

namespace Gabiac.Scripts.ECS.Components.Input
{
    public struct GamepadUpEvent:InputEvent
    {
        public EventArgs EventArgs { get; set; }

        public GamepadUpEvent(GamePadEventArgs _eventArgs){
            EventArgs = _eventArgs;
        }
    }
}