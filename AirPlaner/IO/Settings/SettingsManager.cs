﻿using Microsoft.Xna.Framework;

namespace AirPlaner.IO.Settings
{
    public class SettingsManager
    {
        public AirPlanerGame Game { get; private set; }

        public bool AutoSave { get; set; }

        public SettingsManager(AirPlanerGame game)
        {
            AutoSave = false;
            Game = game;
        }

        public void Load(string filepath)
        {
            
        }

        public void Save(string filepath)
        {
            
        }

        public void Tick(GameTime gameTime)
        {
            if (AutoSave)
            {
                //AutoSave Logic
            }
        }

    }
}