namespace Toil.Scripts
{
    abstract public class Input
    {
        public Transform transform{get;private set;}
        public Input(Transform _transform){
            transform=_transform;
        }
        public abstract void Move();
    }
}