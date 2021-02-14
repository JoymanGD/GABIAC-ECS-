using System.Reflection;
using System.Net.Mime;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components
{
    public struct TrailPoint
    {
        public Vector2 Position { get; private set; }
        public float Size { get; private set; }

        public TrailPoint(Vector2 _position, float _size){
            Position = _position;
            Size = _size;
        }
    }
}