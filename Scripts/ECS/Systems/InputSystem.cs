using Gabiac.Scripts.ECS.Components.Input;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using DefaultEcs;
using DefaultEcs.System;
using Gabiac.Scripts.ECS.Components;
using Gabiac.Scripts.ECS.Components.UI;
using MonoGame.Extended.Input;
using MonoGame.Extended.Input.InputListeners;
using Microsoft.Xna.Framework.Input.Touch;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(Controller))]
    [With(typeof(PhysicBody))]
    public partial class InputSystem : AEntitySetSystem<GameTime>
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

        public InputSystem(World _world) : base(_world){
            world = _world;
            SetListeners();
            SetEvents();
        }

        void SetListeners(){
            mouseListener = new MouseListener();
            keyboardListener = new KeyboardListener();
            touchListener = new TouchListener();
            gamePadListener = new GamePadListener();
        }

        void SetEvents(){
            mouseListener.MouseDown += (sender, args)=>{
                world.Publish((InputEvent)new MouseDownEvent(args));
            };
            mouseListener.MouseUp += (sender, args)=>{
                world.Publish((InputEvent)new MouseUpEvent(args));
            };
        }

        void UpdateListeners(GameTime _gameTime){
            mouseListener.Update(_gameTime);
            keyboardListener.Update(_gameTime);
            gamePadListener.Update(_gameTime);
            touchListener.Update(_gameTime);
        }

        [Update]
        protected void Update(ref Controller _controller, in PhysicBody _physicBody, in Entity _entity, GameTime _gameTime){
            CheckForActiveInput();
            ControlInput(_entity);
            SetDirection(_physicBody, ref _controller);
            UpdateListeners(_gameTime);
        }

        void SetDirection(PhysicBody _physicBody, ref Controller _controller)
        {
            switch (currentInput)
            {
                case Inputs.Mouse:
                    Vector2 mousePos = mouseState.Position.ToVector2();
                    var mouseDir = mousePos - _physicBody.Position();
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
                    var touchDir = touchPos - _physicBody.Position();
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
                    if(mouseState.WasButtonJustDown(MouseButton.Left)){
                        _entity.Set<MovePlayer>();
                        //world.Publish(new MouseEvent(MouseButton.Left));
                    }
                    else if(mouseState.WasButtonJustUp(MouseButton.Left)){
                        _entity.Remove<MovePlayer>();
                    }

                    if(mouseState.WasButtonJustDown(MouseButton.Right)){
                        _entity.Set<DoTheTrail>();
                    }
                    else if(mouseState.WasButtonJustUp(MouseButton.Right)){
                        _entity.Remove<DoTheTrail>();
                    }

                    break;
                case Inputs.Keyboard:

                    bool movementKeysPressed = keyboardState.WasKeyJustUp(Keys.D) || keyboardState.WasKeyJustUp(Keys.S) || keyboardState.WasKeyJustUp(Keys.A) || keyboardState.WasKeyJustUp(Keys.W);
                    bool movementKeysUnpressed = keyboardState.WasKeyJustDown(Keys.D) || keyboardState.WasKeyJustDown(Keys.S) || keyboardState.WasKeyJustDown(Keys.A) || keyboardState.WasKeyJustDown(Keys.W);
                    bool movementKeysHold = keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.W);

                    if(movementKeysPressed){
                        _entity.Set<MovePlayer>();
                    }
                    else if(movementKeysUnpressed && !movementKeysHold){
                        _entity.Remove<MovePlayer>();
                    }

                    if(keyboardState.WasKeyJustUp(Keys.LeftShift)){
                        _entity.Set<DoTheTrail>();
                    }
                    else if(keyboardState.WasKeyJustDown(Keys.LeftShift)){
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

            if(mouseState.PositionChanged || mouseState.WasButtonJustDown(MouseButton.Left) || mouseState.WasButtonJustDown(MouseButton.Right)){
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