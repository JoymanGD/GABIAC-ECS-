using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Gabiac.Scripts.Managers;
using Gabiac.Scripts.Scenes;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using Gabiac.Scripts.Helpers;
using VelcroPhysics.Collision.ContactSystem;

namespace Gabiac
{
    public class Gabiac : Game
    {
        public Gabiac()
        {
            GabiacSettings.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            LoadSettings();
            SetupGraphics();
            SetupHelpers();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SceneManager.LoadScene<MenuScene>();
        }

        protected override void Update(GameTime _gameTime)
        {
            SceneManager.Update(_gameTime);

            base.Update(_gameTime);
        }

        protected override void Draw(GameTime _gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            SceneManager.Draw(_gameTime);
            
            base.Draw(_gameTime);
        }

#region Setup

        private void SetupGraphics(){
            GabiacSettings.graphics.IsFullScreen = false;
            GabiacSettings.graphics.PreferredBackBufferWidth = 1920;
            GabiacSettings.graphics.PreferredBackBufferHeight = 1080;
            GabiacSettings.graphics.ApplyChanges();
        }

        private void LoadSettings(){
            GabiacSettings.spriteBatch = new SpriteBatch(GraphicsDevice);
            GabiacSettings.mainRunner = new DefaultParallelRunner(Environment.ProcessorCount);
            GabiacSettings.game = this;
            GabiacSettings.graphicsDevice = GraphicsDevice;
            GabiacSettings.world = new DefaultEcs.World();
            GabiacSettings.contentManager = Content;
            GabiacSettings.physicWorld = new VelcroPhysics.Dynamics.World(Vector2.Zero);
        }

        private void SetupHelpers(){
            new SceneCreator();
        }

#endregion
    }
}
