using VelcroPhysics.Dynamics;
using VelcroPhysics.Utilities;
using Microsoft.Xna.Framework;
using VelcroPhysics.Factories;

namespace ECS.Components
{
    public struct PhysicBody
    {
        public Body Body {get; private set;}

        public PhysicBody(World _world, Vector2 _position, float _rotation, BodyType _bodyType){
            _position = ConvertUnits.ToSimUnits(_position);
            Body = BodyFactory.CreateRectangle(_world, 2,2, 1, _position, _rotation, _bodyType);
            Body.AngularDamping = .03f;
            Body.LinearDamping = 1f;
            Body.SleepingAllowed = true;
        }

        public void SetBody(Body _body){
            Body = _body;
        }

        public void SetPosition(Vector2 _position){
            Body.Position = ConvertUnits.ToSimUnits(_position);
        }

        public Vector2 Position(bool UnSim = true){
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

        public float Rotation(bool UnSim = true){
            float _rotation;
            if(UnSim)
                _rotation = ConvertUnits.ToDisplayUnits(Body.Rotation);
            else
                _rotation = Body.Rotation;
            return _rotation;
        }
    }
}