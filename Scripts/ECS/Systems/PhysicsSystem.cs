using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using System;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(Transform))]
    [With(typeof(PhysicBody))]
    public partial class PhysicsSystem : AEntitySetSystem<GameTime>
    {
        private IParallelRunner runner;
        private World world;
        private VelcroPhysics.Dynamics.World physicWorld;
        const float stepRatio = 0.001f, fixedStepRatio = 1f / 30f;
        
        public PhysicsSystem(World _world, IParallelRunner _runner, VelcroPhysics.Dynamics.World _physicWorld) : base(_world, CreateEntityContainer, null, 0){
            world = _world;
            runner = _runner;
            physicWorld = _physicWorld;
        }

        [Update]
        private void Update(ref Transform _transform, in PhysicBody _physicBody, GameTime _gameTime){
            var elapsedTime = (float)_gameTime.ElapsedGameTime.TotalMilliseconds;
            float step = Math.Min(elapsedTime * stepRatio, fixedStepRatio);
            physicWorld.Step(step);
            _transform.SetPosition(_physicBody.Position());
            _transform.SetRotation(_physicBody.Rotation());
            _transform.SetDeltaPosition();
        }
    }
}