using VelcroPhysics.Utilities;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using System;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Systems
{
    [Without(typeof(PhysicBody))]
    [With(typeof(TranslationComponent))]
    [With(typeof(Transform))]
    public partial class TranslationSystem : AEntitySetSystem<GameTime>
    {
        private IParallelRunner runner;
        private World world;
        
        public TranslationSystem(World _world, IParallelRunner _runner) : base(_world, CreateEntityContainer, null, 0){
            world = _world;
            runner = _runner;
        }

        [Update]
        private void Update(ref Transform _transform, in TranslationComponent _translationComponent){
            var direction = new Vector2((float)Math.Cos(_transform.Rotation), (float)Math.Sin(_transform.Rotation));
            direction.Normalize();

            _transform.Position += direction * _translationComponent.TranslationSpeed;
        }
    }
}