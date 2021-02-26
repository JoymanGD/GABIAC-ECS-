using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components
{
    public struct Ball
    {
        public Vector2 Direction { get; private set; }
        public bool IsMoving { get; private set; }
        public Ball(PhysicBody _physicBody){
            Direction = Vector2.Zero;
            IsMoving = false;
            //Vector2 norm, 
            //_physicBody.Body.OnCollision += (fixtureA, fixtureB, contact)=>{SetDirection(contact.)}
            _physicBody.Body.LinearDamping = 0;
        }

        public void SetDirection(Vector2 _direction){
            Direction = _direction;
        }
    }
}