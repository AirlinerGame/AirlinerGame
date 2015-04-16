using System;

namespace Airliner.Plugin.Entities.Game.Database
{
    [Serializable]
    public class Runway
    {
        private long _length;

        public string Name { get; set; }

        public long LengthInFeet
        {
            get { return _length; }
            set { _length = value; }
        }

        public long LengthInMeters
        {
            get { return (long) (_length*0.3048); }
            set { _length = (long) (value/0.3048); }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}