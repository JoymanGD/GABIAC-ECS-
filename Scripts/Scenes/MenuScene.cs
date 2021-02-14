using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using FontStashSharp;
using System.IO;
using Gabiac.Scripts.ECS.Systems;
using Gabiac.Scripts.ECS.Components;
using Gabiac.Scripts.Managers;
using System;
using Myra;
using Myra.Graphics2D.UI;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;

namespace Gabiac.Scripts.Scenes
{
    public class MenuScene : IScene
    {

#region ECS

        private IParallelRunner mainRunner;
        private DefaultEcs.World world;
        private Game game;
        private Desktop desktop;

#endregion

        public MenuScene(Game _game){
            game = _game;
            SetupWorld();
        }

        public void Update(GameTime _gameTime){
        }

        public void Draw(GameTime _gameTime){
            desktop.Render();
        }

        public void PreLoad(){

        }

        public void Load(){
            MyraEnvironment.Game = game;

            var grid = new Grid();
            //grid.ShowGridLines = true;
            var fontSys = FontSystemFactory.Create(game.GraphicsDevice, 400, 400);
            fontSys.AddFont(File.ReadAllBytes(@"Content/Fonts/NotoSansJP-Light.otf"));

            var title = new Label
            {
            GridRow = 0,
            Id = "label",
            Text = "GABIAC",
            Font = fontSys.GetFont(80),
            TextColor = Color.Yellow,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Bottom
            };
            grid.Widgets.Add(title);

            // Button
            var button = new TextButton
            {
            GridRow = 1,
            Text = "PLAY",
            Height = 40,
            Width = 130,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0,20,0,0),
            Background = null
            };
            
            button.Click += (s, a) =>
            {
                SceneManager.instance.LoadScene(new GameScene());
            };

            grid.Widgets.Add(button);

            // Add it to the desktop
            desktop = new Desktop();
            desktop.Root = grid;
        }

        public void PostLoad(){

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