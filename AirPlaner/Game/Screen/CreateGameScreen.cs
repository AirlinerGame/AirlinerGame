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

            ScreenManager.ScriptLoader.Load("Content/UI/CreateGameUI.lua");
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
