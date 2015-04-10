using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TomShane.Neoforce.Controls;

namespace AirPlaner.UI.Components
{
    public sealed class ImageButton : Control
    {
        private readonly ImageBox _image;
        private readonly Label _label;

        private Texture2D _border;
        private Color _borderColor;
        public Color HoverColor { get; set; }
        private Color _normalColor;

        public int Padding { get; set; }

        public ImageButton(Manager manager, AirPlanerGame game) : base(manager)
        {
            HoverColor = Color.Red;
            _normalColor = Color.Transparent;

            Padding = 15;
            _image = new ImageBox(manager) {Width = 16, Height = 16};
            _image.Color = Color.White;
            _image.SizeMode = SizeMode.Stretched;

            _label = new Label(manager);
            _label.Color = Color.White;
            var layer = _label.Skin.Layers[0];
            layer.Text.Font.Resource = game.Content.Load<SpriteFont>("font");
            _label.Text = "Test";
            
            _image.Init();
            _label.Init();

            _border = new Texture2D(Manager.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            _border.SetData(new[] { Color.White });

            Add(_image);
            Add(_label);

            Color = Color.Transparent;
        }

        public Texture2D Image
        {
            get { return _image.Image; }
            set
            {
                _image.Image = value;
                _image.Refresh();
            }
        }

        public override string Text
        {
            get { return _label.Text; }
            set
            {
                _label.Text = value; 
                _label.Refresh();
            }
        }

        public override int Left
        {
            get { return base.Left; }
            set
            {
                if (_image != null)
                {
                    _image.Left = Padding;
                    _label.Left = Padding + _image.Width + Padding;
                }
                base.Left = value;
            }
        }

        public override int Top
        {
            get { return base.Top; }
            set
            {
                if (_image != null)
                {
                    _image.Top = Height / 2 - _image.Height / 2;
                    _label.Top = Height / 2 - _label.Height / 2;
                }
                base.Top = value;
            }
        }

        public override int Width
        {
            get { return base.Width; }
            set
            {
                if (_label != null)
                {
                    _label.Width = value - _image.Width - Padding * 3;                    
                }
                base.Width = value;
            }
        }

        public override int Height
        {
            get { return base.Height; }
            set
            {
                if (_label != null)
                {
                    _label.Height = value - 20;                    
                }
                base.Height = value;
            }
        }

        public override Color TextColor
        {
            get { return _label.TextColor; }
            set
            {
                if (_label != null)
                {
                    _label.TextColor = value;                    
                }
                base.TextColor = value;
            }
        }

        public override Color Color
        {
            get { return base.Color; }
            set
            {
                _borderColor = Color.Lerp(value, Color.Black, 0.80f);
                if (!value.Equals(HoverColor))
                {
                    _normalColor = value;                    
                }
                base.Color = value;
            }
        }

        protected override void DrawControl(Renderer renderer, Rectangle rect, GameTime gameTime)
        {
            //Draw Border
            //spriteBatch.Draw(SimpleTexture, new Rectangle(20, 50, 100, 1), Color.Blue);
            if (Hovered)
            {
                Color = HoverColor;
            }
            else
            {
                if (Color.Equals(HoverColor))
                {
                    Color = _normalColor;
                }
            }
            base.DrawControl(renderer, rect, gameTime);
            renderer.SpriteBatch.Draw(_border, new Rectangle(Left, rect.Bottom - 1, Width, 1), _borderColor);
        }
    }
}
