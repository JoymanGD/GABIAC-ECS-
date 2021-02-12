using System.Reflection;
using System.Net.Mime;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components
{
    public struct Controller
    {
        public Vector2 MovementDirection {get; private set;}
        public Vector2 LookDirection {get; private set;}
        public float Speed {get; private set;}
        public bool isMoving {get; private set;}

        public Controller(Vector2 _direction, float _speed, bool _isMoving){
            MovementDirection = _direction;
            LookDirection = MovementDirection;
            Speed = _speed;
            isMoving = _isMoving;
        }

        public void SetMovementDirection(Vector2 _direction){
            MovementDirection = _direction;
        }
        
        public void SetLookDirection(Vector2 _direction){
            LookDirection = _direction;
        }

        public void SetSpeed(float _speed){
            Speed = _speed;
        }

        public void SetMoving(bool _isMoving){
            isMoving = _isMoving;
        }
    }
}