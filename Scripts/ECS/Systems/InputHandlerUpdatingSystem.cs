using DefaultEcs;
using DefaultEcs.System;
using Gabiac.Scripts.ECS.Components;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(InputHandler))]
    public partial class InputHandlerUpdatingSystem : AEntitySetSystem<GameTime>
    {
        private World world;
        
        public InputHandlerUpdatingSystem(World _world) : base(_world){
            world = _world;
        }
        
        [Update]
        private void Update(ref InputHandler _inputHandler, GameTime _gameTime){
            _inputHandler.GamePadListener.Update(_gameTime);
            _inputHandler.TouchListener.Update(_gameTime);
            _inputHandler.MouseListener.Update(_gameTime);
            _inputHandler.KeyboardListener.Update(_gameTime);
        }
    }
}