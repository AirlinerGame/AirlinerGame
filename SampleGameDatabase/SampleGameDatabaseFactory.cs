using Airliner.Plugin.Entities.Game;
using Airliner.Plugin.Entities.Game.Database;

namespace SampleGameDatabase
{
    public class SampleGameDatabaseFactory : IGameDatabaseProvider
    {
        public string GetName()
        {
            return "Simple Sample Database";
        }

        public GameDatabase CreateDatabase()
        {
            var gameDatabase = new GameDatabase();

            var koblenz = new City
            {
                Name = "Koblenz",
                Population = 110000,
                Coordinates = new GeoCoordinate(50.356667, 7.593889)
            };

            var berlin = new City
            {
                Name = "Berlin",
                Population = 3440991,
                Coordinates = new GeoCoordinate(52.518611, 13.408333)
            };

            gameDatabase.Cities.Add(koblenz);
            gameDatabase.Cities.Add(berlin);

            var airportBerlin = new Airport
            {
                Name = "Berlin Tegel",
                ICAO = "EDDT",
                IATA = "TXL",
                Location = berlin,
                Passengers = 20688016
            };
            airportBerlin.Runways.Add(new Runway { Name = "08L/26R", LengthInFeet = 9918});
            airportBerlin.Runways.Add(new Runway { Name = "08R/26L", LengthInFeet = 7966});

            gameDatabase.Airports.Add(airportBerlin);

            var airBerlin = new Airline
            {
                Name = "Air Berlin",
                Headquarter = airportBerlin
            };
            gameDatabase.Airlines.Add(airBerlin);


            return gameDatabase;
        }
    }
}
