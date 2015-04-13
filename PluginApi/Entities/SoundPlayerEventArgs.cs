using System;

namespace Airliner.Plugin.Entities
{
    public class SoundPlayerEventArgs : EventArgs
    {
        public SoundPlayerState NewState { get; set; }
    }

    public enum SoundPlayerState
    {
        Playing, Paused, Stopped
    }
}
