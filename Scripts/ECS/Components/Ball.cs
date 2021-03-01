using System;
using Microsoft.Xna.Framework.Graphics;
using VelcroPhysics.Collision.ContactSystem;
using VelcroPhysics.Shared.Optimization;
using VelcroPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components
{
    public struct Ball
    {
        public Vector2 Direction { get; private set; }
        public float Speed { get; private set; }
        
        
        public Ball(Vector2 _startDirection, float _speed){
            Direction = _startDirection;
            Speed = _speed;
            SetDirection(GetRandomVector());
        }

        public void SetDirection(Vector2 _direction){
            Direction = _direction;
        }

        private void Reflect(Fixture _fixtureA, Fixture _fixtureB, Contact _contact){
            Vector2 norm;
            FixedArray2<Vector2> points;
            _contact.GetWorldManifold(out norm, out points);
            SetDirection(Vector2.Reflect(Direction, norm));
        }

        public void GetDirection(out Vector2 _direction){
            _direction = Direction;
        }

        private Vector2 GetRandomVector(){
            Random random = new Random();
            float x=-1,y=-1;
            return new Vector2(x,y);
        }
    }
}