using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using MonoGame.Extended.Input;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(Controller))]
    [With(typeof(PhysicBody))]
    public partial class MouseInputSystem : AEntitySetSystem<float>
    {
        private IParallelRunner runner;
        private World world;
        
        public MouseInputSystem(World _world, IParallelRunner _runner) : base(_world, CreateEntityContainer, null, 0){
            world = _world;
            runner = _runner;
            
        }

        [Update]
        private void Update(ref Controller _controller, in PhysicBody _physicBody, in Entity _entity){
            var state = MouseExtended.GetState();
            Vector2 mousePos = state.Position.ToVector2();
            var dir = mousePos - _physicBody.Position();
            dir.Normalize();
            if(state.LeftButton == ButtonState.Pressed){
                _controller.SetMovementDirection(dir);
            }
            else{
                _controller.SetMovementDirection(Vector2.Zero);
            }
            _controller.SetLookDirection(dir);
            
            if(state.WasButtonJustDown(MouseButton.Right)){
                _entity.Set<DoTheTrail>();
            }

            if(state.WasButtonJustUp(MouseButton.Right)){
                _entity.Remove<DoTheTrail>();
            }
        }
    }
}