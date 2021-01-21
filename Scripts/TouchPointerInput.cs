using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;

namespace Toil.Scripts
{
    public class TouchPointerInput:Input
    {
        private TouchCollection state;
        private Vector2 lastPos;
        public TouchPointerInput(Transform _transform):base(_transform){

        }

        public override void Move()
        {
            if(isActive()){ //change condition to 'if(touch detected in left part of touchscreen)'
                var dir = state[0].Position - transform.position;
                dir.Normalize();
                transform.SetAxis(dir);
            }
            else
                Stop();
        }

        public override void SetState()
        {
            state = TouchPanel.GetState();
        }

        public override void Additive()
        {
            lastPos = state[0].Position;
        }

        public override bool isActive()
        {
            return state.Count > 0;
        }
    }
}