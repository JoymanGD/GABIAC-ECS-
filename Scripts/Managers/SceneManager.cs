using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Gabiac.Scripts.Scenes;

namespace Gabiac.Scripts.Managers
{
    public class SceneManager {

        public IScene currentScene { get; private set;}
        private SpriteBatch spriteBatch;
        
        public SceneManager(IScene _firstScene, SpriteBatch _spriteBatch, ContentManager _contentManager){
            spriteBatch = _spriteBatch;
            LoadScene(_firstScene, _contentManager);
        }

        public void Draw(GameTime gameTime){
            currentScene.Draw(gameTime);
        }

        public void Update(GameTime gameTime){
            currentScene.Update(gameTime);
        }

        public void LoadScene(IScene _scene, ContentManager _contentManager){
            _scene.PreLoad(_contentManager);
            _scene.Load(_contentManager, spriteBatch);
            _scene.PostLoad(_contentManager);

            var lastScene = currentScene;

            currentScene = _scene;

            lastScene?.Unload();
        }
    }
}