using System;

namespace AirPlaner.Config.Entity
{
    [Serializable]
    public class TurnLength
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
