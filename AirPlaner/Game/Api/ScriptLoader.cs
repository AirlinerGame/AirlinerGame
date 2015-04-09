using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Airliner.Plugin;
using AirPlaner.Game.Screen;
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
        public List<IPlugin> Mods { get; private set; } 

        public ScriptLoader(ScreenManager manager)
        {
            ScreenManager = manager;
            Mods = new List<IPlugin>();
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

        public void LoadPlugins()
        {
            if (Directory.Exists("Mods"))
            {
                
            }
        }

        public void SetContext(object context)
        {
            _context.SetScriptContext(context);
        }

        public void Run()
        {
            _context.Run();
        }

        public void Unload()
        {
            _context.Unload();
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
            Unload();
            Script = new Script();
            Script.Globals["ui"] = new GuiLuaManager(ScriptLoader.ScreenManager.InternalGame.GuiManager, ScriptLoader.ScreenManager.InternalGame);
        }

        public void Unload()
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
            var context = Script.Globals["context"];
            Load();
            SetScriptContext(context);
            Run();
        }

        public void Run()
        {
            Script.DoFile(CurrentScriptPath);
        }

        public void SetScriptContext(object context)
        {
            Script.Globals["context"] = context;
        }
    }
}
