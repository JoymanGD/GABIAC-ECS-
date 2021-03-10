using System;
using VelcroPhysics.Utilities;
using Microsoft.Xna.Framework;
using DefaultEcs;
using DefaultEcs.System;
using Gabiac.Scripts.ECS.Components.Input;
using Gabiac.Scripts.Helpers.Bindings;
using MonoGame.Extended.Input.InputListeners;
using Gabiac.Scripts.ECS.Components;

namespace Gabiac.Scripts.ECS.Systems
{
    public sealed class InputEventReadingSystem : ISystem<GameTime>
    {
        private World world;
        public bool IsEnabled { get; set; } = true;
        
        public InputEventReadingSystem(World _world){
            world = _world;
            world.Subscribe(this);
        }

        [Subscribe]
        private void EventReader(in MouseDownEvent _inputEvent){
            var eventArgs = (MouseEventArgs)_inputEvent.EventArgs;
            if(eventArgs.Button == MouseBinding.Move){
                var entities = world.GetEntities().With<Player>().AsEnumerable();
                foreach (var entity in entities)
                {
                    entity.Set(new TranslationComponent());
                }
            }
        }

        [Subscribe]
        private void EventReader(in MouseMoveEvent _inputEvent){
            var eventArgs = (MouseEventArgs)_inputEvent.EventArgs;
            if(eventArgs.Button == MouseBinding.Move){
                var entities = world.GetEntities().With<Player>().AsEnumerable();
                foreach (var entity in entities)
                {
                    var physicBody = entity.Get<PhysicBody>();
                    var direction = eventArgs.Position.ToVector2() - ConvertUnits.ToDisplayUnits(physicBody.Body.Position);
                    direction.Normalize();
                    entity.Set(new RotationComponent(direction));
                }
            }
        }

        [Subscribe]
        private void EventReader(in MouseUpEvent _inputEvent){
            var eventArgs = (MouseEventArgs)_inputEvent.EventArgs;
            if(eventArgs.Button == MouseBinding.Move){
                var entities = world.GetEntities().With<Player>().With<TranslationComponent>().AsEnumerable();
                foreach (var entity in entities)
                {
                    entity.Remove<TranslationComponent>();
                }
            }
        }

        [Subscribe]
        private void EventReader(in KeyboardDownEvent _inputEvent){
            var eventArgs = (KeyboardEventArgs)_inputEvent.EventArgs;
            if(eventArgs.Key == KeyboardBinding.MoveRight || eventArgs.Key == KeyboardBinding.MoveLeft || eventArgs.Key == KeyboardBinding.MoveUp || eventArgs.Key == KeyboardBinding.MoveDown){
                var entities = world.GetEntities().With<Player>().AsEnumerable();
                foreach (var entity in entities)
                {
                    entity.Set(new TranslationComponent());
                }
            }
        }

        [Subscribe]
        private void EventReader(in KeyboardUpEvent _inputEvent){
            var eventArgs = (KeyboardEventArgs)_inputEvent.EventArgs;
            if(eventArgs.Key == KeyboardBinding.MoveRight || eventArgs.Key == KeyboardBinding.MoveLeft || eventArgs.Key == KeyboardBinding.MoveUp || eventArgs.Key == KeyboardBinding.MoveDown){
                var entities = world.GetEntities().With<Player>().With<TranslationComponent>().AsEnumerable();
                foreach (var entity in entities)
                {
                    entity.Remove<TranslationComponent>();
                }
            }
        }

        [Subscribe]
        private void EventReader(in GamepadDownEvent _inputEvent){
            var eventArgs = (GamePadEventArgs)_inputEvent.EventArgs;
        }

        [Subscribe]
        private void EventReader(in GamepadUpEvent _inputEvent){
            var eventArgs = (GamePadEventArgs)_inputEvent.EventArgs;
        }

        public void Update(GameTime state){}

        public void Dispose(){}
    }
}