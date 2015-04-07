using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
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
            switch (color)
            {
                case "CornflowerBlue":
                    component.Color = Color.CornflowerBlue;
                    break;
                default:
                    component.Color = Color.Black;
                    break;
            }
        }

        public GroupPanel CreateGroupPanel()
        {
            var groupPanel = new GroupPanel(Manager);
            groupPanel.TextColor = Color.White;

            return groupPanel;
        }

        public void AddToManager(Component component)
        {
            Manager.Add(component);
        }
    }
}
