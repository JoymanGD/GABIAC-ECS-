using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components
{
    public struct BallReflection
    {
        public Vector2 Normal { get; private set; }

        public BallReflection(Vector2 _normal){
            Normal = _normal;
        }
    }
}