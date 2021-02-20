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
    [With(typeof(Transform))]
    [With(typeof(Controller))]
    [With(typeof(PhysicBody))]
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
        private void Update(in Transform _transform, ref Trail _trail, in Controller _controller, in PhysicBody _physicBody){
            //logic
            //int currentTrailLength = Math.Clamp((int)(_transform.DeltaPosition*2), 0, _trail.MaxPointsCount);
            float distance = ConvertUnits.ToDisplayUnits(_physicBody.Body.Position - _trail.TrailPoints.First.Value.Position).Length();
            if(distance > _trail.PointsDistance){
                //AddTrailPointNew(_trail);
            }
            // if(_trail.TrailPoints.Count < currentTrailLength){
            //     AddTrailPoint(_trail, _controller.MovementDirection);
            // }
            // else if(_trail.TrailPoints.Count > currentTrailLength){
            //     //RemoveTrailPoint(_trail);
            // }


            //debug draw
            foreach (var trailPoint in _trail.TrailPoints)
            {
                spriteBatch.DrawCircle(ConvertUnits.ToDisplayUnits(trailPoint.Position), 20, 32, Color.Green);
            }

            foreach (var j in physicWorld.JointList)
            {
                spriteBatch.DrawLine(ConvertUnits.ToDisplayUnits(j.BodyA.Position), ConvertUnits.ToDisplayUnits(j.BodyB.Position), Color.Yellow, 1);
            }
        }
        private void AddTrailPointNew(Trail _trail){
            var _pool = _trail.Pool;
            var _list = _trail.TrailPoints;
            
            if(_pool.Count > 0){
                var lastPoolNode = _pool.Last;

                var newPoint = lastPoolNode.Value;
                var prevPoint = _list.First.Value;
                var car = _trail.Car;

                
                // var body = lastPoolNode.Value;
                // Body lastPoint;

                // //setting new node
                // if(_list.Count == 0){
                //     lastPoint = _trail.Car;
                // }
                // else{
                //     lastPoint = _list.Last.Value;
                // }

                // //pooling
                // _pool.RemoveLast();
                // _list.AddLast(lastPoolNode);

                // //creating joint
                // var anchor = Vector2.Lerp(lastPoint.Position, body.Position, .5f);
                // var joint = new RopeJoint(lastPoint, body, anchor, anchor);
                // physicWorld.AddJoint(joint);
            }
        }

        private void AddTrailPoint(Trail _trail, Vector2 _direction){
            var _pool = _trail.Pool;
            var _list = _trail.TrailPoints;
            
            if(_pool.Count > 0){
                var lastPoolNode = _pool.Last;
                var body = lastPoolNode.Value;
                Body lastPoint;

                //setting new node
                if(_list.Count == 0){
                    lastPoint = _trail.Car;
                }
                else{
                    lastPoint = _list.Last.Value;
                }

                //pooling
                _pool.RemoveLast();
                _list.AddLast(lastPoolNode);
                

                body.Position = lastPoint.Position + ConvertUnits.ToSimUnits(_direction * _trail.PointsDistance);
                body.Enabled = true;

                //creating joint
                var anchor = Vector2.Lerp(lastPoint.Position, body.Position, .5f);
                var joint = new RopeJoint(lastPoint, body, anchor, anchor);
                physicWorld.AddJoint(joint);
            }
        }

        private void RemoveTrailPoint(Trail _trail){
            var _pool = _trail.Pool;
            var _list = _trail.TrailPoints;
            if(_pool.Count < _trail.MaxPointsCount){
                var lastListNode = _list.Last;
                var body = lastListNode.Value;

                body.Enabled = false;

                //removing joint
                physicWorld.RemoveJoint(body.JointList.Joint);

                //pooling
                _list.RemoveLast();
                _pool.AddLast(lastListNode);
            }
        }

        protected override void PostUpdate(float _state) => world.Optimize(runner, spriteBatch.End);
    }
}