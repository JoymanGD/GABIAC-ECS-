using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Toil.Scripts;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;

namespace Toil
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;

        private World world;
        private Entity entity;
        Texture2D texture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //_graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            world = new World();
            entity = world.CreateEntity();
            

            // var tr = new Transform(new Vector2(_graphics.GraphicsDevice.Viewport.Width/2, _graphics.GraphicsDevice.Viewport.Height/2), new Vector2(.3f,.3f), 0, 12);
            // var sprite = new Sprite(Content.Load<Texture2D>("Car"), tr);
            // var mouseInput = new MouseInput(tr);
            // var keyboardInput = new KeyboardInput(tr);
            // var inputs = new List<Input>{keyboardInput, mouseInput};
            
            // player = new Player(sprite, inputs, "Player1");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            texture = Texture2D.FromFile(_graphics.GraphicsDevice, "Content/Car.png");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
