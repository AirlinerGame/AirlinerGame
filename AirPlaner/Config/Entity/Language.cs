using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirPlaner.Config.Entity
{
    public class Language
    {
        public string CultureCode { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
