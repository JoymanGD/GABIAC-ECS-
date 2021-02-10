using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Toil.Scripts
{
    public class KeyboardInput:Input
    {
        private KeyboardState state;
        public KeyboardInput(Transform _transform):base(_transform){

        }

        public override void Move()
        {
            Stop();

            if(state.IsKeyDown(Keys.S)){
                transform.SetAxis(new Vector2(transform.Axis.X, 1));
            }
            if(state.IsKeyDown(Keys.W)){
                transform.SetAxis(new Vector2(transform.Axis.X, -1));
            }
            if(state.IsKeyDown(Keys.D)){
                transform.SetAxis(new Vector2(1, transform.Axis.Y));
            }
            if(state.IsKeyDown(Keys.A)){
                transform.SetAxis(new Vector2(-1, transform.Axis.Y));
            }
        }

        public override void SetState()
        {
            state = Keyboard.GetState();
        }

        public override void Additive()
        {
            
        }

        public override bool isActive()
        {
            return state.GetPressedKeyCount() > 0;
        }
    }
}