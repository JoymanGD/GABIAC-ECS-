using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using ECS.Systems;
using ECS.Components;
using VelcroPhysics.Dynamics;
using FontStashSharp;
using System.IO;

namespace Gabiac
{
    public class Gabiac : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ISystem<float> systems;
        private ISystem<float> drawSystem;
        private IParallelRunner mainRunner;
        private DefaultEcs.World world;
        private VelcroPhysics.Dynamics.World physicWorld;
        private FontSystem fontSystem;
        private SpriteFontBase font;

        private Entity Player;

        public Gabiac()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.IsFullScreen = true;
        }
        protected override void Initialize()
        {
            SetupGraphics();
            SetupWorld();
            SetupPlayer();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            systems = new SequentialSystem<float>(
                new MouseInputSystem(world, mainRunner),
                new MovementSystem(world, mainRunner),
                new PhysicsSystem(world, mainRunner, physicWorld)
            );
            drawSystem = new DrawSystem(_spriteBatch, world, mainRunner);

            fontSystem = new FontSystem(GraphicsDevice, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            fontSystem.AddFont(File.ReadAllBytes(@"Content/Fonts/NotoSansJP-Light.otf"));
            font = fontSystem.GetFont(30);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            systems.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            drawSystem.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);

            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, 
                                        
                                        "Velocity: " + Player.Get<Transform>().GetDeltaPosition().ToString() + 
                                        "\n" +
                                        "Position: " + Player.Get<Transform>().Position.ToString() + 
                                        "\n" +
                                        "MousePos: " + Mouse.GetState().Position.ToString()

            , new Vector2(40, 40), Color.White);
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }

        private void SetupGraphics(){
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();
        }

        private void SetupWorld(){
            mainRunner = new DefaultParallelRunner(Environment.ProcessorCount);
            world = new DefaultEcs.World();
            physicWorld = new VelcroPhysics.Dynamics.World(Vector2.Zero);
        }

        private void SetupPlayer(){
            Player = world.CreateEntity();
            Player.Set(new Transform(new Vector2(200, 200), new Vector2(1,1), 0));
            Player.Set(new Controller(Vector2.Zero, 40, false));
            Player.Set(new PhysicBody(physicWorld, new Vector2(200,200), 0, BodyType.Dynamic));
            Player.Set(new Renderer(Texture2D.FromFile(GraphicsDevice, "Content/Car.png"), Color.White));
        }
    }
}
