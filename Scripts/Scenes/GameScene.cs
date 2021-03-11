using Microsoft.Xna.Framework;
using DefaultEcs.System;
using Gabiac.Scripts.ECS.Systems;
using Microsoft.Xna.Framework.Graphics;
using Gabiac.Scripts.Helpers;

namespace Gabiac.Scripts.Scenes
{
    public class GameScene : IScene
    {
        public override void Draw(GameTime _gameTime)
        {
            GabiacSettings.graphicsDevice.Clear(Color.White);
            base.Draw(_gameTime);
        }

        public override void SetEntities(){
            var ecsWorld = GabiacSettings.world;
            var physicWorld = GabiacSettings.physicWorld;

            var entityBuilder = new EntityBuilder();

            var background = entityBuilder.BuildBackground(ecsWorld,
                                                           Texture2D.FromFile(GabiacSettings.graphics.GraphicsDevice, "Content/SquaredPaper.png"),
                                                           Vector2.Zero,
                                                           Vector2.One);

            var player = entityBuilder.BuildPlayer(ecsWorld, 
                                                   Texture2D.FromFile(GabiacSettings.graphics.GraphicsDevice, "Content/Car2.png"),
                                                   new Vector2(200, 200),
                                                   Vector2.One,
                                                   0,
                                                   5,
                                                   physicWorld);

            var ball = entityBuilder.BuildBall(ecsWorld,
                                               Texture2D.FromFile(GabiacSettings.graphics.GraphicsDevice, "Content/Ball2.png"),
                                               new Vector2(600,600),
                                               Vector2.One,
                                               0.02f,
                                               Vector2.One,
                                               physicWorld);

            var world = entityBuilder.BuildWorld(ecsWorld);
        }
        public override void SetSystems(){
            var spriteBatch = GabiacSettings.spriteBatch;
            var graphics = GabiacSettings.graphics;
            var ecsWorld = GabiacSettings.world;
            var mainRunner = GabiacSettings.mainRunner;
            var physicWorld = GabiacSettings.physicWorld;

            UpdateSystems = new SequentialSystem<GameTime>(
                new InputSystem(ecsWorld, mainRunner),
                new PhysicRotationSystem(ecsWorld, mainRunner),
                new PhysicTranslationSystem(ecsWorld, mainRunner),
                new PhysicWorldUpdatingSystem(ecsWorld, physicWorld),
                new TranformByPhysicBodyUpdatingSystem(ecsWorld, mainRunner),
                new BallMovementSystem(ecsWorld)
            );

            DrawSystems = new SequentialSystem<GameTime>(
                new RocketFireSystem(ecsWorld, spriteBatch, mainRunner),
                new RenderingSystem(spriteBatch, ecsWorld, mainRunner),
                new DebugSystem(graphics, spriteBatch, ecsWorld, physicWorld, mainRunner),
                new TrailSystem(spriteBatch, ecsWorld, mainRunner, physicWorld)
            );
        }
    }
}