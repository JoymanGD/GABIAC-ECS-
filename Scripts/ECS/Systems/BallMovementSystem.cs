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
        private void Update(ref PhysicBody _physicBody, ref RotationComponent _rotationComponent, in TranslationComponent _translationComponent){
            var contList = _physicBody.Body.ContactList;
            _physicBody.Body.Position += ConvertUnits.ToSimUnits(_rotationComponent.Direction * _translationComponent.TranslationSpeed);
            if(contList != null){
                var contact = _physicBody.Body.ContactList.Contact;
                Vector2 normal = ConvertUnits.ToDisplayUnits(contact.FixtureA.Body.Position - contact.FixtureB.Body.Position);
                normal.Normalize();
                _rotationComponent.Direction = Vector2.Reflect(_rotationComponent.Direction, normal);
            }
        }
    }
}