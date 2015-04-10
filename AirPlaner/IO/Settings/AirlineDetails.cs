using System;
using AirPlaner.UI.Components;
using Microsoft.Xna.Framework.Graphics;

namespace AirPlaner.IO.Settings
{
    [Serializable]
    public class AirlineDetails
    {
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