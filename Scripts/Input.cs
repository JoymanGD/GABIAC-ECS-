using Microsoft.Xna.Framework;
namespace Toil.Scripts
{
    abstract public class Input
    {
        public Transform transform{get;private set;}
        public bool isActiveFlag {get; protected set;}
        public Input(Transform _transform){
            transform=_transform;
        }

        public void Update(GameTime gameTime){
            SetState();
            Movement();
        }

        private void Movement(){
            if(isActive()){
                if(!isActiveFlag)
                    isActiveFlag = true;
            }
            else{
                if(isActiveFlag){
                    isActiveFlag = false;
                    Stop();
                }
            }

            Additive();
            
            if(isActiveFlag){
                Move();
            }
        }

        protected void Stop(){
            transform.SetAxis(Vector2.Zero);
        }

        public abstract void Move();

        public abstract void SetState();

        public abstract bool isActive();

        public abstract void Additive(); //leave empty if there are no additive actions
    }
}