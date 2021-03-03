using MonoGame.Extended.Input;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components.Input
{
    public struct GamepadEvent:InputEvent
    {
        public Buttons Button { get; set; }

        public GamepadEvent(Buttons _button){
            Button = _button;
        }
    }
}