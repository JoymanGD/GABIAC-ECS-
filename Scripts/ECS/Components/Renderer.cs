using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components
{
    public struct Renderer
    {
        public Texture2D Image {get; private set;}
        public Color Color {get; private set;}
        public int Layer {get; private set;}

        public Renderer(Texture2D _image, Color _color, int _layer){
            Image = _image;
            Color = _color;
            Layer = _layer;
        }

        public void SetImage(Texture2D _image){
            Image = _image;
        }

        public void SetColor(Color _color){
            Color = _color;
        }
    }
}