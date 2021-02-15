using Gabiac.Scripts.Managers;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using MonoGame.Extended;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Modifiers.Containers;
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
        
        public RocketFireSystem(World _world, Vector2 _startPos, SpriteBatch _spriteBatch, IParallelRunner _runner) : base(_world, CreateEntityContainer, null, 0){
            world = _world;
            runner = _runner;
            spriteBatch = _spriteBatch;
            particleTexture = new Texture2D(SceneManager.instance.graphics.GraphicsDevice, 1, 1);
            particleTexture.SetData(new[] { Color.White });
            TextureRegion2D textureRegion = new TextureRegion2D(particleTexture);
            particleEffect = new ParticleEffect(autoTrigger: false)
            {
                Emitters = new List<ParticleEmitter>
                {
                    new ParticleEmitter(textureRegion, 500, TimeSpan.FromSeconds(1), Profile.Spray(-Vector2.UnitX, 2))
                    {
                        Parameters = {
                            Scale = new Range<float>(5, 8),
                            Speed = new Range<float>(7, 19)
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
                    emit.Parameters.Opacity = tak*4;
            }
            Vector2 offset = new Vector2(_renderer.Image.Width * _transform.Scale.X / 2, 0);
            var origin = _transform.Position - offset;
            particleEffect.Position = Vector2.Transform(particleEffect.Position-_transform.Position, Matrix.CreateFromAxisAngle(new Vector3(0,0,1), MathHelper.ToRadians(_transform.Rotation)));
            //particleEffect.Rotation = _transform.Rotation;
            spriteBatch.Draw(particleEffect);
        }

        protected override void PostUpdate(float _state) => world.Optimize(runner, spriteBatch.End);
    }
}