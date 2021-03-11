using System;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using FontStashSharp;
using VelcroPhysics.Utilities;
using System.IO;
using Microsoft.Xna.Framework.Input;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(Debug))]
    public partial class DebugSystem : AEntitySetSystem<GameTime>
    {
        private SpriteBatch spriteBatch;
        private IParallelRunner runner;
        private World world;
        private FontSystem fontSystem;
        private SpriteFontBase font;
        private GraphicsDeviceManager graphics;
        private VelcroPhysics.Dynamics.World physicWorld;
        private EntitySet targets;
        
        public DebugSystem(GraphicsDeviceManager _graphics, SpriteBatch _spriteBatch, World _world, VelcroPhysics.Dynamics.World _physicWorld, IParallelRunner _runner) : base(_world, CreateEntityContainer, null, 0){
            graphics = _graphics;
            spriteBatch = _spriteBatch;
            runner = _runner;
            world = _world;
            physicWorld = _physicWorld;
            fontSystem = new FontSystem(graphics.GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            fontSystem.AddFont(File.ReadAllBytes(@"Content/Fonts/NotoSansJP-Light.otf"));
            font = fontSystem.GetFont(30);
            targets = world.GetEntities().With<Player>().With<PhysicBody>().With<RotationComponent>().AsSet();
        }

        protected override void PreUpdate(GameTime _state) => spriteBatch.Begin();

        [Update]
        private void Update(GameTime _gameTime){
            DrawFPS(_gameTime);
            DrawCommonInformation();
        }

        private void DrawFPS(GameTime _gameTime){
            var elapsedTime = (decimal)_gameTime.ElapsedGameTime.TotalSeconds;
            var fps = Math.Round(1/(elapsedTime));
            var fpsColor = fps < 55 ? Color.Red : Color.Green;
            spriteBatch.DrawString(font, "FPS: " + fps, new Vector2(40, 40), fpsColor);
        }

        private void DrawCommonInformation(){
            
            var targetEntities = targets.GetEntities();

            Vector2 commonDrawPosition = new Vector2(40, 60);

            for (var i = 0; i < targetEntities.Length; i++)
            {
                var physicBody = targetEntities[i].Get<PhysicBody>();
                var trail = targetEntities[i].Get<Trail>();
                var rotationComponent = targetEntities[i].Get<RotationComponent>();

                var position = ConvertUnits.ToDisplayUnits(physicBody.Body.Position);
                var velocity = ConvertUnits.ToDisplayUnits(physicBody.Body.LinearVelocity);

                spriteBatch.DrawString(font, 
                                            
                                            "\n" +
                                            "Velocity: " + velocity.ToString() +
                                            "\n" +
                                            "Position: " + position.ToString() +
                                            "\n" +
                                            "MousePos: " + Mouse.GetState().Position.ToString()

                , new Vector2(40, 40), Color.Black);
            
                //trail points
                foreach (var trailPoint in trail.TrailPoints)
                {
                    spriteBatch.DrawCircle(ConvertUnits.ToDisplayUnits(trailPoint.Position), 20, 32, Color.Green);
                }

                //joints
                foreach (var j in physicWorld.JointList)
                {
                    spriteBatch.DrawLine(ConvertUnits.ToDisplayUnits(j.BodyA.Position), ConvertUnits.ToDisplayUnits(j.BodyB.Position), Color.Yellow, 1);
                }

                //directions
                spriteBatch.DrawLine(position, position + velocity.NormalizedCopy() * 80, Color.Red, 2); //physic force
                spriteBatch.DrawLine(position, position + rotationComponent.Direction * 80, Color.Green, 2); //mouse direction

                commonDrawPosition += new Vector2(100, 0);
            }
        }

        protected override void PostUpdate(GameTime _state) => world.Optimize(runner, spriteBatch.End);
    }
}   