using System;
using AirPlaner.IO.Settings;
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
        public TextBox AirlineName { get; set; }

        public Texture2D ProfilePicture { get; set; }
        public Texture2D AirlinePicture { get; set; }
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
                _backgroundTexture = _content.Load<Texture2D>("CreateGame/alternative");
                ProfilePicture = ScreenManager.Settings.Player.PlayerPicture ?? _content.Load<Texture2D>("CreateGame/empty_profile");
                AirlinePicture = ScreenManager.Settings.Airline.AirlinePicture ?? _content.Load<Texture2D>("CreateGame/empty_airline");
            }

            ScreenManager.ScriptLoader.Load("Content/UI/CreateGameUI.lua");
            ScreenManager.ScriptLoader.SetContext(this);
            ScreenManager.ScriptLoader.Run();
            base.Activate(instancePreserved);
        }

        public void ChangeUserPictureButtonOnClick(object sender, EventArgs eventArgs)
        {
            OpenFileDialog ofDialog = new OpenFileDialog(ScreenManager);
            
            ofDialog.Show();

            ofDialog.Closed += delegate
            {
                if (!string.IsNullOrEmpty(ofDialog.SelectedFile))
                {
                    ProfilePicture = _content.Load<Texture2D>(ofDialog.SelectedFile);
                }
            };
        }

        public void ChangeAirlinePictureButtonOnClick(object sender, EventArgs eventArgs)
        {

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
                return;
            }

            if (string.IsNullOrEmpty(AirlineName.Text))
            {
                ErrorText.Text = strings.createGameErrorAirlineNameMissing;
                ErrorText.Alignment = Alignment.TopCenter;
                ScreenManager.InternalGame.GuiManager.Add(Window);
                return;
            }

            //Write Player Settings to Savegame
            var savegame = ScreenManager.Settings;
            savegame.Player.Firstname = Firstname.Text;
            savegame.Player.Lastname = Lastname.Text;
            savegame.Player.PlayerPicture = ProfilePicture;

            //Write Airline Settings to Savegame
            savegame.Airline.AirlinePicture = AirlinePicture;

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

    public sealed class OpenFileDialog : Window
    {
        public ScreenManager ScreenManager { get; set; }

        private Button _okayButton;
        private readonly TextBox _selectionBox;

        public string SelectedFile { get; set; }

        public OpenFileDialog(ScreenManager screenManager) : base(screenManager.InternalGame.GuiManager)
        {
            ScreenManager = screenManager;

            Width = 500;
            Height = 260;
            CloseButtonVisible = false;
            IconVisible = false;
            Resizable = false;
            Text = strings.txtSelectFile;

            var horizontalWidth = Width - 50;

            var selectLabel = new Label(Manager);
            selectLabel.Text = "Dateiname:";
            selectLabel.Width = horizontalWidth / 3;
            selectLabel.Left = 10;
            selectLabel.Top = Height - 140;
            selectLabel.Alignment = Alignment.MiddleRight;
            selectLabel.TextColor = Color.White;

            _selectionBox = new TextBox(screenManager.InternalGame.GuiManager);
            _selectionBox.Left = selectLabel.Left + selectLabel.Width + 10;
            _selectionBox.Top = selectLabel.Top;
            _selectionBox.Width = horizontalWidth / 3 * 2;
            
            _okayButton = new Button(screenManager.InternalGame.GuiManager);
            _okayButton.Text = strings.btnOpen;
            _okayButton.Top = selectLabel.Top + 35;
            _okayButton.Left = horizontalWidth - _okayButton.Width + 20;
            _okayButton.Click += delegate
            {
                SelectedFile = _selectionBox.Text;
                Close();
            };

            Add(_okayButton);
            Add(selectLabel);
            Add(_selectionBox);
        }

        public override void Show()
        {
            ScreenManager.InternalGame.GuiManager.Add(this);
            base.Show();
        }
    }
}
