using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components
{
    public struct Trail
    {
        public int MaxPointsCount { get; private set; }
        public float PointsDistance { get; private set; }
        public LinkedList<TrailPoint> TrailPoints;
        public TrailPoint[] Pool;

        public Trail(int _maxPointsCount, float _pointsDistance){
            MaxPointsCount = _maxPointsCount;
            PointsDistance = _pointsDistance;
            TrailPoints = new LinkedList<TrailPoint>();
            Pool = new TrailPoint[MaxPointsCount];
            for (var i = 0; i < MaxPointsCount; i++)
            {
                Pool[i] = new TrailPoint();
            }
        }
    }
}