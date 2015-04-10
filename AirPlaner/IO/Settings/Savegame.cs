using System;
using AirPlaner.Config.Entity;
using Microsoft.Xna.Framework.Graphics;

namespace AirPlaner.IO.Settings
{
    [Serializable]
    public class Savegame
    {
        [NonSerialized] 
        public SettingsManager SettingsManager;

        public UserDetails Player;
        public AirlineDetails Airline;
        public TurnLength TurnLength;

        public string Filepath;

        public Savegame(SettingsManager settingsManager)
        {
            SettingsManager = settingsManager;

            Player = new UserDetails(settingsManager.Game.GraphicsDevice);
            Airline = new AirlineDetails(settingsManager.Game.GraphicsDevice);
        }

        public void SetGraphicsDevice(GraphicsDevice graphicsDevice)
        {
            Player.SetGraphicsDevice(graphicsDevice);
            Airline.SetGraphicsDevice(graphicsDevice);
        }
    }
}