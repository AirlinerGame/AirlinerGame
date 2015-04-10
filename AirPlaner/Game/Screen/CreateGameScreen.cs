using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            LoadingScreen.Load(ScreenManager, true, strings.txtLoading, new AirlinerGameScreen());
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

    public sealed class OpenFileDialog : Window
    {
        public ScreenManager ScreenManager { get; set; }

        private Button _okayButton;
        private readonly TextBox _selectionBox;
        public string CurrentDirectory { get; set; }

        public string SelectedFile { get; set; }

        public OpenFileDialog(ScreenManager screenManager) : base(screenManager.InternalGame.GuiManager)
        {
            ScreenManager = screenManager;
            CurrentDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            Width = 500;
            Height = 300;
            CloseButtonVisible = false;
            IconVisible = false;
            Resizable = false;
            Text = strings.txtSelectFile;

            var horizontalWidth = Width - 50;

            var selectLabel = new Label(Manager);
            selectLabel.Text = strings.txtFilename;
            selectLabel.Width = horizontalWidth / 3;
            selectLabel.Left = 10;
            selectLabel.Top = Height - 100;
            selectLabel.Alignment = Alignment.MiddleRight;
            selectLabel.TextColor = Color.White;

            ListBox = new ListBox(Manager);
            ListBox.TextColor = Color.White;
            ListBox.Width = OriginWidth - 30;
            ListBox.Top = 10;
            ListBox.Left = OriginWidth / 2 - ListBox.Width / 2 - 5;
            ListBox.Height = selectLabel.Top - 20;

            //Build Initial Items
            RenderFileTree();

            ListBox.DoubleClick += listBox_DoubleClick;
            
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

            Add(ListBox);
            Add(_okayButton);
            Add(selectLabel);
            Add(_selectionBox);
        }

        private void RenderFileTree()
        {
            ListBox.Items.Clear();

            ListBox.Items.Add("..");

            foreach (var dir in Directory.GetDirectories(CurrentDirectory))
            {
                string[] directories = dir.Split(Path.DirectorySeparatorChar);
                ListBox.Items.Add(directories[directories.Length - 1] + "/");
            }

            foreach (var file in Directory.GetFiles(CurrentDirectory))
            {
                string[] directories = file.Split(Path.DirectorySeparatorChar);
                ListBox.Items.Add(directories[directories.Length - 1]);
            }
            
        }

        public ListBox ListBox { get; set; }

        private void listBox_DoubleClick(object sender, EventArgs e)
        {
            var selectedItem = ListBox.Items[ListBox.ItemIndex] as string;
            if (selectedItem != null)
            {
                if (selectedItem.Equals(".."))
                {
                    //Move Directory up
                    string[] directories = CurrentDirectory.Split(Path.DirectorySeparatorChar);
                    if (directories.Length > 1)
                    {
                        CurrentDirectory = MoveDown(directories);
                    }
                }
                else
                {
                    if (selectedItem.EndsWith("/"))
                    {
                        CurrentDirectory = MoveTo(selectedItem);
                    }
                    else
                    {
                        //File selected!
                        SelectedFile = PathSelection(selectedItem);
                        Close();
                    }
                }

                RenderFileTree();
            }
        }

        private string PathSelection(string selectedItem)
        {
            return CurrentDirectory + Path.DirectorySeparatorChar + selectedItem;
        }

        private string MoveTo(string selectedDir)
        {
            return CurrentDirectory + Path.DirectorySeparatorChar + selectedDir.Substring(0, selectedDir.Length - 1);
        }

        private string MoveDown(string[] directories)
        {
            string newPath = "";
            for (int i = 0; i < directories.Length - 1; i++)
            {
                if (i == directories.Length - 2)
                {
                    newPath = newPath + directories[i];
                }
                else
                {
                    newPath = newPath + directories[i] + Path.DirectorySeparatorChar;                    
                }
            }
            return newPath;
        }

        public override void Show()
        {
            ScreenManager.InternalGame.GuiManager.Add(this);
            base.Show();
        }
    }
}
