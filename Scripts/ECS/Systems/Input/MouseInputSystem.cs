using Microsoft.Xna.Framework;
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
            var isMoving = mouseState.IsButtonDown(MouseBinding.Move);
            if(mouseState.WasButtonJustDown(MouseBinding.Move)){
                _entity.Set(new TranslationComponent(7, TranslationComponent.TranslationType.ApplyForce));
            }
            else if(mouseState.WasButtonJustUp(MouseBinding.Move)){
                _entity.Remove<TranslationComponent>();
            }

            if(mouseState.WasButtonJustDown(MouseBinding.DoTheTrail)){
                _entity.Set<DoTheTrail>();
            }
            else if(mouseState.WasButtonJustUp(MouseBinding.DoTheTrail)){
                _entity.Remove<DoTheTrail>();
            }

            if(_entity.Has<RotationComponent>() && _entity.Has<Transform>() && _entity.Has<TranslationComponent>() && isMoving){
                ref RotationComponent rotationComponent = ref _entity.Get<RotationComponent>();
                ref Transform transform = ref _entity.Get<Transform>();
                var direction = mouseState.Position.ToVector2() - transform.Position;
                direction.Normalize();
                rotationComponent.Direction = Vector2.Lerp(rotationComponent.Direction, direction, .02f);
            }
        }
    }
}