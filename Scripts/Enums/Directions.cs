using Microsoft.Xna.Framework;
using System.Linq;

namespace Gabiac.Scripts.Enums
{
    public static class Directions
    {
        public static Vector2 Get(Direction _direction){
            switch (_direction)
            {
                case Direction.Left:
                    return -Vector2.UnitX;
                case Direction.Right:
                    return Vector2.UnitX;
                case Direction.Up:
                    return -Vector2.UnitY;
                case Direction.Down:
                    return Vector2.UnitY;
            }
            return Vector2.Zero;
        }
    }

    public enum Direction{
        Left, Right, Up, Down
    }
}