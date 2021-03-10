using VelcroPhysics.Utilities;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using System;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(PhysicBody))]
    [With(typeof(MovementComponent))]
    public partial class TranslationByControllerSystem : AEntitySetSystem<GameTime>
    {
        private IParallelRunner runner;
        private World world;
        
        public TranslationByControllerSystem(World _world, IParallelRunner _runner) : base(_world, CreateEntityContainer, null, 0){
            world = _world;
            runner = _runner;
        }

        [Update]
        private void Update(ref PhysicBody _physicBody, in MovementComponent _movementComponent){
            var dir = _physicBody.RotationVector();
            _physicBody.Body.ApplyForce(dir * ConvertUnits.ToSimUnits(_movementComponent.MovementSpeed));
        }
    }
}