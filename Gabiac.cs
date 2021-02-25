using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Gabiac.Scripts.Managers;
using Gabiac.Scripts.Scenes;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using Gabiac.Scripts.Helpers;

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
            SetupEntities();
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

        private void SetupEntities(){
            var player = GabiacSettings.world.CreateEntity();
            var texture = Texture2D.FromFile(GabiacSettings.graphics.GraphicsDevice, "Content/Car.png");
            var physicWorld = GabiacSettings.physicWorld;

            player.Set(new Transform(new Vector2(1,1), 0));
            player.Set(new Controller(Vector2.Zero, 3, false));
            var physicBody = new PhysicBody(physicWorld, new Vector2(200,200), new Vector2(texture.Width, texture.Height), 0, VelcroPhysics.Dynamics.BodyType.Dynamic);
            player.Set(physicBody);
            player.Set(new Renderer(texture, Color.White));
            player.Set(new Player());
            player.Set(new RocketFire());
            player.Set(new RotatePlayer());
            //player.Set(new MouseInput());
            player.Set(new KeyboardInput());
            player.Set(new Trail(10, 45, physicWorld, physicBody.Body, VelcroPhysics.Collision.Filtering.Category.None));
            
            var player1 = GabiacSettings.world.CreateEntity();
            player1.Set(new Transform(new Vector2(1,1), 0));
            player1.Set(new PhysicBody(physicWorld, new Vector2(400,400), new Vector2(texture.Width, texture.Height), 0, VelcroPhysics.Dynamics.BodyType.Dynamic));
            player1.Set(new Renderer(texture, Color.Red));
        }

#endregion
    }
}
