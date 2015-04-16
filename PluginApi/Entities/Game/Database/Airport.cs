using System;
using System.Collections.Generic;

namespace Airliner.Plugin.Entities.Game.Database
{
    [Serializable]
    public class Airport
    {
        public string Name { get; set; }
        public City Location { get; set; }
        public List<Runway> Runways { get; private set; }
        public string ICAO { get; set; }
        public string IATA { get; set; }
        public long Passengers { get; set; }

        public Airport()
        {
            Runways = new List<Runway>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}