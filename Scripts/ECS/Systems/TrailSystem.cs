using VelcroPhysics.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Dynamics.Joints;
using MonoGame.Extended;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(Trail))]
    [With(typeof(Transform))]
    public partial class TrailSystem : AEntitySetSystem<float>
    {
        private SpriteBatch spriteBatch;
        private IParallelRunner runner;
        private DefaultEcs.World world;
        private VelcroPhysics.Dynamics.World physicWorld;
        
        public TrailSystem(SpriteBatch _spriteBatch, DefaultEcs.World _world, IParallelRunner _runner, VelcroPhysics.Dynamics.World _physicWorld) : base(_world, CreateEntityContainer, null, 0){
            spriteBatch = _spriteBatch;
            runner = _runner;
            world = _world;
            physicWorld = _physicWorld;
        }

        protected override void PreUpdate(float _state) => spriteBatch.Begin();

        [Update]
        private void Update(in Transform _transform, ref Trail _trail){
            int currentTrailLength = Math.Clamp((int)_transform.GetDeltaPosition(), 0, _trail.MaxPointsCount);
            foreach (var trailPoint in _trail.TrailPoints)
            {
                //spriteBatch.DrawCircle(trailPoint.Position, 20, Color.Green);
            }
            if(_trail.TrailPoints.Count < currentTrailLength){
                
            }
        }

        private void AddTrailPoint(Trail _trail, Vector2 _direction){
            var _pool = _trail.Pool;
            var _list = _trail.TrailPoints;
            if(_pool.Count > 0){
                var body = _pool.Last.Value;
                var lastPoint = _list.Last.Value;
                
                body.Position = lastPoint.Position + ConvertUnits.ToSimUnits(_direction * _trail.PointsDistance);
                body.Enabled = true;

                //creating joint
                var anchor = Vector2.Lerp(lastPoint.Position, body.Position, .5f);
                var dj = new DistanceJoint(lastPoint, body, anchor, anchor);
                physicWorld.AddJoint(dj);

                //pooling
                _list.AddLast(_pool.Last);
                _pool.RemoveLast();
            }
        }

        private void RemoveTrailPoint(Trail _trail){
            var _pool = _trail.Pool;
            var _list = _trail.TrailPoints;
            if(_pool.Count < _trail.MaxPointsCount){
                var body = _list.Last.Value;

                body.Enabled = false;

                //removing joint
                physicWorld.RemoveJoint(body.JointList.Joint);

                //pooling
                _pool.AddLast(_list.Last);
                _list.RemoveLast();
            }
        }

        protected override void PostUpdate(float _state) => world.Optimize(runner, spriteBatch.End);
    }
}