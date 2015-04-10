using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TomShane.Neoforce.Controls;

namespace AirPlaner.UI.Components
{
    public class MoneyLabel : Label
    {
        private long _value;
        public long Value
        {
            get { return _value; }
            set
            {
                Text = value.ToString() + "$";
                _value = value;
            }
        }

        public bool AutoColor { get; set; }
        public Color PositiveColor { get; set; }
        public Color NegativeColor { get; set; }

        public MoneyLabel(Manager manager) : base(manager)
        {
            AutoColor = true;
            PositiveColor = Color.Lime;
            NegativeColor = Color.Red;
        }

        public override Color TextColor
        {
            get
            {
                if (AutoColor)
                {
                    return Value < 0 ? NegativeColor : PositiveColor;
                }
                return base.TextColor;
            }
            set { base.TextColor = value; }
        }
    }
}
