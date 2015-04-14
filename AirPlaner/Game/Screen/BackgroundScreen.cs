using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Airliner.Plugin.API;
using AirPlaner.Config.Entity;
using AirPlaner.IO.Settings;
using AirPlaner.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TomShane.Neoforce.Controls;
using EventArgs = TomShane.Neoforce.Controls.EventArgs;

namespace AirPlaner.Game.Screen
{
    public class BackgroundScene : GameScreen
    {
        private ContentManager _content;
        private Texture2D _backgroundTexture;

        public Window SettingsWindow { get; set; }
        public ComboBox LanguageComboBox { get; set; }
        public TrackBar MusicVolumeTrackBar { get; set; }
        public TrackBar FxVolumeTrackBar { get; set; }

        public Texture2D Logo { get; set; }

        public BackgroundScene()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        /// <summary>
        /// Activates the screen. This method gets called after the screen is added to the screen manager.
        /// </summary>
        /// <param name="instancePreserved">Not used yet! Will be used later for serialization</param>
        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                if (_content == null)
                {
                    _content = new ContentManager(ScreenManager.Game.Services, "Content");
                }

                Logo = _content.Load<Texture2D>("logo");

                ScreenManager.ScriptLoader.Load("Content/UI/MainMenuUI.lua");
                ScreenManager.ScriptLoader.SetContext(this);
                ScreenManager.ScriptLoader.Run();

                _backgroundTexture = _content.Load<Texture2D>("AirlinerBG");
                MusicVolumeTrackBar.Value = ApplicationSettings.Instance.MusicVolume;
                FxVolumeTrackBar.Value = ApplicationSettings.Instance.FxVolume;
                MusicVolumeTrackBar.ValueChanged += MusicVolumeTrackBar_ValueChanged;
                FxVolumeTrackBar.ValueChanged += FxVolumeTrackBar_ValueChanged;
            }
        }

        void FxVolumeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            SoundManager.Instance.SoundEffectVolume = FxVolumeTrackBar.Value;
            ApplicationSettings.Instance.FxVolume = FxVolumeTrackBar.Value;
        }

        void MusicVolumeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            SoundManager.Instance.MusicVolume = MusicVolumeTrackBar.Value;
            ApplicationSettings.Instance.MusicVolume = MusicVolumeTrackBar.Value;
        }
        public void StartGameButtonOnClick(object sender, EventArgs eventArgs)
        {
            LoadingScreen.Load(ScreenManager, new CreateGameScreen(), strings.txtLoading);
        }

        public void LoadGameButtonOnClick(object sender, EventArgs eventArgs)
        {
            ScreenManager.Settings.SettingsManager.Load("Savegames/Temp.savegame");
            LoadingScreen.Load(ScreenManager, new AirlinerGameScreen(), strings.txtLoading);
        }

        public void SettingsButtonOnClick(object sender, EventArgs eventArgs)
        {
            SettingsWindow.Show();
        }

        public void SettingsSaveAndCloseOnClick(object sender, EventArgs eventArgs)
        {
            SettingsWindow.Close();
            ApplicationSettings.Instance.Save();
        }

        public void InitLanguageComboBox()
        {
            LanguageComboBox.Items.Clear();
            var availableLanguages = ScreenManager.InternalGame.AvailableLanguages;
            LanguageComboBox.Items.AddRange(availableLanguages.Values);
            LanguageComboBox.ItemIndex = DictionaryHelper.GetKeyFromValue(availableLanguages, ApplicationSettings.Instance.SelectedLanguage);
            LanguageComboBox.TextColor = Color.White;

            LanguageComboBox.ItemIndexChanged += delegate
            {
                ApplicationSettings.Instance.SelectedLanguage = availableLanguages[LanguageComboBox.ItemIndex].CultureCode;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(ApplicationSettings.Instance.SelectedLanguage);
                ApplicationSettings.Instance.Save();
            };

            LanguageComboBox.Init();
            LanguageComboBox.Refresh();
        }

        /// <summary>
        /// Unloads screen content.
        /// </summary>
        public override void Unload()
        {
            _content.Unload();
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;
            var viewport = ScreenManager.GraphicsDevice.Viewport;
            var fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();

            spriteBatch.Draw(_backgroundTexture, fullscreen, Color.White);

            spriteBatch.End();
        }

        /// <summary>
        /// This method gets called with every update call. All screens get called! Even the not visible one.
        /// 
        /// To update just the visible one use HandleInput()
        /// </summary>
        /// <param name="gameTime">Current GameTime</param>
        /// <param name="otherScreenHasFocus">Is another screen having focus?</param>
        /// <param name="coveredByOtherScreen">Depending on covered or not the screen will be visible. Pass true to always be visible in the background</param>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
        }
    }

    public class DictionaryHelper
    {
        public static int GetKeyFromValue(Dictionary<int, Language> dictionary, string value)
        {
            return (from sValue in dictionary where sValue.Value.CultureCode.Equals(value) select sValue.Key).FirstOrDefault();
        }
    }
}
