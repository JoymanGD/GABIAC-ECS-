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
        private Vector2 direction;
        
        private Vector2 offset;
        private Vector2 lastPos;
        private Vector2 lastDir;

        public Sprite(Texture2D Image, Rectangle Rect, int Order = 1, Effect Shader = null)
        {
            image = Image;
            rectangle = Rect;
            SetShader(Shader);
            offset = new Vector2(Rect.Width/2, Rect.Height/2);

            lastPos = rectangle.Location.ToVector2();
            lastDir = Vector2.Zero;
        }

        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(image, rectangle, null, Color.White, rotation, offset, SpriteEffects.None, (float)order);
            
            if(shader != null){
                foreach (var pass in shader.CurrentTechnique.Passes)
                {
                    pass.Apply();
                }
            }
        }

        public void SetDirection(Vector2 dir){
            direction = dir;
        }

        public Vector2 GetDirection(){
            var dir = rectangle.Location.ToVector2() - lastPos;
            lastPos = rectangle.Location.ToVector2();
            if(dir != Vector2.Zero)
                lastDir = dir;
            return dir == Vector2.Zero ? lastDir : dir;
        }

        public void SetShader(Effect Shader){
            shader = Shader;
        }

        public void UnsetShader(){
            shader = null;
        }

        public void Rotate(float angle){
            rotation = angle;
        }

        public void SetOrder(int Order){
            order = Order;
        }

        public void Translate(Vector2 translation){
            var rect = rectangle;
            rect.Location += translation.ToPoint();
            rectangle = rect;
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