using Microsoft.Xna.Framework;
using Gabiac.Scripts.Helpers;
using DefaultEcs.System;
using Gabiac.Scripts.ECS.Systems;

namespace Gabiac.Scripts.Scenes
{
    public abstract class IScene : Product {
        private bool EntitiesSet = false;
        private void SetEntitiesInUpdate(){
            if(!EntitiesSet){
                SetEntities();
                EntitiesSet = true;
            }
        }

        public virtual ISystem<float> UpdateSystems { get; set; }
        public virtual ISystem<float> DrawSystems { get; set; }
        public virtual void Update(GameTime _gameTime){
            //SetEntitiesInUpdate();
            UpdateSystems.Update((float)_gameTime.ElapsedGameTime.TotalMilliseconds);
        }
        public virtual void Draw(GameTime _gameTime){
            DrawSystems.Update((float)_gameTime.ElapsedGameTime.TotalMilliseconds);
        }
        public virtual void PreLoad(){}
        public virtual void Load(){
            SetEntities();
            SetSystems();
        }
        public virtual void PostLoad(){}
        public virtual void Unload(){
            UpdateSystems.Dispose();
            DrawSystems.Dispose();
        }


        public abstract void SetEntities();
        public abstract void SetSystems();
    }
}