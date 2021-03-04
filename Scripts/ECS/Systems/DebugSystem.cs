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
    [With(typeof(Player))]
    [With(typeof(PhysicBody))]
    [With(typeof(Controller))]
    public partial class DebugSystem : AEntitySetSystem<GameTime>
    {
        private SpriteBatch spriteBatch;
        private IParallelRunner runner;
        private World world;
        private FontSystem fontSystem;
        private SpriteFontBase font;
        private GraphicsDeviceManager graphics;
        private VelcroPhysics.Dynamics.World physicWorld;
        
        public DebugSystem(GraphicsDeviceManager _graphics, SpriteBatch _spriteBatch, World _world, VelcroPhysics.Dynamics.World _physicWorld, IParallelRunner _runner) : base(_world, CreateEntityContainer, null, 0){
            graphics = _graphics;
            spriteBatch = _spriteBatch;
            runner = _runner;
            world = _world;
            physicWorld = _physicWorld;
            fontSystem = new FontSystem(graphics.GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            fontSystem.AddFont(File.ReadAllBytes(@"Content/Fonts/NotoSansJP-Light.otf"));
            font = fontSystem.GetFont(30);
        }

        protected override void PreUpdate(GameTime _state) => spriteBatch.Begin();

        [Update]
        private void Update(in PhysicBody _physicBody, in Controller _controller, in Trail _trail, GameTime _gameTime){
            var elapsedTime = (decimal)_gameTime.ElapsedGameTime.TotalSeconds;
            var position = ConvertUnits.ToDisplayUnits(_physicBody.Body.Position);
            var velocity = ConvertUnits.ToDisplayUnits(_physicBody.Body.LinearVelocity);
            spriteBatch.DrawString(font, 
                                        
                                        "FPS: " +  Math.Round(1/(elapsedTime)) +
                                        "\n" +
                                        "Velocity: " + velocity.ToString() +
                                        "\n" +
                                        "Position: " + position.ToString() +
                                        "\n" +
                                        "MousePos: " + Mouse.GetState().Position.ToString()

            , new Vector2(40, 40), Color.White);
            
            //trail points
            foreach (var trailPoint in _trail.TrailPoints)
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
            spriteBatch.DrawLine(position, position + _controller.Direction * 80, Color.Green, 2); //mouse direction
        }

        protected override void PostUpdate(GameTime _state) => world.Optimize(runner, spriteBatch.End);
    }
}   