using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Input.InputListeners;

namespace Gabiac.Scripts.ECS.Components
{
    public struct InputHandler
    {
        public MouseListener MouseListener { get; set; }
        public KeyboardListener KeyboardListener { get; set; }
        public TouchListener TouchListener { get; set; }
        public GamePadListener GamePadListener { get; set; }
    }
}