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
        public override void SetEntities(){
            var player = GabiacSettings.world.CreateEntity();
            var texture = Texture2D.FromFile(GabiacSettings.graphics.GraphicsDevice, "Content/Car.png");
            var physicWorld = GabiacSettings.physicWorld;

            player.Set(new Transform(0));
            player.Set(new Controller(Vector2.Zero, 5, false));
            var physicBody = new PhysicBody(physicWorld, new Vector2(200,200), texture.Width/2, VelcroPhysics.Dynamics.BodyType.Dynamic);
            player.Set(physicBody);
            player.Set(new Renderer(texture, Color.White));
            player.Set(new Player());
            player.Set(new InputHandler());
            player.Set(new RocketFire());
            player.Set(new RotatePlayer());
            player.Set(new Trail(10, 45, physicWorld, physicBody.Body, VelcroPhysics.Collision.Filtering.Category.None));
            
            var player1 = GabiacSettings.world.CreateEntity();
            player1.Set(new Transform(0));
            player1.Set(new PhysicBody(physicWorld, new Vector2(400,400), texture.Width/2, VelcroPhysics.Dynamics.BodyType.Dynamic));
            player1.Set(new Renderer(texture, Color.Red));

            var ball = GabiacSettings.world.CreateEntity();
            var ballTexture = Texture2D.FromFile(GabiacSettings.graphics.GraphicsDevice, "Content/Ball.png");
            var ballTransform = new Transform(0);
            ball.Set(ballTransform);
            ball.Set(new Renderer(ballTexture, Color.White));
            var ballBody = new PhysicBody(physicWorld, new Vector2(700,700), ballTexture.Width/2, VelcroPhysics.Dynamics.BodyType.Dynamic, _mass:.0001f);
            var ballComp = new Ball(Vector2.Zero, .02f);
            ballBody.Body.OnCollision += (fixtA, fixtB, contact)=>{
                Vector2 norm;
                FixedArray2<Vector2> points;
                contact.GetWorldManifold(out norm, out points);
                ball.Set(new BallReflection(norm));
            };
            ball.Set(ballBody);
            ball.Set(ballComp);

            var mainMenu = GabiacSettings.world.CreateEntity();
        }

        public override void SetSystems(){
            var spriteBatch = GabiacSettings.spriteBatch;
            var graphics = GabiacSettings.graphics;
            var world = GabiacSettings.world;
            var mainRunner = GabiacSettings.mainRunner;
            var physicWorld = GabiacSettings.physicWorld;

            UpdateSystems = new SequentialSystem<GameTime>(
                new InputSystem(world),
                new RotationSystem(world, mainRunner),
                new MovementSystem(world, mainRunner),
                new PhysicsSystem(world, mainRunner, physicWorld),
                new BallSystem(world),
                new BallReflectionSystem(world)
            );

            DrawSystems = new SequentialSystem<GameTime>(
                new RocketFireSystem(world, spriteBatch, mainRunner),
                new RenderSystem(spriteBatch, world, mainRunner),
                new DebugSystem(graphics, spriteBatch, world, physicWorld, mainRunner),
                new TrailSystem(spriteBatch, world, mainRunner, physicWorld)
            );
        }
    }
}