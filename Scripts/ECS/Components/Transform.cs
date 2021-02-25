using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components
{
    public struct Transform
    {
        public Vector2 Position {get; private set;}
        public Vector2 Scale {get; private set;}
        public float Rotation {get; private set;}
        public float DeltaPosition {get; private set;}
        private Vector2 lastPos;

        public Transform(float _rotation){
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = _rotation;
            lastPos = Position;
            DeltaPosition = 0;
        }

        public void SetPosition(Vector2 _position){
            Position = _position;
        }

        public void SetScale(Vector2 _scale){
            Scale = _scale;
        }

        public void SetRotation(float _rotation){
            Rotation = _rotation;
        }
        public void SetDeltaPosition(float _deltaPosition = 0){
            DeltaPosition = (Position - lastPos).Length();
            lastPos = Position;
        }
    }
}