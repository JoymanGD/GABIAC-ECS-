using System;
using Gabiac.Scripts.ECS.Components.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using VelcroPhysics.Utilities;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(Renderer))]
    [With(typeof(Transform))]
    public partial class RenderingSystem : AEntitySetSystem<GameTime>
    {
        private SpriteBatch spriteBatch;
        private IParallelRunner runner;
        private World world;
        
        public RenderingSystem(SpriteBatch _spriteBatch, World _world, IParallelRunner _runner) : base(_world, CreateEntityContainer, null, 0){
            spriteBatch = _spriteBatch;
            runner = _runner;
            world = _world;
        }
        protected override void PreUpdate(GameTime _state) => spriteBatch.Begin();

        [Update]
        private void Update(in Renderer _renderer, in Transform _transform){
            Vector2 offset = new Vector2(_renderer.Image.Width / 2, _renderer.Image.Height / 2);
            spriteBatch.Draw(_renderer.Image, _transform.Position, null, _renderer.Color, _transform.Rotation, offset, 1, SpriteEffects.None, _renderer.Layer);
        }

        protected override void PostUpdate(GameTime _state) => world.Optimize(runner, spriteBatch.End);
    }
}