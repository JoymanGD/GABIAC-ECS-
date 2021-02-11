using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using ECS.Components;
using VelcroPhysics.Utilities;

namespace ECS.Systems
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
            _physicBody.Body.ApplyForce(_controller.Direction * _controller.Speed);
        }
    }
}