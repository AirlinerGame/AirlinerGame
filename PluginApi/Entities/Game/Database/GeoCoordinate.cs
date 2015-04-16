using System;

namespace Airliner.Plugin.Entities.Game.Database
{
    public struct GeoCoordinate : IEquatable<GeoCoordinate>
    {
        private readonly double _latitude;
        private readonly double _longitude;

        public double Latitude { get { return _latitude; } }
        public double Longitude { get { return _longitude; } }

        public GeoCoordinate(double latitude, double longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
        }

        public override bool Equals(Object other)
        {
            return other is GeoCoordinate && Equals((GeoCoordinate)other);
        }

        public bool Equals(GeoCoordinate other)
        {
            return _latitude == other.Latitude && _longitude == other.Longitude;
        }

        public override int GetHashCode()
        {
            return Latitude.GetHashCode() ^ Longitude.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", Latitude, Longitude);
        }
    }
}