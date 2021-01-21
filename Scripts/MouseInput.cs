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

        public override void Update(GameTime gameTime)
        {
            state = Mouse.GetState();
            if(state.Position != lastPos || state.LeftButton == ButtonState.Pressed || state.RightButton == ButtonState.Pressed || state.MiddleButton == ButtonState.Pressed){
                if(!isActive)
                    isActive = true;

                if(isStopped)
                    isStopped = false;
            }
            else{
                if(isActive)
                    isActive = false;
                
                if(!isStopped){
                    Stop();
                    isStopped = true;
                }
            }

            lastPos = state.Position;
            
            if(isActive){
                Move();
            }
        }

        public override void Move()
        {
            if(state.LeftButton == ButtonState.Pressed){
                var dir = state.Position.ToVector2() - transform.position;
                dir.Normalize();
                transform.SetAxis(dir);
            }
            else
                Stop();
        }
    }
}