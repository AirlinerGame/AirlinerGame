using System;
using Airliner.Plugin.API;
using Airliner.Plugin.Entities;
using AirPlaner.IO.Settings;
using AirPlaner.Screen;
using AirPlaner.UI.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AirPlaner.Game.Screen
{
    public class AirlinerGameScreen : ClosableGameScreen
    {
        private ContentManager _content;
        public Texture2D Background { get; set; }
        public MoneyLabel MoneyLabel { get; set; }
        public MusicPlayer MusicPlayer { get; set; }

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

            //Set Event Listener
            Data.Airline.MoneyChanged += Airline_MoneyChanged;

            SoundManager.Instance.TrackChanged += MusicTrackChanged;
            SoundManager.Instance.PositionChanged += MusicTrackPositionChanged;
            if (SoundManager.Instance.PlayingMusic)
            {
                MusicPlayer.CurrentTrack = SoundManager.Instance.CurrentTrack;
            }

            base.Activate(instancePreserved);
        }

        private void MusicTrackChanged(object sender, MusicTrackEventArgs eventargs)
        {
            MusicPlayer.CurrentTrack = eventargs.Track;
        }

        private void MusicTrackPositionChanged(object sender, EventArgs e)
        {
            MusicPlayer.CurrentPosition = SoundManager.Instance.CurrentPosition;
        }

        void Airline_MoneyChanged(object sender, System.EventArgs e)
        {
            //Display new Money Value
            MoneyLabel.Value = Data.Airline.Money;
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
