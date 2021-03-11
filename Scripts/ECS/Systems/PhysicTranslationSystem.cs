using VelcroPhysics.Utilities;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using System;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(PhysicBody), typeof(TranslationComponent))]
    public partial class PhysicTranslationSystem : AEntitySetSystem<GameTime>
    {
        private IParallelRunner runner;
        private World world;
        
        public PhysicTranslationSystem(World _world, IParallelRunner _runner) : base(_world, CreateEntityContainer, null, 0){
            world = _world;
            runner = _runner;
        }

        [Update]
        private void Update(ref PhysicBody _physicBody, in TranslationComponent _movementComponent){
            var rotation = ConvertUnits.ToDisplayUnits(_physicBody.Body.Rotation);
            var direction = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            direction.Normalize();
            if(_movementComponent.CurrentType == TranslationComponent.TranslationType.ApplyForce)
                _physicBody.Body.ApplyForce(direction * _movementComponent.TranslationSpeed);
            else if(_movementComponent.CurrentType == TranslationComponent.TranslationType.TranslatePosition)
                _physicBody.Body.Position += ConvertUnits.ToSimUnits(direction * _movementComponent.TranslationSpeed);
        }
    }
}