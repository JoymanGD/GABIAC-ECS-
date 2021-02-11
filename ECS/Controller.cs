using System.Reflection;
using System.Net.Mime;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ECS.Components
{
    public struct Controller
    {
        public Vector2 Direction {get; private set;}
        public float Speed {get; private set;}
        public bool isMoving {get; private set;}

        public Controller(Vector2 _direction, float _speed, bool _isMoving){
            Direction = _direction;
            Speed = _speed;
            isMoving = _isMoving;
        }

        public void SetDirection(Vector2 _direction){
            Direction = _direction;
        }

        public void SetSpeed(float _speed){
            Speed = _speed;
        }

        public void SetMoving(bool _isMoving){
            isMoving = _isMoving;
        }
    }
}