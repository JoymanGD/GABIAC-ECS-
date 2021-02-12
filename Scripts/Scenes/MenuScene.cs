using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Systems;
using Gabiac.Scripts.ECS.Components;
using System;

namespace Gabiac.Scripts.Scenes
{
    public class MenuScene : IScene
    {

#region ECS

        private IParallelRunner mainRunner;
        private DefaultEcs.World world;
        private GraphicsDeviceManager graphics;

#endregion

        public MenuScene(GraphicsDeviceManager _graphics){
            graphics = _graphics;
            SetupWorld();
        }

        public void Update(GameTime _gameTime){
        }

        public void Draw(GameTime _gameTime){
        }

        public void PreLoad(ContentManager _contentManager){

        }

        public void Load(ContentManager _contentManager, SpriteBatch _spriteBatch){
            
        }

        public void PostLoad(ContentManager _contentManager){

        }

        public void Unload(){

        }

#region Setup

        private void SetupWorld(){
            mainRunner = new DefaultParallelRunner(Environment.ProcessorCount);
            world = new DefaultEcs.World();
        }

#endregion

    }
}