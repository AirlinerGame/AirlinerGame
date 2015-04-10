using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Xna.Framework;

namespace AirPlaner.IO.Settings
{
    public class SettingsManager
    {
        public AirPlanerGame Game { get; private set; }

        public bool AutoSave { get; set; }

        public Savegame Settings { get; private set; }

        public SettingsManager(AirPlanerGame game)
        {
            Game = game;
            AutoSave = false;
            Settings = new Savegame(this);
            //todo: Workaround until we have a SaveFileDialog
            Settings.Filepath = "Savegames/Temp.savegame";
        }

        public void Load(string filepath)
        {
            using (var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                var formatter = new BinaryFormatter();
                var settings = formatter.Deserialize(fs) as Savegame;
                Settings = settings;
                Settings.SettingsManager = this;
                Settings.SetGraphicsDevice(Game.GraphicsDevice);
            }
        }

        public void Save(string filepath)
        {
            MemoryStream m = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(m, Settings);
            File.WriteAllBytes(Settings.Filepath, m.ToArray());
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