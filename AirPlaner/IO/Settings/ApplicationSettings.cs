using System;
using System.IO;
using System.Xml.Serialization;

namespace AirPlaner.IO.Settings
{
    [Serializable]
    [XmlRoot("ApplicationSettings")]
    public class ApplicationSettings
    {
        private static readonly XmlSerializer Serial = new XmlSerializer(typeof(ApplicationSettings));
        private static ApplicationSettings _instance;
        public static ApplicationSettings Instance
        {
            get
            {
                if (_instance != null) return _instance;
                if (File.Exists("Config/ApplicationSettings.xml"))
                {
                    using (var sr = new StreamReader("Config/ApplicationSettings.xml")) return _instance = (ApplicationSettings)Serial.Deserialize(sr);
                }
                return null;
            }
        }

        public ApplicationSettings()
        {
            
        }

        public void Save()
        {
            using (var writer = new StreamWriter("Config/ApplicationSettings.xml"))
            {
                Serial.Serialize(writer, this);       
            }
        }

        public string SelectedLanguage { get; set; }
        public bool FullScreen { get; set; }
    }

}
