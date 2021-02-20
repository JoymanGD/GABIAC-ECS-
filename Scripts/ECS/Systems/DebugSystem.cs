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
    [With(typeof(Transform))]
    [With(typeof(Player))]
    [With(typeof(PhysicBody))]
    [With(typeof(Controller))]
    public partial class DebugSystem : AEntitySetSystem<float>
    {
        private SpriteBatch spriteBatch;
        private IParallelRunner runner;
        private World world;
        private FontSystem fontSystem;
        private SpriteFontBase font;
        private GraphicsDeviceManager graphics;
        
        public DebugSystem(GraphicsDeviceManager _graphics, SpriteBatch _spriteBatch, World _world, IParallelRunner _runner) : base(_world, CreateEntityContainer, null, 0){
            graphics = _graphics;
            spriteBatch = _spriteBatch;
            runner = _runner;
            world = _world;
            fontSystem = new FontSystem(graphics.GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            fontSystem.AddFont(File.ReadAllBytes(@"Content/Fonts/NotoSansJP-Light.otf"));
            font = fontSystem.GetFont(30);
        }

        protected override void PreUpdate(float _state) => spriteBatch.Begin();

        [Update]
        private void Update(in Transform _transform, in PhysicBody _physicBody, in Controller _controller, float elapsedTime){
            var linearVelocity = ConvertUnits.ToDisplayUnits(_physicBody.Body.LinearVelocity);
            var angularVelocity = ConvertUnits.ToDisplayUnits(_physicBody.Body.AngularVelocity);
            spriteBatch.DrawString(font, 
                                        
                                        "FPS: " + 1/(elapsedTime/100) + 
                                        "\n" +
                                        "Velocity: " + _transform.DeltaPosition.ToString() + 
                                        "\n" +
                                        "Position: " + _transform.Position.ToString() + 
                                        "\n" +
                                        "Angular velocity: " + angularVelocity + 
                                        "\n" +
                                        "Linear velocity: " + linearVelocity + 
                                        "\n" +
                                        "Inertia: " + ConvertUnits.ToDisplayUnits(_physicBody.Body.Inertia) + 
                                        "\n" +
                                        "MousePos: " + Mouse.GetState().Position.ToString()

            , new Vector2(40, 40), Color.White);

            linearVelocity.Normalize();

            spriteBatch.DrawLine(_transform.Position, _transform.Position + linearVelocity * 80, Color.Red, 2);
            spriteBatch.DrawLine(_transform.Position, _transform.Position + _controller.LookDirection * 80, Color.Green, 2);
        }

        protected override void PostUpdate(float _state) => world.Optimize(runner, spriteBatch.End);
    }
}   