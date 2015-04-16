using System;

namespace Airliner.Plugin.Entities.Game.Database
{
    [Serializable]
    public class Airline
    {
        public string Name { get; set; }
        public Airport Headquarter { get; set; }
        

        public override string ToString()
        {
            return Name;
        }
    }
}