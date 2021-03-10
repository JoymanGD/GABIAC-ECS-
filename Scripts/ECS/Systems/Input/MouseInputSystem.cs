using DefaultEcs;
using MonoGame.Extended.Input;
using Gabiac.Scripts.Helpers.Bindings;
using Gabiac.Scripts.ECS.Components;

namespace Gabiac.Scripts.ECS.Systems.Input
{
    public class MouseInputSystem:IInputSystem
    {
        public void Update(Entity _entity){
            var mouseState = MouseExtended.GetState();
            if(mouseState.WasButtonJustDown(MouseBinding.Move)){
                _entity.Set(new MovementComponent(4));
            }
            else if(mouseState.WasButtonJustUp(MouseBinding.Move)){
                _entity.Remove<MovementComponent>();
            }

            if(mouseState.WasButtonJustDown(MouseBinding.DoTheTrail)){
                _entity.Set<DoTheTrail>();
            }
            else if(mouseState.WasButtonJustUp(MouseBinding.DoTheTrail)){
                _entity.Remove<DoTheTrail>();
            }

            var rotationComponent = _entity.Get<RotationComponent>();
            var transformComponent = _entity.Get<Transform>();
            var direction = mouseState.Position.ToVector2() - transformComponent.Position;
            direction.Normalize();
            rotationComponent.Direction = direction;
        }
    }
}