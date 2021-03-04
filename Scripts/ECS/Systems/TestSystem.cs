using Microsoft.Xna.Framework;
using DefaultEcs;
using DefaultEcs.System;
using Gabiac.Scripts.ECS.Components.Input;
using MonoGame.Extended.Input;
using System;

namespace Gabiac.Scripts.ECS.Systems
{
    public sealed class TestSystem : ISystem<GameTime>
    {
        private World world;
        public bool IsEnabled { get; set; } = true;
        
        public TestSystem(World _world){
            world = _world;
            world.Subscribe(this);
        }

        [Subscribe]
        private void On(in MouseDownEvent _inputEvent){
            var currentEvent = (MouseEvent)_inputEvent;
        }

        public void Update(GameTime state){
            var mouseState = MouseExtended.GetState();
            if(mouseState.WasButtonJustDown(MouseButton.Left)){
            }
        }

        public void Dispose() { }
    }
}