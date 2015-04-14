using System.Collections.Generic;
using System.Linq;
using Airliner.Plugin.Entities.Game.Database;
using TomShane.Neoforce.Controls;

namespace AirPlaner.UI.Components
{
    public class GameDatabaseSelectionDialog : Window
    {
        private readonly Label _availableDatabases;
        private readonly ListBox _databaseListBox;
        private readonly Button _selectButton;

        public IGameDatabaseProvider SelectedProvider { get; private set; }
        public EventedList<IGameDatabaseProvider> Items { get; private set; } 

        public event EventHandler OnSelection;

        public GameDatabaseSelectionDialog(Manager manager) : base(manager)
        {
            IconVisible = false;
            Resizable = false;
            Text = "Select Game Database";

            _availableDatabases = new Label(manager);
            _availableDatabases.Text = "Select Database:";
            _databaseListBox = new ListBox(manager);
            _selectButton = new Button(manager);
            _selectButton.Text = "Select";

            _selectButton.Click += delegate
            {
                SelectedProvider = RetrieveSelectedItem();

                if (OnSelection != null)
                {
                    OnSelection(this, new EventArgs());
                }
                Close();
            };

            Items = new EventedList<IGameDatabaseProvider>();
            Items.ItemAdded += delegate
            {
                _databaseListBox.Items.Clear();
                foreach (var gameDatabaseProvider in Items)
                {
                    _databaseListBox.Items.Add(gameDatabaseProvider.GetName());
                }
            };

            Add(_availableDatabases);
            Add(_databaseListBox);
            Add(_selectButton);
        }

        private IGameDatabaseProvider RetrieveSelectedItem()
        {
            if (_databaseListBox.ItemIndex == -1)
            {
                return null;
            }
            var name = _databaseListBox.Items[_databaseListBox.ItemIndex] as string;
            return Items.SingleOrDefault(i => i.GetName().Equals(name));
        }

        public override int Width
        {
            get { return base.Width; }
            set
            {
                base.Width = value;
                CalculateChildPositions();
            }
        }

        public override int Height
        {
            get { return base.Height; }
            set
            {
                base.Height = value;
                CalculateChildPositions();
            }
        }

        private void CalculateChildPositions()
        {
            if (_availableDatabases == null || _databaseListBox == null || _selectButton == null)
            {
                return;
            }

            _availableDatabases.Width = ClientWidth - 20;
            _databaseListBox.Width = ClientWidth - 20;
            _selectButton.Width = ClientWidth - 20;

            _databaseListBox.Height = ClientHeight - _availableDatabases.Height - _selectButton.Height - 25;

            var posLeft = ClientWidth / 2 - _availableDatabases.Width / 2;
            _availableDatabases.Left = posLeft;
            _databaseListBox.Left = posLeft;
            _selectButton.Left = posLeft;

            _availableDatabases.Top = 5;
            _databaseListBox.Top = _availableDatabases.Top + _availableDatabases.Height + 5;
            _selectButton.Top = _databaseListBox.Top + _databaseListBox.Height + 5;
        }

    }
}