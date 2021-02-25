using Microsoft.Xna.Framework;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using MonoGame.Extended.Input;

namespace Gabiac.Scripts.ECS.Systems.Input
{
    [With(typeof(Controller))]
    [With(typeof(PhysicBody))]
    [With(typeof(MouseInput))]
    public partial class MouseInputSystem : AEntitySetSystem<float>, IInputSystem
    {
        IParallelRunner runner;
        World world;

        public MouseInputSystem(World _world, IParallelRunner _runner) : base(_world, _runner){
            world = _world;
            runner = _runner;
        }

        [Update]
        protected void Update(ref Controller _controller, in PhysicBody _physicBody, in Entity _entity){
            ControlInput(_entity);
            SetDirection(_physicBody, ref _controller);
        }

        public void SetDirection(PhysicBody _physicBody, ref Controller _controller)
        {
            var state = MouseExtended.GetState();
            Vector2 mousePos = state.Position.ToVector2();
            var dir = mousePos - _physicBody.Position();
            dir.Normalize();
            _controller.SetDirection(dir);
        }

        public void ControlInput(Entity _entity){
            var mouseState = MouseExtended.GetState();
            if(mouseState.WasButtonJustDown(MouseButton.Left)){
                _entity.Set<MovePlayer>();
            }
            else if(mouseState.WasButtonJustUp(MouseButton.Left)){
                _entity.Remove<MovePlayer>();
            }

            if(mouseState.WasButtonJustDown(MouseButton.Right)){
                _entity.Set<DoTheTrail>();
            }
            else if(mouseState.WasButtonJustUp(MouseButton.Right)){
                _entity.Remove<DoTheTrail>();
            }
        }
    }
}