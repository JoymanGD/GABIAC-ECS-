using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.ECS.Components;
using FontStashSharp;
using System.IO;
using Microsoft.Xna.Framework.Input;

namespace Gabiac.ECS.Systems
{
    [With(typeof(Transform))]
    [With(typeof(Player))]
    public partial class DebugSystem : AEntitySetSystem<float>
    {
        private SpriteBatch spriteBatch;
        private IParallelRunner runner;
        private World world;
        private FontSystem fontSystem;
        private SpriteFontBase font;
        private GraphicsDeviceManager graphics;
        
        public DebugSystem(GraphicsDeviceManager _graphics, SpriteBatch _spriteBatch, World _world, IParallelRunner _runner) : base(_world, CreateEntityContainer, null, 0){
            graphics = _graphics;
            spriteBatch = _spriteBatch;
            runner = _runner;
            world = _world;
            fontSystem = new FontSystem(graphics.GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            fontSystem.AddFont(File.ReadAllBytes(@"Content/Fonts/NotoSansJP-Light.otf"));
            font = fontSystem.GetFont(30);
        }

        protected override void PreUpdate(float _state) => spriteBatch.Begin();

        [Update]
        private void Update(ref Transform _transform){
            spriteBatch.DrawString(font, 
                                        
                                        "Velocity: " + _transform.GetDeltaPosition().ToString() + 
                                        "\n" +
                                        "Position: " + _transform.Position.ToString() + 
                                        "\n" +
                                        "MousePos: " + Mouse.GetState().Position.ToString()

            , new Vector2(40, 40), Color.White);
        }

        protected override void PostUpdate(float _state) => world.Optimize(runner, spriteBatch.End);
    }
}   