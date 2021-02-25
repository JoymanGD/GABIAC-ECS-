using System;
using VelcroPhysics.Collision.ContactSystem;
using VelcroPhysics.Factories;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Collision.Shapes;
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
        public bool Whipped;

        public Trail(int _maxPointsCount, float _pointsDistance, World _physicWorld, Body _car, Category _category = Category.None){
            Car = _car;
            MaxPointsCount = _maxPointsCount;
            PointsDistance = _pointsDistance;
            TrailPoints = new LinkedList<Body>();
            Pool = new LinkedList<Body>();
            Whipped = false;
            for (var i = 0; i < MaxPointsCount; i++)
            {
                //var body = BodyFactory.CreateBody(_physicWorld, default, default, BodyType.Dynamic);
                var circleRadius = ConvertUnits.ToSimUnits(20);
                var body = BodyFactory.CreateCircle(_physicWorld, circleRadius, 0, default, BodyType.Kinematic);
                body.AngularDamping = .03f;
                body.LinearDamping = 1f;
                body.SleepingAllowed = true;
                body.OnSeparation += DisableBody;
                body.IsSensor = true;
                body.Enabled = false;
                body.Mass = _car.Mass / 100;
                Pool.AddLast(body);
            }
        }

        public void DisableBody(Fixture _fixtureA, Fixture _fixtureB, Contact _contact){
            _fixtureA.IsSensor = false;
        }
    }
}