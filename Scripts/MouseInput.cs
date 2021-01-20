using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Toil.Scripts
{
    public class MouseInput:Input
    {
        public MouseInput(Transform _transform):base(_transform){

        }

        public override void Move()
        {
            var state = Mouse.GetState();
            if(state.LeftButton == ButtonState.Pressed){
                var dir = state.Position.ToVector2() - transform.position;
                dir.Normalize();
                transform.SetAxis(dir);
            }
            else
                transform.SetAxis(Vector2.Zero);
        }
    }
}