﻿using System;
using AirPlaner.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TomShane.Neoforce.Controls;
using EventArgs = TomShane.Neoforce.Controls.EventArgs;

namespace AirPlaner.Game.Screen
{
    public class CreateGameScreen : GameScreen
    {
        private ContentManager _content;
        private Texture2D _backgroundTexture;

        public TextBox Firstname { get; set; }
        public TextBox Lastname { get; set; }

        public Texture2D ProfilePicture { get; set; }
        public Window Window { get; set; }
        public Label ErrorText { get; set; }

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
                ProfilePicture = _content.Load<Texture2D>("CreateGame/empty_profile");
            }

            ScreenManager.ScriptLoader.Load("Content/UI/CreateGameUI.lua");
            ScreenManager.ScriptLoader.SetContext(this);
            ScreenManager.ScriptLoader.Run();
            base.Activate(instancePreserved);
        }

        public void StartGameButtonOnClick(object sender, EventArgs eventArgs)
        {
            //Check if required data is set
            if (string.IsNullOrEmpty(Firstname.Text) || string.IsNullOrEmpty(Lastname.Text))
            {
                //Show Error Message
                ErrorText.Text = strings.createGameErrorNameMissing;
                ErrorText.Alignment = Alignment.TopCenter;
                ScreenManager.InternalGame.GuiManager.Add(Window);
            }
        }

        public void ErrorMessageOkayOnClick(object sender, EventArgs eventArgs)
        {
            ScreenManager.InternalGame.GuiManager.Remove(Window);
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
