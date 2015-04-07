using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MoonSharp.Interpreter;
using TomShane.Neoforce.Controls;

namespace AirPlaner.UI
{
    [MoonSharpUserData]
    public class GuiLuaManager
    {

        public Manager Manager { get; private set; }

        public GuiLuaManager(Manager manager)
        {
            Manager = manager;
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

        public SideBar CreateSideBar()
        {
            var sideBar = new SideBar(Manager);
            return sideBar;
        }

        public ImageBox CreateImageBox()
        {
            var imageBox = new ImageBox(Manager);
            return imageBox;
        }

        public void AddToManager(Component component)
        {
            Manager.Add(component);
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

        public void SetCallMethod(Button button, object context, string methodName)
        {
            var type = context.GetType();
            var method = type.GetMethod(methodName);

            var buttonEvent = typeof (Button).GetEvent("Click");
            var delegateT = Delegate.CreateDelegate(buttonEvent.EventHandlerType, context, method);
            var addMethod = buttonEvent.GetAddMethod();
            Object[] addHandlers = { delegateT };
            addMethod.Invoke(button, addHandlers);
        }
    }
}
