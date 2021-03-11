using Microsoft.Xna.Framework;
using DefaultEcs;
using MonoGame.Extended.Input;
using Gabiac.Scripts.Helpers.Bindings;
using Gabiac.Scripts.ECS.Components;
using System;
using Microsoft.Xna.Framework.Input;

namespace Gabiac.Scripts.ECS.Systems.Input
{
    public class KeyboardInputSystem:IInputSystem
    {
        public void Update(Entity _entity){
            var keyboardState = KeyboardExtended.GetState();
            bool movementKeysPressed = keyboardState.WasKeyJustUp(KeyboardBinding.MoveRight) || keyboardState.WasKeyJustUp(KeyboardBinding.MoveDown) || keyboardState.WasKeyJustUp(KeyboardBinding.MoveLeft) || keyboardState.WasKeyJustUp(KeyboardBinding.MoveUp);
            bool movementKeysUnpressed = keyboardState.WasKeyJustDown(KeyboardBinding.MoveRight) || keyboardState.WasKeyJustDown(KeyboardBinding.MoveDown) || keyboardState.WasKeyJustDown(KeyboardBinding.MoveLeft) || keyboardState.WasKeyJustDown(KeyboardBinding.MoveUp);
            bool movementKeysHold = keyboardState.IsKeyDown(KeyboardBinding.MoveRight) || keyboardState.IsKeyDown(KeyboardBinding.MoveDown) || keyboardState.IsKeyDown(KeyboardBinding.MoveLeft) || keyboardState.IsKeyDown(KeyboardBinding.MoveUp);

            if(movementKeysPressed){
                _entity.Set(new TranslationComponent(7));
            }
            
            if(movementKeysUnpressed && !movementKeysHold){
                _entity.Remove<TranslationComponent>();
            }

            if(keyboardState.WasKeyJustUp(KeyboardBinding.DoTheTrail)){
                _entity.Set<DoTheTrail>();
            }
            else if(keyboardState.WasKeyJustDown(KeyboardBinding.DoTheTrail)){
                _entity.Remove<DoTheTrail>();
            }

            if(_entity.Has<RotationComponent>() && _entity.Has<TranslationComponent>() && movementKeysHold){
                ref RotationComponent rotationComponent = ref _entity.Get<RotationComponent>();
                Vector2 keyboardVector = new Vector2(-Convert.ToInt32(keyboardState.IsKeyDown(Keys.A)) + Convert.ToInt32(keyboardState.IsKeyDown(Keys.D)), -Convert.ToInt32(keyboardState.IsKeyDown(Keys.W)) + Convert.ToInt32(keyboardState.IsKeyDown(Keys.S)));
                rotationComponent.Direction = Vector2.Lerp(rotationComponent.Direction, keyboardVector, .02f);
            }
        }
    }
}