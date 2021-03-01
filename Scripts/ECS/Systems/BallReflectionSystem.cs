using Microsoft.Xna.Framework;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using VelcroPhysics.Utilities;

namespace Gabiac.Scripts.ECS.Systems
{
    [WhenAdded(typeof(BallReflection))]
    [With(typeof(PhysicBody))]
    
    public partial class BallReflectionSystem : AEntitySetSystem<float>
    {
        private World world;
        
        public BallReflectionSystem(World _world) : base(_world, CreateEntityContainer, null, 0){
            world = _world;
        }

        [Update]
        private void Update(ref Ball _ball, [Added] in BallReflection _ballReflection, in Entity _entity){
            _ball.SetDirection(Vector2.Reflect(_ball.Direction, _ballReflection.Normal));
            _entity.Remove<BallReflection>();
        }
    }
}