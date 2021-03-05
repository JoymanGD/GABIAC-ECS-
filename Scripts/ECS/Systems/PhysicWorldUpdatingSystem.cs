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
    [With(typeof(Transform))]
    public partial class PhysicWorldUpdatingSystem : AEntitySetSystem<GameTime>
    {
        private IParallelRunner runner;
        private World world;
        private VelcroPhysics.Dynamics.World physicWorld;
        const float stepRatio = 0.001f, fixedStepRatio = 1f / 30f;
        
        public PhysicWorldUpdatingSystem(World _world, IParallelRunner _runner, VelcroPhysics.Dynamics.World _physicWorld) : base(_world, CreateEntityContainer, null, 0){
            world = _world;
            runner = _runner;
            physicWorld = _physicWorld;
        }

        [Update]
        private void Update(in PhysicBody _physicBody, ref Transform _transform, GameTime _gameTime){
            var elapsedTime = (float)_gameTime.ElapsedGameTime.TotalMilliseconds;
            float step = Math.Min(elapsedTime * stepRatio, fixedStepRatio);
            _transform.Position = ConvertUnits.ToDisplayUnits(_physicBody.Body.Position);
            _transform.Rotation = ConvertUnits.ToDisplayUnits(_physicBody.Body.Rotation);
            physicWorld.Step(step);
        }
    }
}