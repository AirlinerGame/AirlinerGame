﻿using System;
using Microsoft.Xna.Framework.Graphics;

namespace AirPlaner.IO.Settings
{
    [Serializable]
    public class Savegame
    {
        public SettingsManager SettingsManager { get; private set; }

        public UserDetails Player;
        public AirlineDetails Airline;

        public Savegame()
        {
            
        }

        public Savegame(SettingsManager settingsManager)
        {
            SettingsManager = settingsManager;

            Player = new UserDetails();
            Airline = new AirlineDetails();
        }

    }
}