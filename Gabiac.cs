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
            var Player = world.CreateEntity();
            Player.Set(new Transform(new Vector2(200, 200), new Vector2(1,1), 0));
            Player.Set(new Controller(Vector2.Zero, 1, false));
            Player.Set(new PhysicBody(physicWorld, new Vector2(200,200), 0, BodyType.Dynamic));
            Player.Set(new Renderer(Texture2D.FromFile(GraphicsDevice, "Content/Car.png"), Color.White));
        }
    }
}
