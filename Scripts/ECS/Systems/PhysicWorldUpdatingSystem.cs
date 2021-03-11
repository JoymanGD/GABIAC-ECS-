using VelcroPhysics.Utilities;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using System;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(PhysicWorld))]
    public class PhysicWorldUpdatingSystem : ISystem<GameTime>
    {
        private World world;
        public bool IsEnabled { get; set; } = true;
        private VelcroPhysics.Dynamics.World physicWorld;
        const float stepRatio = 0.001f, fixedStepRatio = 1f / 30f;
        
        public PhysicWorldUpdatingSystem(World _world, VelcroPhysics.Dynamics.World _physicWorld){
            world = _world;
            physicWorld = _physicWorld;
        }

        public void Update(GameTime _gameTime){
            var elapsedTime = (float)_gameTime.ElapsedGameTime.TotalMilliseconds;
            float step = Math.Min(elapsedTime * stepRatio, fixedStepRatio);
            physicWorld.Step(step);
        }

        public void Dispose(){}
    }
}