using Microsoft.Xna.Framework.Graphics;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;

namespace ECS.Components
{
    [With(typeof(Renderer))]
    public partial class DrawSystem : AEntitySetSystem<float>
    {
        private SpriteBatch spriteBatch;
        private IParallelRunner runner;
        Texture2D sad;
        private World world;
        public DrawSystem(SpriteBatch _spriteBatch, World _world, IParallelRunner _runner) : base(_world, CreateEntityContainer, null, 0){
            spriteBatch = _spriteBatch;
            runner = _runner;
            
            world = _world;
        }

        protected override void PreUpdate(float _qstate) => spriteBatch.Begin();

        [Update]
        private void Update(){
            
        }

        protected override void PostUpdate(float _state) => world.Optimize(runner, spriteBatch.End);
    }
}