using Microsoft.Xna.Framework;
using DefaultEcs.Threading;
using FontStashSharp;
using System.IO;
using Gabiac.Scripts.Managers;
using System;
using Myra;
using Myra.Graphics2D.UI;
using Myra.Graphics2D;
using Gabiac.Scripts.Helpers;

namespace Gabiac.Scripts.Scenes
{
    public class MenuScene : IScene
    {

#region ECS
        private Desktop desktop;

#endregion

        public override void Update(GameTime _gameTime){
        }

        public override void Draw(GameTime _gameTime){
            desktop.Render();
        }

        public override void PreLoad(){

        }

        public override void Load(){
            MyraEnvironment.Game = GabiacSettings.game;

            var grid = new Grid();
            //grid.ShowGridLines = true;
            var fontSys = FontSystemFactory.Create(GabiacSettings.game.GraphicsDevice, 400, 400);
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
                SceneManager.LoadScene<GameScene>();
            };

            grid.Widgets.Add(button);

            // Add it to the desktop
            desktop = new Desktop();
            desktop.Root = grid;
        }

        public override void PostLoad(){

        }

        public override void Unload(){

        }
    }
}