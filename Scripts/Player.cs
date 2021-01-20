using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;

namespace Toil.Scripts
{
    public class Player
    {
        public Sprite sprite {get;private set;}
        public Input input {get;private set;}
        public Transform transform {get;private set;}
        public string name  {get;private set;}
        public string tag {get;private set;}

        public Player(Sprite _sprite, Input _input, string _name, string _tag="Default")
        {
            sprite = _sprite;
            input = _input;
            name = _name;
            tag = _tag;
            transform = sprite.transform;
        }

        public void Update(GameTime gameTime){
            KeyboardState state = Keyboard.GetState();
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            transform.SetAxis(new Vector2(0, 0));
            if(state.IsKeyDown((Keys)input.Down)){
                transform.SetAxis(new Vector2(transform.Axis.X, 1));
            }
            if(state.IsKeyDown((Keys)input.Up)){
                transform.SetAxis(new Vector2(transform.Axis.X, -1));
            }
            if(state.IsKeyDown((Keys)input.Right)){
                transform.SetAxis(new Vector2(1, transform.Axis.Y));
            }
            if(state.IsKeyDown((Keys)input.Left)){
                transform.SetAxis(new Vector2(-1, transform.Axis.Y));
            }

            transform.Update(gameTime);
        }
    }
}