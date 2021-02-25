using DefaultEcs;
using Gabiac.Scripts.ECS.Components;
using MonoGame.Extended.Input.InputListeners;

namespace Gabiac.Scripts.ECS.Systems.Input
{
    public interface IInputSystem
    {
        public void SetDirection(PhysicBody _physicBody, ref Controller _controller);
        public void ControlInput(Entity _entity);
    }
}