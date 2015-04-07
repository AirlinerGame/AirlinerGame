using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AirPlaner.Screen;
using AirPlaner.UI;
using MoonSharp.Interpreter;
using TomShane.Neoforce.Controls;

namespace AirPlaner.Game.Api
{
    public class ScriptLoader
    {
        private ScriptContext _context;

        public ScreenManager ScreenManager { get; private set; }

        public ScriptLoader(ScreenManager manager)
        {
            ScreenManager = manager;
        }

        public void Reload()
        {
            Debug.WriteLine("Reload ScriptContext");
            _context.Reload();
        }

        public void Load(string filepath)
        {
            _context = new ScriptContext(filepath, this);
        }
    }

    public class ScriptContext
    {
        public string CurrentScriptPath { get; private set; }
        public Script Script { get; private set; }
        public ScriptLoader ScriptLoader { get; private set; }

        public ScriptContext(string filepath, ScriptLoader scriptLoader)
        {
            CurrentScriptPath = filepath;
            ScriptLoader = scriptLoader;

            Load();
        }

        private void Load()
        {
            Script = new Script();
            Script.Globals["ui"] = new GuiLuaManager(ScriptLoader.ScreenManager.InternalGame.GuiManager);
            Script.DoFile("Content/UI/CreateGameUI.lua");
        }

        private void Unload()
        {
            var guiManager = ScriptLoader.ScreenManager.InternalGame.GuiManager;

            var cachedComp = guiManager.Controls.ToArray();
            foreach (var component in cachedComp)
            {
                guiManager.Remove(component);
            }
        }

        public void Reload()
        {
            Unload();
            Load();
        }

    }
}
