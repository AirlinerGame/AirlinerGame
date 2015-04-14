namespace Airliner.Plugin.Entities.Game.Database
{
    public interface IGameDatabaseProvider
    {
        string GetName();
        GameDatabase CreateDatabase();
    }
}
