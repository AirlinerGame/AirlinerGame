using System;
using AirPlaner.UI.Components;
using Microsoft.Xna.Framework.Graphics;

namespace AirPlaner.IO.Settings
{
    [Serializable]
    public class AirlineDetails
    {

        [NonSerialized]
        private EventHandler _moneyChanged;
        public event EventHandler MoneyChanged
        {
            add { _moneyChanged += value; }
            remove { _moneyChanged -= value; }
        }

        private long _money;

        public long Money
        {
            get { return _money; }
            set
            {
                _money = value;
                if (_moneyChanged != null)
                {
                    _moneyChanged(this, new EventArgs());
                }
            }
        }

        public SerializableTexture2D AirlinePicture;
        public string Name;


        public AirlineDetails(GraphicsDevice graphicsDevice)
        {
            AirlinePicture = new SerializableTexture2D(graphicsDevice);
        }

        public void SetGraphicsDevice(GraphicsDevice graphicsDevice)
        {
            AirlinePicture.GraphicsDevice = graphicsDevice;
        }
    }
}