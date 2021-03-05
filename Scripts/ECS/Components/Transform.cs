using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components
{
    public struct Transform
    {
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public float Rotation { get; set; }

        public Transform(Vector2 _position, Vector2 _scale, float _rotation){
            Position = _position;
            Scale = _scale;
            Rotation = _rotation;
        }
    }
}