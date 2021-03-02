using DefaultEcs;
using DefaultEcs.System;
using Gabiac.Scripts.ECS.Components.Input;
using Gabiac.Scripts.ECS.Components;
using MonoGame.Extended.Input;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input.InputListeners;

namespace Gabiac.Scripts.ECS.Systems
{
    [WhenAdded(typeof(InputHandler))]
    public partial class InputHandlerCreatingSystem : AEntitySetSystem<float>
    {
        private World world;
        
        public InputHandlerCreatingSystem(World _world) : base(_world){
            world = _world;
        }

        
        protected override void Update(float elapsedTime, in Entity _entity){
            var inputHandler = _entity.Get<InputHandler>();
            SetListeners(ref inputHandler);
            SetEvents(ref _inputHandler, _entity);
        }

        private void SetListeners(ref InputHandler _inputHandler){
            _inputHandler.MouseListener = new MouseListener();
            _inputHandler.TouchListener = new TouchListener();
            _inputHandler.GamePadListener = new GamePadListener();
            _inputHandler.KeyboardListener = new KeyboardListener();
        }

        private void SetEvents(ref InputHandler _inputHandler, in Entity _entity){
            var mouseListener =_inputHandler.MouseListener;
            mouseListener.MouseDown += (sender, args)=> _entity.Set(new MouseEvent());
        }
    }
}