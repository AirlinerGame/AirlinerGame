using System.Collections.Generic;
using System.Runtime.InteropServices;
using Airliner.Plugin.Entities.Game.Database;

namespace Airliner.Plugin.Entities.Game
{
    public class GameDatabase
    {

        public List<Airline> Airlines { get; private set; }
        public List<Airport> Airports { get; private set; }
        public List<City> Cities { get; private set; }
        public List<GameEvent> Events { get; private set; }

        public GameDatabase()
        {
            Airlines = new List<Airline>();
            Airports = new List<Airport>();
            Cities = new List<City>();
            Events = new List<GameEvent>();
        }

    }
}
