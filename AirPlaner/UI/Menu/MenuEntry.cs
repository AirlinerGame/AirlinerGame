using AirPlaner.Game.Physics;
using Microsoft.Xna.Framework;

namespace Slaysher.Game.GUI.Menu
{
    public class MenuEntry : Box
    {
        public string Text { get; set; }

        public MenuEntry(string text, Vector2 position, Vector2 size)
            : base(position, size)
        {
            Text = text;
        }
    }
}