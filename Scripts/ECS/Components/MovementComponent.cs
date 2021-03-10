using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components
{
    public struct MovementComponent
    {
        public float MovementSpeed { get; private set; }

        public MovementComponent(float _movementSpeed){
            MovementSpeed = _movementSpeed;
        }
    }
}