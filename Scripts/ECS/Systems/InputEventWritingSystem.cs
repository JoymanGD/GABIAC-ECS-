using Gabiac.Scripts.ECS.Components.Input;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using DefaultEcs;
using DefaultEcs.System;
using Gabiac.Scripts.ECS.Components;
using MonoGame.Extended.Input;
using MonoGame.Extended.Input.InputListeners;
using Microsoft.Xna.Framework.Input.Touch;
using Gabiac.Scripts.Helpers.Bindings;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(Controller))]
    [With(typeof(Transform))]
    public partial class InputEventWritingSystem : AEntitySetSystem<GameTime>
    {
        World world;
        Inputs currentInput = Inputs.Keyboard;
        MouseStateExtended mouseState;
        KeyboardStateExtended keyboardState;
        TouchCollection touchState;
        MouseListener mouseListener;
        KeyboardListener keyboardListener;
        GamePadListener gamePadListener;
        TouchListener touchListener;

        public InputEventWritingSystem(World _world) : base(_world){
            world = _world;
            //SetListeners();
            //SetEvents();
        }

        void SetListeners(){
            mouseListener = new MouseListener();
            keyboardListener = new KeyboardListener();
            touchListener = new TouchListener();
            gamePadListener = new GamePadListener();
        }

        void SetEvents(){
            mouseListener.MouseDown += (sender, args)=>{
                world.Publish(new MouseDownEvent(args));
            };
            mouseListener.MouseUp += (sender, args)=>{
                world.Publish(new MouseUpEvent(args));
            };
            mouseListener.MouseMoved += (sender, args) => {
                world.Publish(new MouseMoveEvent(args));
            };
            keyboardListener.KeyPressed += (sender, args)=>{
                world.Publish(new KeyboardDownEvent(args));
            };
            keyboardListener.KeyReleased += (sender, args)=>{
                world.Publish(new KeyboardUpEvent(args));
            };
            gamePadListener.ButtonDown += (sender, args)=>{
                world.Publish(new GamepadDownEvent(args));
            };
            gamePadListener.ButtonUp += (sender, args)=>{
                world.Publish(new GamepadUpEvent(args));
            };
        }

        void UpdateListeners(GameTime _gameTime){
            mouseListener.Update(_gameTime);
            keyboardListener.Update(_gameTime);
            gamePadListener.Update(_gameTime);
            touchListener.Update(_gameTime);
        }

        [Update]
        protected void Update(ref Controller _controller, in Transform _transform, in Entity _entity, GameTime _gameTime){
            CheckForActiveInput();
            ControlInput(_entity);
            SetDirection(_transform, ref _controller);
            //UpdateListeners(_gameTime);
        }

        void SetDirection(Transform _transform, ref Controller _controller)
        {
            switch (currentInput)
            {
                case Inputs.Mouse:
                    Vector2 mousePos = mouseState.Position.ToVector2();
                    var mouseDir = mousePos - _transform.Position;
                    mouseDir.Normalize();
                    _controller.SetDirection(mouseDir);
                    break;
                case Inputs.Keyboard:
                    Vector2 keyboardVector = new Vector2(-Convert.ToInt32(keyboardState.IsKeyDown(Keys.A)) + Convert.ToInt32(keyboardState.IsKeyDown(Keys.D)), -Convert.ToInt32(keyboardState.IsKeyDown(Keys.W)) + Convert.ToInt32(keyboardState.IsKeyDown(Keys.S)));
                    var keyboardDir = keyboardVector;
                    _controller.SetDirection(Vector2.Lerp(_controller.Direction, keyboardDir, .1f));
                    break;
                case Inputs.Touch:
                    Vector2 touchPos = touchState[0].Position;
                    var touchDir = touchPos - _transform.Position;
                    touchDir.Normalize();
                    _controller.SetDirection(touchDir);
                    break;
                default:
                    break;
            }
        }

        void ControlInput(Entity _entity)
        {
            switch (currentInput)
            {
                case Inputs.Mouse:
                    if(mouseState.WasButtonJustDown(MouseBinding.Move)){
                        _entity.Set<TranslationComponent>();
                    }
                    else if(mouseState.WasButtonJustUp(MouseBinding.Move)){
                        _entity.Remove<TranslationComponent>();
                    }

                    if(mouseState.WasButtonJustDown(MouseBinding.DoTheTrail)){
                        _entity.Set<DoTheTrail>();
                    }
                    else if(mouseState.WasButtonJustUp(MouseBinding.DoTheTrail)){
                        _entity.Remove<DoTheTrail>();
                    }

                    break;
                case Inputs.Keyboard:

                    bool movementKeysPressed = keyboardState.WasKeyJustUp(KeyboardBinding.MoveRight) || keyboardState.WasKeyJustUp(KeyboardBinding.MoveDown) || keyboardState.WasKeyJustUp(KeyboardBinding.MoveLeft) || keyboardState.WasKeyJustUp(KeyboardBinding.MoveUp);
                    bool movementKeysUnpressed = keyboardState.WasKeyJustUp(KeyboardBinding.MoveRight) || keyboardState.WasKeyJustUp(KeyboardBinding.MoveDown) || keyboardState.WasKeyJustUp(KeyboardBinding.MoveLeft) || keyboardState.WasKeyJustUp(KeyboardBinding.MoveUp);
                    bool movementKeysHold = keyboardState.IsKeyDown(KeyboardBinding.MoveRight) || keyboardState.IsKeyDown(KeyboardBinding.MoveDown) || keyboardState.IsKeyDown(KeyboardBinding.MoveLeft) || keyboardState.IsKeyDown(KeyboardBinding.MoveUp);

                    if(movementKeysPressed){
                        _entity.Set<TranslationComponent>();
                    }
                    else if(movementKeysUnpressed && !movementKeysHold){
                        _entity.Remove<TranslationComponent>();
                    }

                    if(keyboardState.WasKeyJustUp(KeyboardBinding.DoTheTrail)){
                        _entity.Set<DoTheTrail>();
                    }
                    else if(keyboardState.WasKeyJustDown(KeyboardBinding.DoTheTrail)){
                        _entity.Remove<DoTheTrail>();
                    }
                    break;
                default:
                    break;
            }
        }

        void CheckForActiveInput(){
            mouseState = MouseExtended.GetState();
            keyboardState = KeyboardExtended.GetState();
            touchState = TouchPanel.GetState(); 

            if(mouseState.PositionChanged || mouseState.WasButtonJustDown(MouseBinding.Move) || mouseState.WasButtonJustDown(MouseBinding.DoTheTrail)){
                if(currentInput!=Inputs.Mouse)
                    currentInput = Inputs.Mouse;
            }

            if(keyboardState.GetPressedKeys().Length > 0 || keyboardState.WasAnyKeyJustDown()){
                if(currentInput!=Inputs.Keyboard)
                    currentInput = Inputs.Keyboard;
            }
            
            if(touchState.Count > 0){
                if(currentInput!=Inputs.Touch)
                    currentInput = Inputs.Touch;
            }
        }

        public enum Inputs
        {
            Mouse, Keyboard, Touch, Gamepad
        }
    }
}