using Microsoft.Xna.Framework;
using Gabiac.Scripts.Scenes;
using System.Collections.Generic;
using System.Linq;
using Gabiac.Scripts.Helpers;

namespace Gabiac.Scripts.Managers
{
    public static class SceneManager {
        private static List<IScene> scenes = new List<IScene>();
        public static IScene currentScene { get; private set;}

        public static void Draw(GameTime gameTime){
            currentScene.Draw(gameTime);
        }

        public static void Update(GameTime gameTime){
            currentScene.Update(gameTime);
        }

        public static IScene GetScene<T>() where T:IScene{
            return scenes.FirstOrDefault(c=>c.GetType()==typeof(T));
        }

        public static void LoadScene<T>() where T:IScene{
            var _scene = GetScene<T>();
            if(_scene == null){
                _scene = (IScene)SceneCreator.instance.Create<T>();
                scenes.Add(_scene);
            }
            _scene.PreLoad();
            _scene.Load();
            _scene.PostLoad();

            var lastScene = currentScene;

            currentScene = _scene;

            lastScene?.Unload();
        }
    }
}