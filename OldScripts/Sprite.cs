using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Toil.Scripts{
    public class Sprite
    {
        public Texture2D image {get;private set;}
        public Transform transform {get; private set;}
        public Effect shader {get;private set;}
        public int order {get;private set;}
        public Vector2 offset {get; private set;}

        public Sprite(Texture2D Image, Transform tr, int Order = 1, Effect Shader = null)
        {
            image = Image;
            transform = tr;
            SetShader(Shader);
            offset = new Vector2(image.Width / 2, image.Height / 2);
        }

        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(image, transform.position, null, Color.White, transform.rotation, offset, transform.scale, SpriteEffects.None, (float)order);

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

        public void SetOrder(int Order){
            order = Order;
        }
    }
}