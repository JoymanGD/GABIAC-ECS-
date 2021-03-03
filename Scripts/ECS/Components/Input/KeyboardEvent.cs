using MonoGame.Extended.Input;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components.Input
{
    public struct KeyboardEvent:InputEvent
    {
        public Keys Key { get; set; }

        public KeyboardEvent(Keys _key){
            Key = _key;
        }
    }
}