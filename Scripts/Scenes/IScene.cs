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

        public virtual ISystem<GameTime> UpdateSystems { get; set; }
        public virtual ISystem<GameTime> DrawSystems { get; set; }
        public virtual void Update(GameTime _gameTime){
            SetEntitiesInUpdate();
            UpdateSystems.Update(_gameTime);
        }
        public virtual void Draw(GameTime _gameTime){
            DrawSystems.Update(_gameTime);
        }
        public virtual void PreLoad(){}
        public virtual void Load(){
            //SetEntities();
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