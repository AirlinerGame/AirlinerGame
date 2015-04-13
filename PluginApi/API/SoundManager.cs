using System;
using System.Collections.Generic;
using Airliner.Plugin.Entities;

namespace Airliner.Plugin.API
{
    public delegate void SoundPlayerChanged(object sender, SoundPlayerEventArgs eventArgs);

    public delegate void MusicTrackChanged(object sender, MusicTrackEventArgs eventArgs);

    public class SoundManager
    {
        private static SoundManager _instance;
        private MusicTrack _currentTrack;
        private long _currentPosition;

        public event MusicTrackChanged TrackChanged;
        public event EventHandler VolumeChanged;
        public event EventHandler PositionChanged;
        public event SoundPlayerChanged PlayerStateChanged;

        public bool PlayingMusic { get; set; }

        public long CurrentPosition
        {
            get { return _currentPosition; }
            set
            {
                _currentPosition = value;
                if (PositionChanged != null)
                {
                    PositionChanged(this, EventArgs.Empty);
                }
            }
        }

        public Queue<MusicTrack> TrackQueue { get; set; }

        public MusicTrack CurrentTrack
        {
            get { return _currentTrack; }
            set
            {
                if (TrackChanged != null)
                {
                    TrackChanged(this, new MusicTrackEventArgs {Track = value});
                }
                _currentTrack = value;
            }
        }

        public float MusicVolume { get; set; }
        public float SoundEffectVolume { get; set; }

        private SoundManager()
        {
            TrackQueue = new Queue<MusicTrack>();
        }



        public static SoundManager Instance
        {
            get { return _instance ?? (_instance = new SoundManager()); }
        }

    }
}
