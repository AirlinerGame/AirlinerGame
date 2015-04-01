﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
        private SideBar _sideBar;

        public Button StartGameButton { get; set; }
        public Button LoadGameButton { get; set; }
        public Button SettingsButton { get; set; }

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

                var manager = ScreenManager.InternalGame.GuiManager;

                _sideBar = new SideBar(manager) { Width = 250, Height = manager.ScreenHeight };
                _sideBar.Left = manager.ScreenWidth - _sideBar.Width;

                var logo = new ImageBox(manager);
                logo.SizeMode = SizeMode.Stretched;
                logo.Width = _sideBar.Width;
                logo.Height = 256;
                logo.Image = _content.Load<Texture2D>("logo");

                var baseHeight = logo.Height + 20;

                StartGameButton = new Button(manager) {Width = 200};
                StartGameButton.Left = _sideBar.Width / 2 - StartGameButton.Width / 2;
                StartGameButton.Top = baseHeight;
                StartGameButton.Text = strings.menuStartGame;
                StartGameButton.Click += StartGameButtonOnClick;

                LoadGameButton = new Button(manager) {Width = 200};
                LoadGameButton.Left = _sideBar.Width / 2 - LoadGameButton.Width / 2;
                LoadGameButton.Top = StartGameButton.Top + 30;
                LoadGameButton.Text = strings.menuLoadGame;

                SettingsButton = new Button(manager) {Width = 200};
                SettingsButton.Left = _sideBar.Width / 2 - SettingsButton.Width / 2;
                SettingsButton.Top = LoadGameButton.Top + 30;
                SettingsButton.Text = strings.menuSettings;
                SettingsButton.Click += SettingsButtonOnClick;

                _sideBar.Add(logo);
                _sideBar.Add(StartGameButton);
                _sideBar.Add(LoadGameButton);
                _sideBar.Add(SettingsButton);

                manager.Add(_sideBar);

                _backgroundTexture = _content.Load<Texture2D>("AirlinerBG");
            }
        }

        private void StartGameButtonOnClick(object sender, EventArgs eventArgs)
        {
            LoadingScreen.Load(ScreenManager, true, "Loading...", new CreateGameScreen());
        }

        private void SettingsButtonOnClick(object sender, EventArgs eventArgs)
        {
            var manager = ScreenManager.InternalGame.GuiManager;
            var window = new Window(manager);
            
            window.Width = 400;
            window.Height = 200;
            window.Text = strings.settingsWindowTitle;
            window.IconVisible = false;
            window.CloseButtonVisible = false;
            window.Movable = false;
            window.Resizable = false;

            var groupBox = new GroupBox(manager);
            groupBox.Text = strings.menuLocaleTitle;
            groupBox.Width = window.Width - 50;
            groupBox.Height = 50;
            groupBox.Left = window.Width/2 - groupBox.Width/2 - 10;
            groupBox.Top = 10;
            groupBox.TextColor = Color.White;

            var comboBox = new ComboBox(manager);
            var availableLanguages = ScreenManager.InternalGame.AvailableLanguages;
            comboBox.Items.AddRange(availableLanguages.Values);

            comboBox.Width = groupBox.Width - 25;
            comboBox.Height = 25;
            comboBox.Left = groupBox.Width/2 - comboBox.Width/2;
            comboBox.ItemIndex = DictionaryHelper.GetKeyFromValue(availableLanguages, ApplicationSettings.Instance.SelectedLanguage);
            comboBox.TextColor = Color.White;
            //comboBox.Text = availableLanguages[comboBox.ItemIndex].Name;

            comboBox.ItemIndexChanged += delegate
            {
                ApplicationSettings.Instance.SelectedLanguage = availableLanguages[comboBox.ItemIndex].CultureCode;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(ApplicationSettings.Instance.SelectedLanguage);
                ApplicationSettings.Instance.Save();
            };
            
            comboBox.Init();
            comboBox.Refresh();

            var btnSaveAndClose = new Button(manager);
            btnSaveAndClose.Text = strings.btnSaveAndClose;
            btnSaveAndClose.Width = window.Width/2 - 25;
            btnSaveAndClose.TextColor = Color.White;
            btnSaveAndClose.Left = 10;
            btnSaveAndClose.Top = window.Height - btnSaveAndClose.Height - 50;
            btnSaveAndClose.Click += delegate(object o, EventArgs args)
            {
                window.Close();
            };

            var btnClose = new Button(manager);
            btnClose.Text = strings.btnClose;
            btnClose.Width = window.Width / 2 - 25;
            btnClose.TextColor = Color.White;
            btnClose.Left = 20 + btnSaveAndClose.Width;
            btnClose.Top = window.Height - btnClose.Height - 50;
            btnClose.Click += delegate(object o, EventArgs args)
            {
                window.Close();
            };

            groupBox.Add(comboBox);

            window.Add(groupBox);
            window.Add(btnSaveAndClose);
            window.Add(btnClose);
            manager.Add(window);
        }

        /// <summary>
        /// Unloads screen content.
        /// </summary>
        public override void Unload()
        {
            _content.Unload();
            ScreenManager.InternalGame.GuiManager.Remove(_sideBar);
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
