using Microsoft.Xna.Framework;
using DefaultEcs.System;
using Gabiac.Scripts.ECS.Systems;
using Gabiac.Scripts.ECS.Components;
using Microsoft.Xna.Framework.Graphics;
using Gabiac.Scripts.Helpers;
using VelcroPhysics.Shared.Optimization;

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
            var world = GabiacSettings.world;
            var physicWorld = GabiacSettings.physicWorld;

            var entityBuilder = new EntityBuilder();

            var background = entityBuilder.BuildBackground(world,
                                                           Texture2D.FromFile(GabiacSettings.graphics.GraphicsDevice, "Content/SquaredPaper.png"),
                                                           Vector2.Zero,
                                                           Vector2.One);

            var player = entityBuilder.BuildPlayer(world, 
                                                   Texture2D.FromFile(GabiacSettings.graphics.GraphicsDevice, "Content/Car2.png"),
                                                   new Vector2(200, 200),
                                                   Vector2.One,
                                                   0,
                                                   5,
                                                   physicWorld);

            var ball = entityBuilder.BuildBall(world,
                                               Texture2D.FromFile(GabiacSettings.graphics.GraphicsDevice, "Content/Ball2.png"),
                                               new Vector2(600,600),
                                               Vector2.One,
                                               0.02f,
                                               Vector2.One,
                                               physicWorld);
        }
        public override void SetSystems(){
            var spriteBatch = GabiacSettings.spriteBatch;
            var graphics = GabiacSettings.graphics;
            var world = GabiacSettings.world;
            var mainRunner = GabiacSettings.mainRunner;
            var physicWorld = GabiacSettings.physicWorld;

            UpdateSystems = new SequentialSystem<GameTime>(
                new InputEventWritingSystem(world),
                new InputEventReadingSystem(world),
                new RotationByControllerSystem(world, mainRunner),
                new TranslationByControllerSystem(world, mainRunner),
                new PhysicWorldUpdatingSystem(world, mainRunner, physicWorld),
                new BallMovementSystem(world),
                new BallReflectionSystem(world)
            );

            DrawSystems = new SequentialSystem<GameTime>(
                new RocketFireSystem(world, spriteBatch, mainRunner),
                new RenderingSystem(spriteBatch, world, mainRunner),
                new DebugSystem(graphics, spriteBatch, world, physicWorld, mainRunner),
                new TrailSystem(spriteBatch, world, mainRunner, physicWorld)
            );
        }
    }
}