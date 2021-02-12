using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Gabiac.Scripts.Scenes
{
    public interface IScene {
        void Update(GameTime _gameTime);
        void Draw(GameTime _gameTime);
        void PreLoad(ContentManager _contentManager);
        void Load(ContentManager _contentManager, SpriteBatch _spriteBatch);
        void PostLoad(ContentManager _contentManager);
        void Unload();
    }
}