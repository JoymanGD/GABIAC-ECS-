using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Toil.Scripts
{
    public class KeyboardInput:Input
    {
        private KeyboardState state;
        public KeyboardInput(Transform _transform):base(_transform){

        }

        public override void Update(GameTime gameTime)
        {
            state = Keyboard.GetState();
            if(state.GetPressedKeyCount() > 0){
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

            if(isActive)
                Move();
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
    }
}