using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using ECS.Components;
using VelcroPhysics.Utilities;

namespace ECS.Systems
{
    [With(typeof(Transform))]
    [With(typeof(PhysicBody))]
    public partial class PhysicsSystem : AEntitySetSystem<float>
    {
        private IParallelRunner runner;
        private World world;
        private VelcroPhysics.Dynamics.World physicWorld;
        
        public PhysicsSystem(World _world, IParallelRunner _runner, VelcroPhysics.Dynamics.World _physicWorld) : base(_world, CreateEntityContainer, null, 0){
            world = _world;
            runner = _runner;
            physicWorld = _physicWorld;
        }

        [Update]
        private void Update(ref Transform _transform, in PhysicBody _physicBody, float elapsedTime){
            physicWorld.Step(elapsedTime);
            _transform.SetPosition(_physicBody.Position());
            _transform.SetRotation(_physicBody.Rotation());
        }
    }
}