using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using DefaultEcs;
using DefaultEcs.System;
using Gabiac.Scripts.ECS.Components.Input;
using MonoGame.Extended.Input;
using MonoGame.Extended.Input.InputListeners;=

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
        private void EventReader(in InputEvent _inputEvent){
            var currentEventType = _inputEvent.GetType();
            if(currentEventType == typeof(MouseDownEvent) || currentEventType == typeof(MouseUpEvent)){
                var currentEventArgs = (MouseEventArgs)_inputEvent;
                
                if(currentEventArgs.Button == MouseButton.Left){
                    if(currentEventType == typeof(MouseDownEvent)){

                    }
                    else if(currentEventType == typeof(MouseUpEvent)){

                    }
                }
                else if(currentEventArgs.Button == MouseButton.Right){
                    if(currentEventType == typeof(MouseDownEvent)){

                    }
                    else if(currentEventType == typeof(MouseUpEvent)){

                    }
                }
            }
            else if(currentEventType == typeof(KeyboardDownEvent) || currentEventType == typeof(KeyboardUpEvent)){
                var currentEventArgs = (KeyboardEventArgs)_inputEvent;
                
                if(currentEventArgs.Key == Keys.D){
                    if(currentEventType == typeof(MouseDownEvent)){

                    }
                    else if(currentEventType == typeof(MouseUpEvent)){

                    }
                }
                else if(currentEventArgs.Key == Keys.LeftShift){
                    if(currentEventType == typeof(MouseDownEvent)){

                    }
                    else if(currentEventType == typeof(MouseUpEvent)){

                    }
                }
            }
        }

        public void Update(GameTime state){

        }

        public void Dispose() { }
    }
}