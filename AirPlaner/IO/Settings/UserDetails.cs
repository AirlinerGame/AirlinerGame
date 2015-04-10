using System;
using AirPlaner.UI.Components;
using Microsoft.Xna.Framework.Graphics;

namespace AirPlaner.IO.Settings
{
    [Serializable]
    public class UserDetails
    {
        public SerializableTexture2D PlayerPicture;
        public string Firstname;
        public string Lastname;

        public UserDetails(GraphicsDevice graphicsDevice)
        {
            PlayerPicture = new SerializableTexture2D(graphicsDevice);
        }

        public void SetGraphicsDevice(GraphicsDevice graphicsDevice)
        {
            PlayerPicture.GraphicsDevice = graphicsDevice;
        }
    }
}