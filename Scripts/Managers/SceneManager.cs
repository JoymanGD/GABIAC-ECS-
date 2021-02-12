using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Gabiac.Scripts.Scenes;

namespace Gabiac.Scripts.Managers
{
    public class SceneManager {
        public static SceneManager instance;
        public IScene currentScene { get; private set;}
        public SpriteBatch spriteBatch { get; private set;}
        public GraphicsDeviceManager graphics { get; private set;}
        public ContentManager contentManager { get; private set;}
        
        public SceneManager(IScene _firstScene, SpriteBatch _spriteBatch, GraphicsDeviceManager _graphics, ContentManager _contentManager){
            if(instance == null)
                instance = this;

            spriteBatch = _spriteBatch;
            graphics = _graphics;
            contentManager = _contentManager;
            
            LoadScene(_firstScene);
        }

        public void Draw(GameTime gameTime){
            currentScene.Draw(gameTime);
        }

        public void Update(GameTime gameTime){
            currentScene.Update(gameTime);
        }

        public void LoadScene(IScene _scene){
            _scene.PreLoad();
            _scene.Load();
            _scene.PostLoad();

            var lastScene = currentScene;

            currentScene = _scene;

            lastScene?.Unload();
        }
    }
}