using DefaultEcs;
using DefaultEcs.System;
using Gabiac.Scripts.ECS.Components.Input;
using Gabiac.Scripts.ECS.Components;
using MonoGame.Extended.Input;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Input.InputListeners;

namespace Gabiac.Scripts.ECS.Systems
{
    [WhenAdded(typeof(InputHandler))]
    public partial class InputHandlerCreatingSystem : AEntitySetSystem<GameTime>
    {
        private World world;
        
        public InputHandlerCreatingSystem(World _world) : base(_world){
            world = _world;
        }
        
        [Update]
        private void Update(in Entity _entity, [Added] ref InputHandler _inputHandler){
            SetListeners(ref _inputHandler);
            SetEvents(ref _inputHandler, _entity);
        }

        private void SetListeners(ref InputHandler _inputHandler){
            _inputHandler.MouseListener = new MouseListener();
            _inputHandler.TouchListener = new TouchListener();
            _inputHandler.GamePadListener = new GamePadListener();
            _inputHandler.KeyboardListener = new KeyboardListener();
        }

        private void SetEvents(ref InputHandler _inputHandler, in Entity _entity){
            var entities = world.GetEntities().WhenAdded<InputHandler>().AsEnumerable();
            var mouseListener =_inputHandler.MouseListener;
            var keyboardListener = _inputHandler.KeyboardListener;
            var touchListener = _inputHandler.TouchListener;
            var gamepadListener = _inputHandler.GamePadListener;
            foreach (var entity in entities)
            {
                mouseListener.MouseDown += (sender, args)=>{
                    entity.Set(new MouseEvent(args.Button));
                };
                keyboardListener.KeyPressed += (sender, args)=> entity.Set(new KeyboardEvent(args.Key));
                touchListener.TouchStarted += (sender, args)=> entity.Set(new TouchEvent(args.RawTouchLocation));
                gamepadListener.ButtonDown += (sender, args)=> entity.Set(new GamepadEvent(args.Button));
            }
        }
    }
}