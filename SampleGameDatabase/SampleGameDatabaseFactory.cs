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
            return gameDatabase;
        }
    }
}
