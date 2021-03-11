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
    public partial class TranformByPhysicBodyUpdatingSystem : AEntitySetSystem<GameTime>
    {
        private IParallelRunner runner;
        private World world;
        
        public TranformByPhysicBodyUpdatingSystem(World _world, IParallelRunner _runner) : base(_world, CreateEntityContainer, null, 0){
            world = _world;
            runner = _runner;
        }

        [Update]
        private void Update(in PhysicBody _physicBody, ref Transform _transform){
            _transform.Position = ConvertUnits.ToDisplayUnits(_physicBody.Body.Position);
            _transform.Rotation = ConvertUnits.ToDisplayUnits(_physicBody.Body.Rotation);
        }
    }
}