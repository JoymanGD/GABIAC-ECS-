using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using Microsoft.Xna.Framework;
using System;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(PhysicBody))]
    [With(typeof(RotationComponent))]
    public partial class PhysicRotationSystem : AEntitySetSystem<GameTime>
    {
        private IParallelRunner runner;
        private World world;
        
        public PhysicRotationSystem(World _world, IParallelRunner _runner) : base(_world, CreateEntityContainer, null, 0){
            world = _world;
            runner = _runner;
        }

        [Update]
        private void Update(ref PhysicBody _physicBody, in RotationComponent _rotationComponent){
            //_transform.Rotation = (float)Math.Atan2(_rotationComponent.Direction.Y, _rotationComponent.Direction.X);
            _physicBody.SetRotation((float)Math.Atan2(_rotationComponent.Direction.Y, _rotationComponent.Direction.X));
        }
    }
}