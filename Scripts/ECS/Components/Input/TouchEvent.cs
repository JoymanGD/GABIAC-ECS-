using MonoGame.Extended.Input;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components.Input
{
    public struct TouchEvent:InputEvent
    {
        public TouchLocation Location { get; set; }

        public TouchEvent(TouchLocation _location){
            Location = _location;
        }
    }
}