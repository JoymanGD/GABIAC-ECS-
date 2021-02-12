using Microsoft.Xna.Framework;

namespace ECS.Components
{
    public struct Transform
    {
        public Vector2 Position {get; private set;}
        public Vector2 Scale {get; private set;}
        public float Rotation {get; private set;}
        private Vector2 lastPos;

        public Transform(Vector2 _position, Vector2 _scale, float _rotation){
            Position = _position;
            Scale = _scale;
            Rotation = _rotation;
            lastPos = Position;
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
        public float GetDeltaPosition(){
            var pos = Position - lastPos;
            lastPos = Position;
            return pos.Length();
        }
    }
}