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
    [With(typeof(PhysicBody))]
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
        private void Update(in Renderer _renderer, in PhysicBody _physicBody){
            Vector2 offset = new Vector2(_renderer.Image.Width / 2, _renderer.Image.Height / 2);
            spriteBatch.Draw(_renderer.Image, ConvertUnits.ToDisplayUnits(_physicBody.Body.Position), null, _renderer.Color, ConvertUnits.ToDisplayUnits(_physicBody.Body.Rotation), offset, 1, SpriteEffects.None, 0);
        }

        protected override void PostUpdate(GameTime _state) => world.Optimize(runner, spriteBatch.End);
    }
}