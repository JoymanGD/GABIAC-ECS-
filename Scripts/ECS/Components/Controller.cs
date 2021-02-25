using System.Reflection;
using System.Net.Mime;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components
{
    public struct Controller
    {
        public Vector2 Direction {get; private set;}
        public float Speed {get; private set;}

        public Controller(Vector2 _direction, float _speed, bool _isMoving){
            Direction = _direction;
            Speed = _speed;
        }

        public void SetDirection(Vector2 _direction){
            Direction = _direction;
        }

        public void SetSpeed(float _speed){
            Speed = _speed;
        }
    }
}