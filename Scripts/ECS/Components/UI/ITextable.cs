using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components.UI
{
    public interface ITextable
    {
        string Text { get; set; }
        Vector2 TextPosition { get; set; }
    }
}