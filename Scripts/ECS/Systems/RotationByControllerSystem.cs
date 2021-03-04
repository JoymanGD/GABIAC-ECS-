using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using Microsoft.Xna.Framework;
using System;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(PhysicBody))]
    [With(typeof(Controller))]
    [With(typeof(RotatePlayer))]
    public partial class RotationByControllerSystem : AEntitySetSystem<GameTime>
    {
        private IParallelRunner runner;
        private World world;
        
        public RotationByControllerSystem(World _world, IParallelRunner _runner) : base(_world, CreateEntityContainer, null, 0){
            world = _world;
            runner = _runner;
        }

        [Update]
        private void Update(ref PhysicBody _physicBody, in Controller _controller){
            var lookDir = _controller.Direction;
            if(lookDir != Vector2.Zero)
                _physicBody.SetRotation((float)Math.Atan2(lookDir.Y, lookDir.X));
        }
    }
}