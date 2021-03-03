using System;
using DefaultEcs;
using DefaultEcs.System;
using Gabiac.Scripts.ECS.Components.UI;
using Gabiac.Scripts.ECS.Components.Input;
using Gabiac.Scripts.ECS.Components;
using MonoGame.Extended.Input;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Systems
{
    [WhenAddedEither(typeof(MouseEvent), typeof(KeyboardEvent), typeof(GamepadEvent), typeof(TouchEvent))]
    public partial class InputEventSystem : AEntitySetSystem<GameTime>
    {
        private World world;
        
        public InputEventSystem(World _world) : base(_world){
            world = _world;
        }

        [Update]
        private void Update(in Entity _entity){
            Console.WriteLine("Added input event, value: ");
        }
    }
}