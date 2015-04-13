using System;
using System.Collections.Generic;
using System.IO;
using AirPlaner.UI.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MoonSharp.Interpreter;
using TomShane.Neoforce.Controls;
using Button = TomShane.Neoforce.Controls.Button;

namespace AirPlaner.UI
{
    [MoonSharpUserData]
    public class GuiLuaManager
    {

        public Manager Manager { get; private set; }
        public AirPlanerGame Game { get; set; }

        public GuiLuaManager(Manager manager, AirPlanerGame game)
        {
            Manager = manager;
            Game = game;
        }

        public int GetScreenWidth()
        {
            return Manager.ScreenWidth;
        }

        public int GetScreenHeight()
        {
            return Manager.ScreenHeight;
        }

        public string GetText(string key)
        {
            return strings.ResourceManager.GetString(key);
        }

        public void SetColor(string color, Container component)
        {
            var colors = typeof (Color).GetProperties();

            foreach (var colorT in colors)
            {
                if (colorT.Name.Equals(color))
                {
                    component.Color = (Color) colorT.GetValue(null, null);
                }
            }
        }

        public Color GetColor(string color)
        {
            var colors = typeof(Color).GetProperties();

            foreach (var colorT in colors)
            {
                if (colorT.Name.Equals(color))
                {
                    return (Color) colorT.GetValue(null, null);
                }
            }

            return Color.Black;
        }

        public Color CreateColor(string color, float alpha)
        {
            return new Color(GetColor(color), alpha);
        }

        public Texture2D GetTexture(string texture)
        {
            return Manager.Content.Load<Texture2D>(texture);
        }

        public GroupPanel CreateGroupPanel()
        {
            var groupPanel = new GroupPanel(Manager);
            groupPanel.TextColor = Color.White;

            return groupPanel;
        }

        public Button CreateButton()
        {
            var button = new Button(Manager);
            button.TextColor = Color.White;

            return button;
        }

        public ImageButton CreateImageButton()
        {
            var imageButton = new ImageButton(Manager, Game);
            imageButton.TextColor = Color.White;

            return imageButton;
        }

        public SideBar CreateSideBar()
        {
            var sideBar = new SideBar(Manager);
            return sideBar;
        }

        public Panel CreatePanel()
        {
            var panel = new Panel(Manager);
            return panel;
        }

        public ImageBox CreateImageBox()
        {
            var imageBox = new ImageBox(Manager);
            return imageBox;
        }

        public TextBox CreateTextBox()
        {
            var textBox = new TextBox(Manager) {TextColor = Color.White};
            return textBox;
        }

        public Label CreateLabel()
        {
            var label = new Label(Manager) {TextColor = Color.White};
            return label;
        }

        public MoneyLabel CreateMoneyLabel()
        {
            var moneyLabel = new MoneyLabel(Manager);
            return moneyLabel;
        }

        public HeaderLabel CreateHeaderLabel()
        {
            var headerLabel = new HeaderLabel(Manager, Game);
            headerLabel.TextColor = Color.White;
            headerLabel.Init();
            return headerLabel;
        }

        public GroupBox CreateGroupBox()
        {
            var groupBox = new GroupBox(Manager);
            groupBox.TextColor = Color.White;

            return groupBox;
        }

        public ComboBox CreateComboBox()
        {
            var comboBox = new ComboBox(Manager);
            comboBox.TextColor = Color.White;

            return comboBox;
        }

        public Window CreateWindow()
        {
            var window = new Window(Manager) {TextColor = Color.White};
            return window;
        }

        public NotifyIcon CreateNotifyIcon()
        {
            var icon = new NotifyIcon(Manager);
            //icon.Font = Game.Content.Load<SpriteFont>("numberfont");
            return icon;
        }

        public MusicPlayer CreateMusicPlayer()
        {
            var musicPlayer = new MusicPlayer(Manager);
            return musicPlayer;
        }

        public void AddToManager(Component component)
        {
            Manager.Add(component);
        }

        public void AddItem(ComboBox comboBox, object value)
        {
            comboBox.Items.Add(value);
        }

        public void AddItems(ComboBox comboBox, List<object> value)
        {
            comboBox.Items.AddRange(value);
        }

        public void SetImage(ImageBox imageBox, Texture2D texture)
        {
            imageBox.Image = texture;
        }

        public void SetImageMode(ImageBox imageBox, string mode)
        {
            switch (mode)
            {
                case "Auto":
                    imageBox.SizeMode = SizeMode.Auto;
                    break;
                case "Centered":
                    imageBox.SizeMode = SizeMode.Centered;
                    break;
                case "Stretched":
                    imageBox.SizeMode = SizeMode.Stretched;
                    break;
            }
        }

        public Texture2D TextureFromFile(string path)
        {
            Texture2D result = null;
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                result = Texture2D.FromStream(Manager.GraphicsDevice, stream);
            }
            return result;
        }

        public void SetCallMethod(Control control, object context, string methodName)
        {
            var type = context.GetType();
            var method = type.GetMethod(methodName);

            var buttonEvent = typeof (Control).GetEvent("Click");
            var delegateT = Delegate.CreateDelegate(buttonEvent.EventHandlerType, context, method);
            var addMethod = buttonEvent.GetAddMethod();
            Object[] addHandlers = { delegateT };
            addMethod.Invoke(control, addHandlers);
        }

        public void SetSelectionMethod(ComboBox comboBox, object context, string methodName)
        {
            var type = context.GetType();
            var method = type.GetMethod(methodName);

            var comboBoxEvent = typeof(ComboBox).GetEvent("ItemIndexChanged");
            var delegateT = Delegate.CreateDelegate(comboBoxEvent.EventHandlerType, context, method);
            var addMethod = comboBoxEvent.GetAddMethod();
            Object[] addHandlers = { delegateT };
            addMethod.Invoke(comboBox, addHandlers);
        }

        public void Include(string luaPath)
        {
            Game.ScreenManager.ScriptLoader.Include(luaPath);
        }
    }
}
