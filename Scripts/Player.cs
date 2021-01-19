using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;

namespace Toil.Scripts
{
    public class Player
    {
        public Sprite sprite {get;private set;}
        public Input input {get;private set;}
        public string name  {get;private set;}
        public string tag {get;private set;}

        public Player(Sprite _sprite, Input _input, string _name, string _tag="Default")
        {
            sprite = _sprite;
            input = _input;
            name = _name;
            tag = _tag;
        }

        public void Update(GameTime gameTime){
            KeyboardState state = Keyboard.GetState();
            if(state.IsKeyDown((Keys)input.Down)){
                sprite.Translate(new Vector2(0,1));
            }
            if(state.IsKeyDown((Keys)input.Up)){
                sprite.Translate(new Vector2(0,-1));
            }
            if(state.IsKeyDown((Keys)input.Right)){
                sprite.Translate(new Vector2(1,0));
            }
            if(state.IsKeyDown((Keys)input.Left)){
                sprite.Translate(new Vector2(-1,0));
            }

            RotateForward();
        }

        private void RotateForward(){
            var dir = sprite.GetDirection();
            var rot = (float)Math.Atan2(dir.Y, dir.X);
            sprite.Rotate(CurveAngle(sprite.rotation, rot, 0.06f));
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