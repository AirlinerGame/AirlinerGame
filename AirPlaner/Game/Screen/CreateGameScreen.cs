using System;
using System.Threading;
using AirPlaner.Screen;
using AirPlaner.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MoonSharp.Interpreter;
using TomShane.Neoforce.Controls;

namespace AirPlaner.Game.Screen
{
    public class CreateGameScreen : GameScreen
    {
        private ContentManager _content;
        private Texture2D _backgroundTexture;

        public CreateGameScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void Activate(bool instancePreserved)
        {

            if (_content == null)
            {
                _content = new ContentManager(ScreenManager.Game.Services, "Content");
            }

            if (_backgroundTexture == null)
            {
                _backgroundTexture = _content.Load<Texture2D>("CreateGameBG");
            }

            var manager = ScreenManager.InternalGame.GuiManager;

            var script = new Script();
            script.Globals["ui"] = new GuiLuaManager(manager);
            script.DoFile("Content/UI/CreateGameUI.lua");

            /*
            var groupPanelPlayer = new GroupPanel(manager);

            groupPanelPlayer.Text = strings.createGameUserSettingsCaption;

            groupPanelPlayer.Top = 20;
            groupPanelPlayer.Left = 20;
            groupPanelPlayer.Width = manager.ScreenWidth/2 - 50;
            groupPanelPlayer.Height = 300;

            groupPanelPlayer.Color = Color.CornflowerBlue;
            groupPanelPlayer.TextColor = Color.White;

            manager.Add(groupPanelPlayer);

            var groupPanelAirline = new GroupPanel(manager);

            groupPanelAirline.Text = strings.createGameAirlineSettingsCaption;

            groupPanelAirline.Top = 20;
            groupPanelAirline.Left = 50 + groupPanelPlayer.Width + 20;
            groupPanelAirline.Width = groupPanelPlayer.Width;
            groupPanelAirline.Height = 300;

            groupPanelAirline.Color = Color.CornflowerBlue;
            groupPanelAirline.TextColor = Color.White;

            manager.Add(groupPanelAirline);
            */
            base.Activate(instancePreserved);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, false, false);
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;
            var viewport = ScreenManager.GraphicsDevice.Viewport;
            var fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();
            spriteBatch.Draw(_backgroundTexture, fullscreen, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
