using Microsoft.Xna.Framework.Graphics;
using System;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using System.Collections.Generic;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(Trail))]
    [With(typeof(Transform))]
    public partial class TrailSystem : AEntitySetSystem<float>
    {
        private SpriteBatch spriteBatch;
        private IParallelRunner runner;
        private World world;
        
        public TrailSystem(SpriteBatch _spriteBatch, World _world, IParallelRunner _runner) : base(_world, CreateEntityContainer, null, 0){
            spriteBatch = _spriteBatch;
            runner = _runner;
            world = _world;
        }

        protected override void PreUpdate(float _state) => spriteBatch.Begin();

        [Update]
        private void Update(in Transform _transform, ref Trail _trail){
            int currentTrailLength = Math.Clamp((int)_transform.GetDeltaPosition(), 0, _trail.MaxPointsCount);
            if(_trail.TrailPoints.Count < currentTrailLength){
                //_trail.TrailPoints.AddLast();
            }
        }

        private void PoolOut(LinkedList<TrailPoint> _list, TrailPoint[] _pool){
            // if(_pool.Count > 0){
            //     int lastElementIndex = _pool.Count-1;
            //     TrailPoint pooledElem = _pool[lastElementIndex];
            //     _pool.RemoveAt(lastElementIndex);
            //     _list.AddLast(pooledElem);
            // }
        }

        private void PoolIn(LinkedList<TrailPoint> _list, List<TrailPoint> _pool){
            // if(_pool.Count < _pool.){
            //     int lastElementIndex = _pool.Count-1;
            //     TrailPoint pooledElem = _pool[lastElementIndex];
            //     _pool.RemoveAt(lastElementIndex);
            //     _list.AddLast(pooledElem);
            // }
        }

        protected override void PostUpdate(float _state) => world.Optimize(runner, spriteBatch.End);
    }
}