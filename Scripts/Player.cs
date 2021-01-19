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
            sprite.Rotate((float)Math.Atan2(dir.Y, dir.X));
        }
    }
}