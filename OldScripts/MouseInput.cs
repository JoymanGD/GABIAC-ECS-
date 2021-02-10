using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Toil.Scripts
{
    public class MouseInput:Input
    {
        private Point lastPos;
        private MouseState state;
        public MouseInput(Transform _transform):base(_transform){

        }

        public override void Move()
        {
            if(state.LeftButton == ButtonState.Pressed){
                var dir = state.Position.ToVector2() - transform.position;
                dir.Normalize();

                if(state.RightButton == ButtonState.Pressed)
                    transform.SetAxis(dir * 3f);
                else
                    transform.SetAxis(dir);
            }
            else
                Stop();
        }

        public override void SetState()
        {
            state = Mouse.GetState();
        }

        public override void Additive()
        {
            lastPos = state.Position;
        }

        public override bool isActive()
        {
            return state.Position != lastPos || state.LeftButton == ButtonState.Pressed || state.RightButton == ButtonState.Pressed || state.MiddleButton == ButtonState.Pressed;
        }
    }
}