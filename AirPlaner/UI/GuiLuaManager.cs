using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public GroupPanel CreateGroupPanel()
        {
            var groupPanel = new GroupPanel(Manager);

            return groupPanel;
        }

        public void AddToManager(Component component)
        {
            Manager.Add(component);
        }
    }
}
