using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Gabiac.Scripts.Scenes
{
    public interface IScene {
        void Update(GameTime _gameTime);
        void Draw(GameTime _gameTime);
        void PreLoad();
        void Load();
        void PostLoad();
        void Unload();
    }
}