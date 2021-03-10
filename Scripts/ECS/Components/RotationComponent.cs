using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components
{
    public struct RotationComponent
    {
        public Vector2 Direction { get; set; }

        public RotationComponent(Vector2 _direction){
            Direction = _direction;
        }
    }
}