using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using ECS.Components;

namespace ECS.Systems
{
    [With(typeof(Renderer))]
    [With(typeof(Transform))]
    public partial class DrawSystem : AEntitySetSystem<float>
    {
        private SpriteBatch spriteBatch;
        private IParallelRunner runner;
        private World world;
        public DrawSystem(SpriteBatch _spriteBatch, World _world, IParallelRunner _runner) : base(_world, CreateEntityContainer, null, 0){
            spriteBatch = _spriteBatch;
            runner = _runner;
            world = _world;
        }

        protected override void PreUpdate(float _state) => spriteBatch.Begin();

        [Update]
        private void Update(in Renderer _renderer, in Transform _transform){
            Vector2 offset = new Vector2(_renderer.Image.Width * _transform.Scale.X / 2, _renderer.Image.Height * _transform.Scale.Y / 2);
            spriteBatch.Draw(_renderer.Image, _transform.Position, null, _renderer.Color, _transform.Rotation, offset, _transform.Scale, SpriteEffects.None, 0);
        }

        protected override void PostUpdate(float _state) => world.Optimize(runner, spriteBatch.End);
    }
}