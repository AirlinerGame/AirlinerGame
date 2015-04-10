using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AirPlaner.UI.Components
{
    [Serializable]
    public class SerializableTexture2D
    {
        [NonSerialized] 
        private Texture2D _image;
        [NonSerialized]
        public GraphicsDevice GraphicsDevice;


        public byte[] ImageData;
        public int Width;
        public int Height;

        public SerializableTexture2D(GraphicsDevice device)
        {
            GraphicsDevice = device;
        }


        public Texture2D Image
        {
            get { return _image ?? (_image = LoadTexture()); }
            set
            {
                SaveTexture(value);
                _image = value;
            }
        }

        private void SaveTexture(Texture2D value)
        {
            var memoryStream = new MemoryStream();
            memoryStream.Seek(0, SeekOrigin.Begin);
            value.SaveAsPng(memoryStream, Width, Height);
            ImageData = memoryStream.ToArray();
            memoryStream = null;
        }

        private Texture2D LoadTexture()
        {
            if (ImageData == null)
            {
                return null;
            }
            var memoryStream = new MemoryStream(ImageData);
            var texture = Texture2D.FromStream(GraphicsDevice, memoryStream);
            return texture;
        }

    }
}
