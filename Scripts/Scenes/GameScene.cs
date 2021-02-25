using Microsoft.Xna.Framework;
using DefaultEcs.System;
using Gabiac.Scripts.ECS.Systems;
using Gabiac.Scripts.Managers;
using Gabiac.Scripts.Helpers;

namespace Gabiac.Scripts.Scenes
{
    public class GameScene : IScene
    {

#region ECS

        private ISystem<float> updateSystems;
        private ISystem<float> drawSystems;

#endregion

        public override void Update(GameTime _gameTime){
            updateSystems.Update((float)_gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public override void Draw(GameTime _gameTime){
            drawSystems.Update((float)_gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public override void PreLoad(){

        }

        public override void Load(){
            var spriteBatch = GabiacSettings.spriteBatch;
            var graphics = GabiacSettings.graphics;
            var world = GabiacSettings.world;
            var mainRunner = GabiacSettings.mainRunner;
            var physicWorld = GabiacSettings.physicWorld;

            updateSystems = new SequentialSystem<float>(
                new InputSystem(world),
                new RotationSystem(world, mainRunner),
                new MovementSystem(world, mainRunner),
                new PhysicsSystem(world, mainRunner, physicWorld)
            );

            drawSystems = new SequentialSystem<float>(
                new RocketFireSystem(world, spriteBatch, mainRunner),
                new RenderSystem(spriteBatch, world, mainRunner),
                new DebugSystem(graphics, spriteBatch, world, physicWorld, mainRunner),
                new TrailSystem(spriteBatch, world, mainRunner, physicWorld)
            );
        }

        public override void PostLoad(){

        }

        public override void Unload(){
            drawSystems.Dispose();
            updateSystems.Dispose();
        }
    }
}