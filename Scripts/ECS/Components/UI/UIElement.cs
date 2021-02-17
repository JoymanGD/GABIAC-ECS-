using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components.UI
{
    public class UIElement
    {
        public Vector2 Position;
        public Vector2 Scale;
        public float Rotation;

        public UIElement(Vector2 _position, Vector2 _scale, float _rotation){
            Position = _position;
            Scale = _scale;
            Rotation = _rotation;
        }
    }
}