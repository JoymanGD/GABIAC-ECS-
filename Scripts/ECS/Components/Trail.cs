using System.Reflection;
using System.Net.Mime;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components
{
    public struct Trail
    {
        public int PointsCount { get; private set; }
        public float PointsDistance { get; private set; }
        public float TrailLength { get; private set; }

        public Trail(int _pointsCount, float _pointsDistance, float _trailLength){
            PointsCount = _pointsCount;
            PointsDistance = _pointsDistance;
            TrailLength = _trailLength;
        }
    }
}