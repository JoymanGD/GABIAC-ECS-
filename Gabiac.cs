using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Gabiac.Scripts.Managers;
using Gabiac.Scripts.Scenes;

namespace Gabiac
{
    public class Gabiac : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public Gabiac()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.IsFullScreen = true;
        }
        protected override void Initialize()
        {
            SetupGraphics();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            new SceneManager(new GameScene(graphics), spriteBatch, Content);
        }

        protected override void Update(GameTime _gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            SceneManager.instance.Update(_gameTime);

            base.Update(_gameTime);
        }

        protected override void Draw(GameTime _gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            SceneManager.instance.Draw(_gameTime);
            
            base.Draw(_gameTime);
        }

        private void SetupGraphics(){
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
        }
    }
}
