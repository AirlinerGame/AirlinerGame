using System;

namespace Airliner.Plugin.Entities.Game
{
    public class GameStateEventArgs : EventArgs
    {
        public GameState NewState { get; set; }
    }
}
