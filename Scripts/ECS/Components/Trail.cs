using VelcroPhysics.Factories;
using VelcroPhysics.Dynamics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components
{
    public struct Trail
    {
        public int MaxPointsCount { get; private set; }
        public float PointsDistance { get; private set; }
        public LinkedList<Body> TrailPoints;
        public LinkedList<Body> Pool;

        public Trail(int _maxPointsCount, float _pointsDistance, World _physicWorld){
            MaxPointsCount = _maxPointsCount;
            PointsDistance = _pointsDistance;
            TrailPoints = new LinkedList<Body>();
            Pool = new LinkedList<Body>();
            for (var i = 0; i < MaxPointsCount; i++)
            {
                var body = BodyFactory.CreateBody(_physicWorld, default, default, BodyType.Dynamic);
                body.Enabled = false;
                Pool.AddLast(body);
            }
        }
    }
}