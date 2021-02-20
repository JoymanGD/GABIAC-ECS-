using VelcroPhysics.Factories;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Dynamics.Joints;
using System.Collections.Generic;
using VelcroPhysics.Collision.Filtering;
using VelcroPhysics.Utilities;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components
{
    public struct Trail
    {
        public int MaxPointsCount { get; private set; }
        public float PointsDistance { get; private set; }
        public LinkedList<Body> TrailPoints;
        public LinkedList<Body> Pool;
        public Body Car;

        public Trail(int _maxPointsCount, float _pointsDistance, World _physicWorld, Body _car, Category _category = Category.None){
            Car = _car;
            MaxPointsCount = _maxPointsCount;
            PointsDistance = _pointsDistance;
            TrailPoints = new LinkedList<Body>();
            Pool = new LinkedList<Body>();
            for (var i = 0; i < MaxPointsCount; i++)
            {
                //var body = BodyFactory.CreateBody(_physicWorld, default, default, BodyType.Dynamic);
                var body = BodyFactory.CreateCircle(_physicWorld, ConvertUnits.ToSimUnits(20), 0, default, BodyType.Dynamic);
                body.AngularDamping = .03f;
                body.LinearDamping = 1f;
                body.SleepingAllowed = true;

                //body.FixedRotation = true;
                body.Enabled = false;
                body.Mass = _car.Mass / 100;
                Pool.AddLast(body);
            }
            var first = Pool.Last;
            var firstBody = first.Value;
            firstBody.Position = Car.Position + ConvertUnits.ToSimUnits(new Vector2(5,5));
            firstBody.Enabled = true;
            var anchor = Vector2.Lerp(Car.Position, firstBody.Position, .5f);
            var rj = new RopeJoint(Car, firstBody, anchor, anchor);
            var sj = new WeldJoint(Car, firstBody, anchor, anchor);
            var jj = new FixedMouseJoint(firstBody, Vector2.Zero);
            rj.MaxLength = ConvertUnits.ToSimUnits(_pointsDistance);
            _physicWorld.AddJoint(rj);
            Pool.RemoveLast();
            TrailPoints.AddFirst(first);
        }
    }
}