using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Airliner.Plugin.Entities.Game.Database;
using AirPlaner.Config.Entity;
using AirPlaner.IO.Settings;
using AirPlaner.Screen;
using AirPlaner.UI.Components;
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

        public ImageBox UserImageBox { get; set; }
        public ImageBox AirlineImageBox { get; set; }

        public Texture2D ProfilePicture { get; set; }
        public Texture2D AirlinePicture { get; set; }
        public Window Window { get; set; }
        public Label ErrorText { get; set; }
        public ComboBox TurnComboBox { get; set; }
        public GameDatabaseSelectionDialog GameDatabaseSelectionDialog { get; set; }

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
                _backgroundTexture = _content.Load<Texture2D>("AirlinerBG");
                ProfilePicture = ScreenManager.Settings.Player.PlayerPicture.Image ?? _content.Load<Texture2D>("CreateGame/empty_profile");
                AirlinePicture = ScreenManager.Settings.Airline.AirlinePicture.Image ?? _content.Load<Texture2D>("CreateGame/empty_airline");
            }

            ScreenManager.ScriptLoader.Load("Content/UI/CreateGameUI.lua");
            ScreenManager.ScriptLoader.SetContext(this);
            ScreenManager.ScriptLoader.Run();

            GameDatabaseSelectionDialog.OnSelection += GameDatabaseSelectionDialog_OnSelection;

            base.Activate(instancePreserved);
        }

        void GameDatabaseSelectionDialog_OnSelection(object sender, EventArgs e)
        {
            //todo: Continue
            var selected = GameDatabaseSelectionDialog.SelectedProvider;
        }

        public void ChangeUserPictureButtonOnClick(object sender, EventArgs eventArgs)
        {
            OpenFileDialog ofDialog = new OpenFileDialog(ScreenManager);
            
            ofDialog.Show();

            ofDialog.Closed += delegate
            {
                if (!string.IsNullOrEmpty(ofDialog.SelectedFile))
                {
                    using (FileStream stream = new FileStream(ofDialog.SelectedFile, FileMode.Open, FileAccess.Read))
                    {
                        ProfilePicture = Texture2D.FromStream(ScreenManager.GraphicsDevice, stream);
                    }
                    UserImageBox.Image = ProfilePicture;
                    UserImageBox.Refresh();
                }
            };
        }

        public void ChangeAirlinePictureButtonOnClick(object sender, EventArgs eventArgs)
        {
            OpenFileDialog ofDialog = new OpenFileDialog(ScreenManager);

            ofDialog.Show();

            ofDialog.Closed += delegate
            {
                if (!string.IsNullOrEmpty(ofDialog.SelectedFile))
                {
                    using (FileStream stream = new FileStream(ofDialog.SelectedFile, FileMode.Open, FileAccess.Read))
                    {
                        AirlinePicture = Texture2D.FromStream(ScreenManager.GraphicsDevice, stream);
                    }
                    AirlineImageBox.Image = AirlinePicture;
                    AirlineImageBox.Refresh();
                }
            };
        }

        public void SelectGameDatabaseDialogButtonOnClick(object sender, EventArgs eventArgs)
        {
            GameDatabaseSelectionDialog.Show();
        }

        public void GameDatabaseSelectionInit()
        {
            var assembly = AppDomain.CurrentDomain.Load("SampleGameDatabase");

            foreach (Type mytype in assembly.GetTypes().Where(mytype => mytype.GetInterfaces().Contains(typeof(IGameDatabaseProvider))))
            {
                var provider = Activator.CreateInstance(mytype) as IGameDatabaseProvider;
                GameDatabaseSelectionDialog.Items.Add(provider);
            }
        }

        public List<TurnLength> GetTurnLengths()
        {
            var resultList = new List<TurnLength>
            {
                new TurnLength {Text = strings.txtTurnOneDay, Value = 0},
                new TurnLength {Text = strings.txtTurnOneWeek, Value = 1},
                new TurnLength {Text = strings.txtTurnTwoWeeks, Value = 2},
                new TurnLength {Text = strings.txtTurnOneMonth, Value = 3}
            };

            return resultList;
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
            savegame.Player.PlayerPicture.Image = ProfilePicture;

            //Write Airline Settings to Savegame
            savegame.Airline.AirlinePicture.Image = AirlinePicture;
            savegame.Airline.Name = AirlineName.Text;

            //Switch to created GameScreen
            LoadingScreen.Load(ScreenManager, new AirlinerGameScreen(), strings.txtLoading);
            ScreenManager.RemoveScreen(this);
        }

        public void ComboBoxSelectionOnChange(object sender, EventArgs eventArgs)
        {
            var selected = TurnComboBox.Items[TurnComboBox.ItemIndex] as TurnLength;
            ScreenManager.Settings.TurnLength = selected;
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
