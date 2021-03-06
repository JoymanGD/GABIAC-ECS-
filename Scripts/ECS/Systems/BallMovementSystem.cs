using DefaultEcs;
using DefaultEcs.System;
using Gabiac.Scripts.ECS.Components;
using VelcroPhysics.Utilities;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(Ball), typeof(PhysicBody), typeof(TranslationComponent), typeof(RotationComponent))]
    
    public partial class BallMovementSystem : AEntitySetSystem<GameTime>
    {
        private World world;
        
        public BallMovementSystem(World _world) : base(_world){
            world = _world;
        }

        [Update]
        private void Update(ref PhysicBody _physicBody, ref RotationComponent _rotationComponent){
        }
    }
}