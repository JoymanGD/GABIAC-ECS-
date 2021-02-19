using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components
{
    public struct Transform
    {
        public Vector2 Position {get; private set;}
        public Vector2 Scale {get; private set;}
        public float Rotation {get; private set;}
        public float DeltaPosition {get; private set;}
        public float DeltaRotation {get; private set;}
        private Vector2 lastPos;
        private float lastRot;

        public Transform(Vector2 _scale, float _rotation){
            Position = Vector2.Zero;
            Scale = _scale;
            Rotation = _rotation;
            lastPos = Position;
            lastRot = Rotation;
            DeltaPosition = 0;
            DeltaRotation = 0;
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

        public void SetDeltaRotation(){
            DeltaRotation = Rotation - lastRot;
            lastRot = Rotation;
        }
    }
}