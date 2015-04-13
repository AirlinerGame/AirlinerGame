using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TomShane.Neoforce.Controls;

namespace AirPlaner.UI.Components
{
    public class NotifyIcon : ImageBox
    {
        public int Value { get; set; }
        public Color BadgeColor { get; set; }
        public SpriteFont Font { get; set; }

        private Texture2D _dummyTexture;

        public NotifyIcon(Manager manager) : base(manager)
        {
            BadgeColor = Color.Red;
            SizeMode = SizeMode.Stretched;
            Font = Skin.Layers[0].Text.Font.Resource;
        }

        protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            base.DrawControl(renderer, rect, gameTime);

            if (_dummyTexture == null)
            {
                _dummyTexture = new Texture2D(renderer.SpriteBatch.GraphicsDevice, 1, 1);
                _dummyTexture.SetData(new Color[] { Color.White });
            }

            //Draw Notification
            if (Value > 0)
            {
                var badgeWidth = Width/2;
                var badgeHeight = Height/3;
                var rec = new Rectangle(rect.Right - badgeWidth, rect.Bottom - badgeHeight - 5, badgeWidth, badgeHeight);
                renderer.SpriteBatch.Draw(_dummyTexture, rec, BadgeColor);
                var stringSize = Font.MeasureString(Value.ToString());
                renderer.SpriteBatch.DrawString(Font, Value.ToString(), new Vector2(rec.Left + rec.Width / 2 - stringSize.X / 2, rec.Top + rec.Height / 2 - stringSize.Y / 2), Color.White);
            }
        }
    }
}
