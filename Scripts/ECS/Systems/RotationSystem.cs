using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using Microsoft.Xna.Framework;
using System;

namespace Gabiac.Scripts.ECS.Systems
{
    [Without(typeof(PhysicBody))]
    [With(typeof(RotationComponent))]
    [With(typeof(Transform))]
    public partial class RotationSystem : AEntitySetSystem<GameTime>
    {
        private IParallelRunner runner;
        private World world;
        
        public RotationSystem(World _world, IParallelRunner _runner) : base(_world, CreateEntityContainer, null, 0){
            world = _world;
            runner = _runner;
        }

        [Update]
        private void Update(ref Transform _transform, in RotationComponent _rotationComponent){
            _transform.Rotation = (float)Math.Atan2(_rotationComponent.Direction.Y, _rotationComponent.Direction.X);
        }
    }
}