using VelcroPhysics.Dynamics;
using VelcroPhysics.Collision.Shapes;
using VelcroPhysics.Utilities;
using Microsoft.Xna.Framework;
using VelcroPhysics.Factories;
using VelcroPhysics.Collision.Filtering;
using VelcroPhysics.Shared.Optimization;
using VelcroPhysics.Collision.ContactSystem;
using System;

namespace Gabiac.Scripts.ECS.Components
{
    public struct PhysicBody
    {
        public Body Body {get; private set;}

        public PhysicBody(World _world, Vector2 _position, Vector2 _scale, float _rotation, BodyType _bodyType, Category _category = Category.All){
            _position = ConvertUnits.ToSimUnits(_position);
            _rotation = ConvertUnits.ToSimUnits(_rotation);
            _scale = ConvertUnits.ToSimUnits(_scale);
            
            Body = BodyFactory.CreateRectangle(_world, _scale.X, _scale.Y, 1, _position, _rotation, _bodyType);
            Body.AngularDamping = .03f;
            Body.LinearDamping = 1f;
            Body.CollidesWith = _category;
            Body.SleepingAllowed = true;
            Body.FixedRotation = true;
        }

        public PhysicBody(World _world, Vector2 _position, float _radius, BodyType _bodyType, Category _category = Category.All, float _mass = 0){
            _radius = ConvertUnits.ToSimUnits(_radius);
            _position = ConvertUnits.ToSimUnits(_position);
            
            Body = BodyFactory.CreateCircle(_world, _radius, 0, _position, _bodyType);
            Body.AngularDamping = .03f;
            Body.LinearDamping = 1f;
            Body.CollidesWith = _category;
            Body.SleepingAllowed = true;
            Body.FixedRotation = true;
            if(_mass > 0)
                Body.Mass = _mass;
        }

        public Vector2 GetPosition(bool UnSim = true){
            Vector2 _position;
            if(UnSim)
                _position = ConvertUnits.ToDisplayUnits(Body.Position);
            else
                _position = Body.Position;
            return _position;
        }

        public void SetRotation(float _rotation){
            Body.Rotation = ConvertUnits.ToSimUnits(_rotation);
        }
    }
}