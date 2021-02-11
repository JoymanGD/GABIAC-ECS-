using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using ECS.Components;
using VelcroPhysics.Utilities;

namespace ECS.Systems
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
        private void Update(ref Controller _controller, in PhysicBody _physicBody){
            var state = Mouse.GetState();
            if(state.LeftButton == ButtonState.Pressed){
                Vector2 mousePos = state.Position.ToVector2();
                var dir = mousePos - _physicBody.Position();
                dir.Normalize();
                _controller.SetDirection(dir);
            }
            else{
                _controller.SetDirection(Vector2.Zero);
            }
        }
    }
}