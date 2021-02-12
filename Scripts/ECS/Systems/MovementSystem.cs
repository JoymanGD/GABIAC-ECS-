using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using System;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(PhysicBody))]
    [With(typeof(Controller))]
    public partial class MovementSystem : AEntitySetSystem<float>
    {
        private IParallelRunner runner;
        private World world;
        
        public MovementSystem(World _world, IParallelRunner _runner) : base(_world, CreateEntityContainer, null, 0){
            world = _world;
            runner = _runner;
        }

        [Update]
        private void Update(ref PhysicBody _physicBody, in Controller _controller){
            _physicBody.Body.ApplyForce(_controller.MovementDirection * _controller.Speed);
            var lookDir = _controller.LookDirection;
            _physicBody.SetRotation((float)Math.Atan2(lookDir.Y, lookDir.X));
        }
    }
}