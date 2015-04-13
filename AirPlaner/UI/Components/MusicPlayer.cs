using System;
using System.IO;
using Airliner.Plugin.Entities;
using Microsoft.Xna.Framework.Graphics;
using TomShane.Neoforce.Controls;

namespace AirPlaner.UI.Components
{
    public class MusicPlayer : Container
    {
        private readonly ImageBox _imageBox;
        private readonly Label _artistLabel;
        private readonly Label _titleLabel;
        private readonly Label _currentTime;
        private readonly Label _trackLength;
        private MusicTrack _currentTrack;
        private long _currentPosition;

        public long CurrentPosition
        {
            get
            {
                return _currentPosition;
            }
            set
            {
                _currentPosition = value;
                var timeSpan = TimeSpan.FromMilliseconds(value);
                _currentTime.Text = timeSpan.ToString("%m\\:ss");
            }
        }

        public MusicTrack CurrentTrack
        {
            get { return _currentTrack; }
            set
            {
                _currentTrack = value;
                SetTrackValues();
            }
        }

        public Texture2D Cover
        {
            get { return _imageBox.Image; }
            set { _imageBox.Image = value; }
        }

        public MusicPlayer(Manager manager) : base(manager)
        {
            _imageBox = new ImageBox(manager)
            {
                SizeMode = SizeMode.Stretched,
                Left = 5,
                Color = Microsoft.Xna.Framework.Color.White
            };
            _artistLabel = new Label(manager);
            _titleLabel = new Label(manager);
            _currentTime = new Label(manager);
            _trackLength = new Label(manager);
            Add(_imageBox);
            Add(_artistLabel);
            Add(_titleLabel);
            Add(_currentTime);
            Add(_trackLength);
        }

        private void CalculatePositions()
        {
            _artistLabel.Width = Width;
            _titleLabel.Width = Width;
            _artistLabel.Left = _imageBox.Left + _imageBox.Width + 5;
            _titleLabel.Left = _imageBox.Left + _imageBox.Width + 5;
            _artistLabel.Top = _imageBox.Top;
            _titleLabel.Top = _artistLabel.Top + _artistLabel.Height + 5;
            _currentTime.Width = _currentTime.Text.Length*5;
            _currentTime.Left = _artistLabel.Left;
            _currentTime.Top = _titleLabel.Top + 20;
            _trackLength.Top = _currentTime.Top;
            _trackLength.Left = _currentTime.Left + _currentTime.Width;
        }

        private void SetTrackValues()
        {
            _artistLabel.Text = CurrentTrack.Artist;
            _titleLabel.Text = CurrentTrack.Title;
            if (CurrentTrack.ImagePath != null)
            {
                using (FileStream fs = new FileStream(CurrentTrack.ImagePath, FileMode.Open, FileAccess.Read))
                {
                    Cover = Texture2D.FromStream(Manager.GraphicsDevice, fs);
                } 
            }

            TimeSpan ts = new TimeSpan(CurrentTrack.Length * 10000);
            _trackLength.Text = "/" + ts.ToString("%m\\:ss");
            _currentTime.Text = "0:00";
        }

        public override int Height
        {
            get { return base.Height; }
            set
            {
                if (_imageBox != null)
                {
                    _imageBox.Height = value - 10;
                    _imageBox.Width = value - 10;
                    _imageBox.Top = value / 2 - (value - 10) / 2;
                    CalculatePositions();
                }
                base.Height = value;
            }
        }

    }
}
