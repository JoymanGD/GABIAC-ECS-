using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Systems;
using Gabiac.Scripts.ECS.Components;
using System;

namespace Gabiac.Scripts.Scenes
{
    public class GameScene : IScene
    {

#region ECS

        private Entity player;
        private ISystem<float> updateSystems;
        private ISystem<float> drawSystems;
        private IParallelRunner mainRunner;
        private DefaultEcs.World world;
        private GraphicsDeviceManager graphics;

#endregion

        private VelcroPhysics.Dynamics.World physicWorld;

        public GameScene(GraphicsDeviceManager _graphics){
            graphics = _graphics;
            SetupWorld();
            SetupPlayer();
        }

        public void Update(GameTime _gameTime){
            updateSystems.Update((float)_gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public void Draw(GameTime _gameTime){
            drawSystems.Update((float)_gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public void PreLoad(ContentManager _contentManager){

        }

        public void Load(ContentManager _contentManager, SpriteBatch _spriteBatch){
            updateSystems = new SequentialSystem<float>(
                new MouseInputSystem(world, mainRunner),
                new MovementSystem(world, mainRunner),
                new PhysicsSystem(world, mainRunner, physicWorld)
            );

            drawSystems = new SequentialSystem<float>(
                new RenderSystem(_spriteBatch, world, mainRunner),
                new DebugSystem(graphics, _spriteBatch, world, mainRunner)
            );
        }

        public void PostLoad(ContentManager _contentManager){

        }

        public void Unload(){

        }

#region Setup

        private void SetupWorld(){
            mainRunner = new DefaultParallelRunner(Environment.ProcessorCount);
            world = new DefaultEcs.World();
            physicWorld = new VelcroPhysics.Dynamics.World(Vector2.Zero);
        }

        private void SetupPlayer(){
            player = world.CreateEntity();
            player.Set(new Transform(new Vector2(200, 200), new Vector2(1,1), 0));
            player.Set(new Controller(Vector2.Zero, 10, false));
            player.Set(new PhysicBody(physicWorld, new Vector2(200,200), 0, VelcroPhysics.Dynamics.BodyType.Dynamic));
            player.Set(new Renderer(Texture2D.FromFile(graphics.GraphicsDevice, "Content/Car.png"), Color.White));
            player.Set(new Player());
        }

#endregion

    }
}