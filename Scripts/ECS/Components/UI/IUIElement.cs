using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components.UI
{
    public interface IUIElement
    {
        Vector2 Position { get; set; }
        Vector2 Scale { get; set; }
        float Rotation { get; set; }
    }
}