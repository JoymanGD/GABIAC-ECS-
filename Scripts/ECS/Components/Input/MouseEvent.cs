using MonoGame.Extended.Input;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components.Input
{
    public struct MouseEvent:InputEvent
    {
        public MouseButton Button { get; set; }

        public MouseEvent(MouseButton _button){
            Button = _button;
        }
    }
}