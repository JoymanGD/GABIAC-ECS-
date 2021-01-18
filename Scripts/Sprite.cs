using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Toil.Scripts{
    public class Sprite
    {
        public Texture2D image {get;private set;}
        public float rotation {get;private set;}
        public Rectangle rectangle {get;private set;}
        public Effect shader {get;private set;}
        public int order {get;private set;}

        public Sprite(Texture2D Image, Rectangle Rect, int Order = 1, Effect Shader = null)
        {
            rectangle = Rect;
            SetShader(Shader);
        }

        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(image, rectangle, null, Color.White, rotation, Vector2.Zero, SpriteEffects.None, (float)order);
            
            if(shader != null){
                foreach (var pass in shader.CurrentTechnique.Passes)
                {
                    pass.Apply();
                }
            }
        }

        public void SetShader(Effect Shader){
            shader = Shader;
        }

        public void UnsetShader(){
            shader = null;
        }

        public void Rotate(float angle){
            rotation += angle;
        }

        public void SetOrder(int Order){
            order = Order;
        }

        public void Translate(Vector2 translation){
            rectangle.Offset(translation);
        }

        public void Translate(float x, float y){
            rectangle.Offset(x, y);
        }

        public void SetScale(Vector2 Scale){
            rectangle.Inflate(Scale.X, Scale.Y);
        }

        public void SetScale(float x, float y){
            rectangle.Inflate(x, y);
        }
    }
}