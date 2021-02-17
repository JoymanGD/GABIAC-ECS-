using Microsoft.Xna.Framework;
using Gabiac.Scripts.Helpers;

namespace Gabiac.Scripts.Scenes
{
    public abstract class IScene : Product {
        public abstract void Update(GameTime _gameTime);
        public abstract void Draw(GameTime _gameTime);
        public abstract void PreLoad();
        public abstract void Load();
        public abstract void PostLoad();
        public abstract void Unload();
    }
}