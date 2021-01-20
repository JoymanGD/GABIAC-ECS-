using System.Diagnostics;
using Microsoft.Xna.Framework;
using System;

namespace Toil.Scripts{
    public class Transform
    {
        public Vector2 position {get; private set;}
        public Vector2 scale {get; private set;}
        public float rotation {get; private set;}
        public float speed {get; private set;}
        public Vector2 Axis {get; private set;}
        private Vector2 direction;
        private Vector2 lastPos;
        private Vector2 lastDir;

        public Transform(Vector2 _position, Vector2 _scale, float _rotation, float _speed = 1){
            position = _position;
            scale = _scale;
            rotation = _rotation;
            speed = _speed;

            lastPos = position;
            lastDir = Vector2.Zero;
        }

        public void Update(GameTime gameTime){
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //position = Vector2.Lerp(position, position+Axis, .5f);
            LookForward();
            Debug.WriteLine("Axis: " + Axis);
            Debug.WriteLine("Rotation: " + rotation);
        }

        public void Translate(float x, float y){
            position += new Vector2(x,y);
        }

        public void SetScale(Vector2 Scale){
            scale += Scale;
        }

        public void SetAxis(Vector2 newAxis){
            Axis = newAxis;
        }

        public void SetScale(float x, float y){
            scale += new Vector2(x,y);
        }

        public void Rotate(float angle){
            rotation = angle;
        }

        public void SetDirection(Vector2 dir){
            direction = dir;
        }

        public Vector2 GetDirection(){
            var dir = position - lastPos;
            lastPos = position;
            if(dir != Vector2.Zero)
                lastDir = dir;
            return dir == Vector2.Zero ? lastDir : dir;
        }

        public void LookForward(){
            //var dir = GetDirection();
            var rot = (float)Math.Atan2(Axis.Y, Axis.X);
            if(Axis != Vector2.Zero)
                Rotate(CurveAngle(rotation, rot, 0.1f));
        }

        //MOVE IT TO LIB
        private float CurveAngle(float from, float to, float step)
        {
            if (step == 0) return from;
            if (from == to || step == 1) return to;

            Vector2 fromVector = new Vector2((float)Math.Cos(from), (float)Math.Sin(from));
            Vector2 toVector = new Vector2((float)Math.Cos(to), (float)Math.Sin(to));

            Vector2 currentVector = Slerp(fromVector, toVector, step);

            return (float)Math.Atan2(currentVector.Y, currentVector.X);
        }

        private Vector2 Slerp(Vector2 from, Vector2 to, float step)
        {
            if (step == 0) return from;
            if (from == to || step == 1) return to;

            double theta = Math.Acos(Vector2.Dot(from, to));
            if (theta == 0) return to;

            double sinTheta = Math.Sin(theta);
            return (float)(Math.Sin((1 - step) * theta) / sinTheta) * from + (float)(Math.Sin(step * theta) / sinTheta) * to;
        }
    }    

}
