using Microsoft.Xna.Framework;
using System;

namespace Toil.Scripts{
    public class Transform
    {
        public Vector2 position {get; private set;}
        public Vector2 velocity {get; private set;}
        public Vector2 acceleration {get; private set;}
        public Vector2 scale {get; private set;}
        public float rotation {get; private set;}
        public float speed {get; private set;}

        private bool isAccelerating;
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
            velocity += acceleration * deltaTime;
            position += velocity * deltaTime;
        }

        public void Accelerate(Vector2 translation){
            if(!isAccelerating)
                isAccelerating = true;

            acceleration += translation;
        }

        private void Deaccelerate(float deltaTime){
            if(!isAccelerating){
                if(acceleration.X > 0){
                    acceleration -= new Vector2(deltaTime, 0);
                }
                else
                    acceleration = new Vector2(0, acceleration.Y);

                if(acceleration.Y > 0){
                    acceleration -= new Vector2(0, deltaTime);
                }
                else
                    acceleration = new Vector2(acceleration.X, 0);
            }
        }

        public void Translate(float x, float y){
            position += new Vector2(x,y);
        }

        public void SetScale(Vector2 Scale){
            scale += Scale;
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
            var dir = GetDirection();
            var rot = (float)Math.Atan2(dir.Y, dir.X);
            Rotate(CurveAngle(rotation, rot, 0.06f));
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
