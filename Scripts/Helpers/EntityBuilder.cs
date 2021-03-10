using VelcroPhysics.Utilities;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Shared.Optimization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Gabiac.Scripts.ECS.Components;
using Gabiac.Scripts.ECS.Components.Input;
using DefaultEcs;

namespace Gabiac.Scripts.Helpers
{
    public class EntityBuilder
    {
        public Entity BuildPlayer(DefaultEcs.World _world, Texture2D _image, Vector2 _position, Vector2 _scale, float _rotation, float _speed, VelcroPhysics.Dynamics.World _physicWorld){
            var newPlayer = _world.CreateEntity();
            
            //Define components
            var transform = new Transform(_position, _scale, _rotation);
            var physicBody = new PhysicBody(_physicWorld, _position, _image.Width/2, BodyType.Dynamic);
            var renderer = new Renderer(_image, Color.White, 1);
            var trail = new Trail(10, 45, _physicWorld, physicBody.Body, VelcroPhysics.Collision.Filtering.Category.None);
            var controller = new Controller(Vector2.Zero, _speed, false);

            //Set components
            newPlayer.Set(transform);
            newPlayer.Set(physicBody);
            newPlayer.Set(renderer);
            newPlayer.Set(new InputComponent());
            newPlayer.Set(trail);
            newPlayer.Set(new RotationComponent(Vector2.Zero));
            newPlayer.Set(new Player());
            newPlayer.Set(controller);
            
            return newPlayer;
        }

        public Entity BuildBall(DefaultEcs.World _world, Texture2D _image, Vector2 _position, Vector2 _scale, float _startSpeed, Vector2 _startDirection, VelcroPhysics.Dynamics.World _physicWorld){
            var newBall = _world.CreateEntity();

            //Define components
            var renderer = new Renderer(_image, Color.White, 1);
            var transform = new Transform(_position, _scale, 0);
            var physicBody = new PhysicBody(_physicWorld, _position, _image.Width/2, VelcroPhysics.Dynamics.BodyType.Dynamic, _mass:.0001f);
            physicBody.Body.OnCollision += (fixtA, fixtB, contact)=>{
                Vector2 norm;
                FixedArray2<Vector2> points;
                contact.GetWorldManifold(out norm, out points);
                newBall.Set(new BallReflection(norm));
            };
            var ballComponent = new Ball(_startDirection, _startSpeed); //(Vector2.One, 0.02f)
            
            //Set components
            newBall.Set(renderer);
            newBall.Set(physicBody);
            newBall.Set(ballComponent);

            return newBall;
        }

        public Entity BuildBackground(DefaultEcs.World _world, Texture2D _image, Vector2 _position, Vector2 _scale){
            var newBackground = _world.CreateEntity();

            //Define components
            var renderer = new Renderer(_image, Color.White, 0);
            var transform = new Transform(_position, _scale, 0);
            
            //Set components
            newBackground.Set(renderer);
            newBackground.Set(transform);
            
            return newBackground;
        }
    }
}