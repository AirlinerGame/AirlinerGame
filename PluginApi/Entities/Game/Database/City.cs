namespace Airliner.Plugin.Entities.Game.Database
{
    public class City
    {
        public string Name { get; set; }
        public int Population { get; set; }
        public GeoCoordinate Coordinates { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}