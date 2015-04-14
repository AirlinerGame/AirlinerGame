using System;
using System.Data;
using Airliner.Plugin.Entities.Game;

namespace Airliner.Plugin.API
{
    public delegate void GameStateChangedEventHandler(object sender, GameStateEventArgs eventArgs);

    public class GameManager
    {
        #region Fields
        private bool _gameRunning;
        private GameDatabase _database;
        #endregion

        #region Events
        public event GameStateChangedEventHandler GameStateChanged;
        public event EventHandler DatabaseLoaded;
        #endregion

        #region Properties

        public bool GameRunning
        {
            get { return _gameRunning; }
            set
            {
                if (GameStateChanged != null)
                {
                    GameStateChanged(this, new GameStateEventArgs {NewState = (_gameRunning)?GameState.Stopped : GameState.Started});
                }
                _gameRunning = value;
            }
        }

        public GameDatabase ActiveDatabase
        {
            get { return _database; }
            set
            {
                if (DatabaseLoaded != null)
                {
                    DatabaseLoaded(this, EventArgs.Empty);
                }
                _database = value;
            }
        }

        public GameData CurrentGameData { get; set; }
        #endregion

        #region Singleton Methods
        private static GameManager _instance;

        private GameManager()
        {
            
        }

        public static GameManager Instance
        {
            get { return _instance ?? (_instance = new GameManager()); }
        }
        #endregion
    }
}
