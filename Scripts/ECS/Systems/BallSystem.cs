using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using System;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(Ball))]
    [With(typeof(PhysicBody))]
    public partial class BallSystem : AEntitySetSystem<float>
    {
        private IParallelRunner runner;
        private World world;
        private VelcroPhysics.Dynamics.World physicWorld;
        
        public BallSystem(World _world, IParallelRunner _runner, VelcroPhysics.Dynamics.World _physicWorld) : base(_world, CreateEntityContainer, null, 0){
            world = _world;
            runner = _runner;
            physicWorld = _physicWorld;
        }

        [Update]
        private void Update(in PhysicBody _physicBody){
            //_physicBody.Body.on
        }
    }
}