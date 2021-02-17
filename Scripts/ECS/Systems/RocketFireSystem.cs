using Gabiac.Scripts.Helpers;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using MonoGame.Extended;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using MonoGame.Extended.Particles.Profiles;
using MonoGame.Extended.TextureAtlases;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(RocketFire))]
    public partial class RocketFireSystem : AEntitySetSystem<float>
    {
        private IParallelRunner runner;
        private World world;
        public ParticleEffect particleEffect;
        private SpriteBatch spriteBatch;
        private Texture2D particleTexture;
        
        public RocketFireSystem(World _world, SpriteBatch _spriteBatch, IParallelRunner _runner) : base(_world, CreateEntityContainer, null, 0){
            world = _world;
            runner = _runner;
            spriteBatch = _spriteBatch;
            particleTexture = new Texture2D(GabiacSettings.graphics.GraphicsDevice, 1, 1);
            particleTexture.SetData(new[] { Color.White });
            TextureRegion2D textureRegion = new TextureRegion2D(particleTexture);
            particleEffect = new ParticleEffect(autoTrigger: false)
            {
                Emitters = new List<ParticleEmitter>
                {
                    new ParticleEmitter(textureRegion, 500, TimeSpan.FromSeconds(.5), Profile.Circle(12, Profile.CircleRadiation.Out))
                    {
                        Parameters = {
                            Scale = new Range<float>(3, 8),
                            Quantity = 20,
                            Speed = new Range<float>(6, 9)
                        },

                        Modifiers =
                        {
                            new VelocityModifier
                            {
                                Interpolators =
                                {
                                    new ColorInterpolator
                                    {
                                        StartValue = new HslColor(.48f, 1f, .5f),
                                        EndValue = new HslColor(.58f, 1f, .5f)
                                    }
                                }
                            },
                            new RotationModifier {RotationRate = -2.1f}
                        }
                    }
                }
            };
        }

        protected override void PreUpdate(float _state) => spriteBatch.Begin(blendState: BlendState.AlphaBlend);

        [Update]
        private void Update(in Renderer _renderer, in Transform _transform, float elapsedTime){
            particleEffect.Update(elapsedTime/100);
            foreach(var emit in particleEffect.Emitters){
                var tak = (int)_transform.GetDeltaPosition();
                emit.Parameters.Opacity = tak*10;
            }
            Vector2 offset = new Vector2(_renderer.Image.Width * _transform.Scale.X / 2, 0);
            var origin = _transform.Position - offset;
            particleEffect.Position = Rotate(_transform.Rotation, origin, _transform.Position);
            spriteBatch.Draw(particleEffect);
        }

        protected override void PostUpdate(float _state) => world.Optimize(runner, spriteBatch.End);

        public Vector2 Rotate(float angle, Vector2 currentPos, Vector2 centre)
        {
            double distance = Math.Sqrt(Math.Pow(currentPos.X-centre.X, 2) + Math.Pow(currentPos.Y-centre.Y, 2));
            return centre - new Vector2((float)(distance * Math.Cos(angle)), (float)(distance * Math.Sin(angle)));
        }
    }
}