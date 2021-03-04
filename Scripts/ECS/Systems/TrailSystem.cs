using VelcroPhysics.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Dynamics.Joints;
using MonoGame.Extended;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(Trail))]
    [With(typeof(Controller))]
    [With(typeof(PhysicBody))]
    [With(typeof(DoTheTrail))]
    public partial class TrailSystem : AEntitySetSystem<GameTime>
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

        protected override void PreUpdate(GameTime _state) => spriteBatch.Begin();

        [Update]
        private void Update(ref Trail _trail, in Controller _controller, in PhysicBody _physicBody){
            //logic
            if(_trail.TrailPoints.Count == 0)
                AddTrailPointNew(_trail, _physicBody, _controller);
            
            float distanceToFirst = ConvertUnits.ToDisplayUnits(_physicBody.Body.Position - _trail.TrailPoints.First.Value.Position).Length();
            float distanceToLast = ConvertUnits.ToDisplayUnits(_trail.TrailPoints.First.Value.Position - _trail.TrailPoints.Last.Value.Position).Length();
            if(distanceToFirst > _trail.PointsDistance){
                AddTrailPointNew(_trail, _physicBody, _controller);
            }

            // if(distanceToLast > _trail.PointsDistance * 4){
            //     RemoveTrailPointNew(_trail);
            // }

            if(_trail.TrailPoints.Count == _trail.MaxPointsCount){
                RemoveTrailPointNew(_trail);
            }
        }
        private void AddTrailPointNew(Trail _trail, PhysicBody _physicBody, Controller _controller){
            var _pool = _trail.Pool;
            var _list = _trail.TrailPoints;
            
            if(_pool.Count > 0){
                var lastPoolNode = _pool.Last;
                var lastBody = lastPoolNode.Value;
                lastBody.Enabled = true;
                lastBody.Position = _physicBody.Position(false);

                //pooling
                _pool.RemoveLast();
                _list.AddFirst(lastPoolNode);
            }
        }

        private void RemoveTrailPointNew(Trail _trail){
            var _pool = _trail.Pool;
            var _list = _trail.TrailPoints;
            if(_pool.Count < _trail.MaxPointsCount){
                var lastListNode = _list.Last;
                var body = lastListNode.Value;

                body.IsSensor = true;
                body.Enabled = false;

                //pooling
                _list.RemoveLast();
                _pool.AddLast(lastListNode);
            }
        }

        protected override void PostUpdate(GameTime _state) => world.Optimize(runner, spriteBatch.End);
    }
}