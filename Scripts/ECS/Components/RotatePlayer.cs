using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components
{
    public struct RotatePlayer
    {
        public Vector2 Direction { get; set; }

        public RotatePlayer(Vector2 _direction){
            Direction = _direction;
        }
    }
}