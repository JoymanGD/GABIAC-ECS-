using Gabiac.Scripts.Scenes;
using System;

namespace Gabiac.Scripts.Helpers
{
    public class SceneCreator : Creator
    {
        public static SceneCreator instance;

        public SceneCreator(){
            if(instance == null)
                instance = this;
        }
        public override Product Create<T>(){
            IScene scene = (IScene)Activator.CreateInstance(typeof(T));
            return scene;
        }
    }
}