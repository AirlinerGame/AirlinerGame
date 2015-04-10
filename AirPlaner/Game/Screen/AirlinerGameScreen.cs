using AirPlaner.IO.Settings;
using AirPlaner.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AirPlaner.Game.Screen
{
    public class AirlinerGameScreen : GameScreen
    {
        private ContentManager _content;
        public Texture2D Background { get; set; }

        public Savegame Data
        {
            get { return ScreenManager.InternalGame.UserSettings.Settings; }
        }

        public override void Activate(bool instancePreserved)
        {
            if (_content == null)
            {
                _content = new ContentManager(ScreenManager.Game.Services, "Content");
            }

            Background = _content.Load<Texture2D>("AirlinerGame/blurred");

            ScreenManager.ScriptLoader.Load("Content/UI/AirlinerGameUI.lua");
            ScreenManager.ScriptLoader.SetContext(this);
            ScreenManager.ScriptLoader.Run();

            base.Activate(instancePreserved);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;
            var viewport = ScreenManager.GraphicsDevice.Viewport;
            var fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();
            spriteBatch.Draw(Background, fullscreen, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
