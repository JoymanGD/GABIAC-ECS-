using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using VelcroPhysics.Utilities;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(Ball))]
    [With(typeof(PhysicBody))]
    
    public partial class BallMovementSystem : AEntitySetSystem<GameTime>
    {
        private World world;
        
        public BallMovementSystem(World _world) : base(_world){
            world = _world;
        }

        [Update]
        private void Update(ref PhysicBody _physicBody, ref Ball _ball){
            _physicBody.Body.ApplyForce(ConvertUnits.ToSimUnits(_ball.Direction * _ball.Speed));
        }
    }
}