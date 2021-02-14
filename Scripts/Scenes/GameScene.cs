using System.Diagnostics.SymbolStore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Systems;
using Gabiac.Scripts.ECS.Components;
using Gabiac.Scripts.Managers;
using LilyPath;
using LilyPath.Utility;
using LilyPath.Shapes;
using System;
namespace Gabiac.Scripts.Scenes
{
    public class GameScene : IScene
    {

#region ECS

        private Entity player;
        private ISystem<float> updateSystems;
        private ISystem<float> drawSystems;
        private IParallelRunner mainRunner;MonoGame.SplineFlower.BezierCurve sad;
        private DefaultEcs.World world; private DrawBatch drawBatch; Pen pen;

#endregion

        private VelcroPhysics.Dynamics.World physicWorld;

        public GameScene(){
            SetupWorld();
            SetupPlayer();
        }

        public void Update(GameTime _gameTime){
            updateSystems.Update((float)_gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public void Draw(GameTime _gameTime){
            drawBatch.DrawBezier(pen, new Vector2(200,200), new Vector2(250,250), new Vector2(400, 200));
            drawSystems.Update((float)_gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public void PreLoad(){

        }

        public void Load(){
            var spriteBatch = SceneManager.instance.spriteBatch;
            var graphics = SceneManager.instance.graphics;
            drawBatch = new DrawBatch(graphics.GraphicsDevice);
            pen = new Pen(Color.Red);
            updateSystems = new SequentialSystem<float>(
                new MouseInputSystem(world, mainRunner),
                new MovementSystem(world, mainRunner),
                new PhysicsSystem(world, mainRunner, physicWorld)
            );

            drawSystems = new SequentialSystem<float>(
                new RenderSystem(spriteBatch, world, mainRunner),
                new DebugSystem(graphics, spriteBatch, world, mainRunner)
            );
        }

        public void PostLoad(){

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
            player.Set(new Renderer(Texture2D.FromFile(SceneManager.instance.graphics.GraphicsDevice, "Content/Car.png"), Color.White));
            player.Set(new Player());
        }

#endregion

    }
}