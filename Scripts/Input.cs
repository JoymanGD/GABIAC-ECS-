using Microsoft.Xna.Framework;
namespace Toil.Scripts
{
    abstract public class Input
    {
        public Transform transform{get;private set;}
        public bool isActive {get; protected set;}
        public Input(Transform _transform){
            transform=_transform;
        }
        public abstract void Move();
        public abstract void Update(GameTime gameTime);
        protected void Stop(){
            transform.SetAxis(Vector2.Zero);
        }

        protected bool isStopped=false;
    }
}