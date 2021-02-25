using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using MonoGame.Extended.Input;

namespace Gabiac.Scripts.ECS.Systems.Input
{
    [With(typeof(Controller))]
    [With(typeof(PhysicBody))]
    [With(typeof(KeyboardInput))]
    public partial class KeyboardInputSystem : AEntitySetSystem<float>, IInputSystem
    {
        IParallelRunner runner;
        World world;

        public KeyboardInputSystem(World _world, IParallelRunner _runner) : base(_world, _runner){
            world = _world;
            runner = _runner;
        }

        [Update]
        protected void Update(ref Controller _controller, in PhysicBody _physicBody, in Entity _entity){
            ControlInput(_entity);
            SetDirection(_physicBody, ref _controller);
        }

        public void SetDirection(PhysicBody _physicBody, ref Controller _controller)
        {
            var keyboardState = KeyboardExtended.GetState();
            Vector2 keyboardVector = new Vector2(-Convert.ToInt32(keyboardState.IsKeyDown(Keys.A)) + Convert.ToInt32(keyboardState.IsKeyDown(Keys.D)), -Convert.ToInt32(keyboardState.IsKeyDown(Keys.W)) + Convert.ToInt32(keyboardState.IsKeyDown(Keys.S)));
            var dir = keyboardVector;
            _controller.SetDirection(dir);
        }

        public void ControlInput(Entity _entity){
            var keyboardState = KeyboardExtended.GetState();
            bool movementKeysPressed = keyboardState.WasKeyJustDown(Keys.D) || keyboardState.WasKeyJustDown(Keys.S) || keyboardState.WasKeyJustDown(Keys.A) || keyboardState.WasKeyJustDown(Keys.W);
            bool movementKeysUnpressed = keyboardState.WasKeyJustUp(Keys.D) || keyboardState.WasKeyJustUp(Keys.S) || keyboardState.WasKeyJustUp(Keys.A) || keyboardState.WasKeyJustUp(Keys.W);
            bool movementKeysHold = keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.W);
            if(movementKeysPressed){
                _entity.Set<MovePlayer>();
            }
            else if(movementKeysUnpressed && !movementKeysHold){
                _entity.Remove<MovePlayer>();
            }

            if(keyboardState.WasKeyJustDown(Keys.LeftShift)){
                _entity.Set<DoTheTrail>();
            }
            else if(keyboardState.WasKeyJustUp(Keys.LeftShift)){
                _entity.Remove<DoTheTrail>();
            }
        }
    }
}