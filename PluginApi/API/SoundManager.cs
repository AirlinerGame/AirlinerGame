using System;
using System.Collections.Generic;
using Airliner.Plugin.Entities;
using Airliner.Plugin.Entities.Audio;

namespace Airliner.Plugin.API
{
    public delegate void SoundPlayerChanged(object sender, SoundPlayerEventArgs eventArgs);

    public delegate void MusicTrackChanged(object sender, MusicTrackEventArgs eventArgs);

    public class SoundManager
    {
        private static SoundManager _instance;
        private MusicTrack _currentTrack;
        private long _currentPosition;
        private int _musicVolume;
        private int _soundEffectVolume;

        public event MusicTrackChanged TrackChanged;
        public event EventHandler MusicVolumeChanged;
        public event EventHandler SoundEffectVolumeChanged;
        public event EventHandler PositionChanged;
        public event SoundPlayerChanged PlayerStateChanged;

        public bool PlayingMusic { get; set; }
        public Queue<MusicTrack> TrackQueue { get; set; }

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

        public int MusicVolume
        {
            get { return _musicVolume; }
            set
            {
                if (MusicVolumeChanged != null)
                {
                    MusicVolumeChanged(this, EventArgs.Empty);
                }
                _musicVolume = value;
            }
        }

        public int SoundEffectVolume
        {
            get { return _soundEffectVolume; }
            set
            {
                if (SoundEffectVolumeChanged != null)
                {
                    SoundEffectVolumeChanged(this, EventArgs.Empty);
                }
                _soundEffectVolume = value;
            }
        }

        public void PlaySoundtrack(string path)
        {
            //todo: Implement
        }

        #region Handle MusicTracks
        public void StartMusic()
        {
            if (!PlayingMusic)
            {
                if (PlayerStateChanged != null)
                {
                    PlayerStateChanged(this, new SoundPlayerEventArgs {NewState = SoundPlayerState.Playing});
                }
            }
        }

        public void StopMusic()
        {
            if (PlayingMusic)
            {
                if (PlayerStateChanged != null)
                {
                    PlayerStateChanged(this, new SoundPlayerEventArgs { NewState = SoundPlayerState.Stopped });
                }
            }
        }

        public void PauseMusic()
        {
            if (PlayingMusic)
            {
                if (PlayerStateChanged != null)
                {
                    PlayerStateChanged(this, new SoundPlayerEventArgs { NewState = SoundPlayerState.Paused });
                }
            }
        }

        #endregion

        #region Singleton Methods

        private SoundManager()
        {
            TrackQueue = new Queue<MusicTrack>();
        }

        public static SoundManager Instance
        {
            get { return _instance ?? (_instance = new SoundManager()); }
        }

        #endregion

    }
}
