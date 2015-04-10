using System;
using System.IO;
using AirPlaner.Screen;
using Microsoft.Xna.Framework;
using TomShane.Neoforce.Controls;
using EventArgs = TomShane.Neoforce.Controls.EventArgs;

namespace AirPlaner.UI.Components
{
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