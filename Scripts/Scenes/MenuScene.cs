using Microsoft.Xna.Framework;
using FontStashSharp;
using System.IO;
using Gabiac.Scripts.Managers;
using Myra;
using Myra.Graphics2D.UI;
using Myra.Graphics2D;
using Gabiac.Scripts.Helpers;
using DefaultEcs.System;

namespace Gabiac.Scripts.Scenes
{
    public class MenuScene : IScene
    {
        Desktop desktop;
        public override void Draw(GameTime _gameTime){
            GabiacSettings.graphicsDevice.Clear(Color.Black);
            desktop.Render();
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

            desktop = new Desktop();
            desktop.Root = grid;

            // Add it to the desktop
            base.Load();
        }

        public override void SetEntities(){
            
        }

        public override void SetSystems(){
            var world = GabiacSettings.world;

            UpdateSystems = new SequentialSystem<GameTime>(
                
            );

            DrawSystems = new SequentialSystem<GameTime>(
                
            );
        }
    }
}