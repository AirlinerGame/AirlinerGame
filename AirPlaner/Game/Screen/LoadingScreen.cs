using System;
using System.ComponentModel;
using System.Linq;
using AirPlaner.Game.Screen.State;
using AirPlaner.Screen;
using Microsoft.Xna.Framework;
using TomShane.Neoforce.Controls;

namespace AirPlaner.Game.Screen
{
    public class LoadingScreen : GameScreen
    {
        private readonly bool _loadingIsSlow;
        private bool _otherScreensAreGone;
        private bool _loadingStuff;

        private int _progress;

        public string Message { get; set; }

        private readonly GameScreen _screen;

        private LoadingScreen(ScreenManager screenManager, bool loadingIsSlow, GameScreen screen)
        {
            _loadingIsSlow = loadingIsSlow;
            _screen = screen;
            Message = strings.txtLoading;

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
        }

        public static void Load(ScreenManager screenManager, GameScreen screen, string message = "Loading...")
        {
            foreach (GameScreen gameScreen in screenManager.GetScreens())
            {
                gameScreen.ExitScreen();
            }
            screenManager.ScriptLoader.Unload();

            bool loadingIsSlow = screen.GetType().GetInterfaces().Contains(typeof (IAsyncLoadable));
            LoadingScreen loadingScreen = new LoadingScreen(screenManager, loadingIsSlow, screen) { Message = strings.txtLoading };
            screenManager.AddScreen(loadingScreen);
        }

        /// <summary>
        /// This method gets called with every update call. All screens get called! Even the not visible one.
        /// 
        /// To update just the visible one use HandleInput()
        /// </summary>
        /// <param name="gameTime">Current GameTime</param>
        /// <param name="otherScreenHasFocus">Is another screen having focus?</param>
        /// <param name="coveredByOtherScreen">Depending on covered or not the screen will be visible. Pass true to always be visible in the background</param>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (_otherScreensAreGone)
            {
                if (_loadingIsSlow && !_loadingStuff)
                {
                    var manager = ScreenManager.InternalGame.GuiManager;
                    var textLabel = new Label(manager);
                    textLabel.Color = Color.White;
                    textLabel.Text = Message;
                    textLabel.Top = manager.ScreenHeight/2 - 100;
                    textLabel.Width = 200;

                    var progressBar = new ProgressBar(manager);
                    progressBar.Mode = ProgressBarMode.Default;
                    progressBar.Range = 100;
                    progressBar.Value = 0;
                    progressBar.Color = Color.Orange;
                    progressBar.Width = manager.ScreenWidth/2;
                    progressBar.Top = textLabel.Top + textLabel.Height + 5; 
                    progressBar.Left = manager.ScreenWidth/2 - progressBar.Width/2;
                    textLabel.Left = progressBar.Left;

                    manager.Add(textLabel);
                    manager.Add(progressBar);

                    var worker = new BackgroundWorker {WorkerReportsProgress = true};
                    var target = _screen as IAsyncLoadable;
                    worker.DoWork += delegate(object sender, DoWorkEventArgs args)
                    {
                        target.AsyncContentLoad(worker);
                    };
                    worker.ProgressChanged += delegate(object sender, ProgressChangedEventArgs args)
                    {
                        _progress = args.ProgressPercentage;
                        progressBar.Value = _progress;
                    };
                    worker.RunWorkerCompleted += delegate
                    {
                        SwitchState();
                    };
                    worker.RunWorkerAsync(ScreenManager);
                    _loadingStuff = true;
                }
                else if(!_loadingIsSlow)
                {
                    ScreenManager.RemoveScreen(this);
                    ScreenManager.AddScreen(_screen);
                }

                ScreenManager.Game.ResetElapsedTime();
            }
        }

        private void SwitchState()
        {
            ScreenManager.RemoveScreen(this);
            ScreenManager.AddScreen(_screen);
        }

        public override void Draw(GameTime gameTime)
        {
            if ((ScreenState == ScreenState.Active) && (ScreenManager.GetScreens().Length == 1))
            {
                _otherScreensAreGone = true;
            }

        }
    }
}
