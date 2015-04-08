using System;
using Microsoft.Xna.Framework.Graphics;

namespace AirPlaner.IO.Settings
{
    [Serializable]
    public class UserDetails
    {
        public Texture2D PlayerPicture;
        public string Firstname;
        public string Lastname;

        public UserDetails()
        {
            
        }
    }
}