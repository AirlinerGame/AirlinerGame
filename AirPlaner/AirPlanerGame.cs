using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Airliner.Plugin.API;
using Airliner.Plugin.Entities.Audio;
using AirPlaner.Config.Entity;
using AirPlaner.Game.Screen;
using AirPlaner.IO.Settings;
using AirPlaner.Screen;
using AirPlaner.UI.Components;
using FMOD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MoonSharp.Interpreter;
using TomShane.Neoforce.Controls;
using EventArgs = System.EventArgs;

namespace AirPlaner
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class AirPlanerGame : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private readonly float _scaleRatio;

        public ScreenManager ScreenManager { get; set; }
        private ScreenFactory _screenFactory;
        public Manager GuiManager { get; set; }

        public SettingsManager UserSettings { get; private set; }

        public Dictionary<int, Language> AvailableLanguages { get; set; }

        private FMOD.System _fmodsystem;
        private Channel _fmodchannel;
        private ChannelGroup _fmodChannelGroup;
        private bool _playing;
        private Sound _fmodsound;

        static readonly Random Random = new Random();

        public AirPlanerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            //Window.IsBorderless = true;
            Content.RootDirectory = "Content";

            //ApplicationSettings.Instance.FullScreen = true;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;

            //_graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            AvailableLanguages = new Dictionary<int, Language>
            {
                {0, new Language {CultureCode = "en", Name = "English"}},
                {1, new Language {CultureCode = "de", Name = "Deutsch"}}
            };

            _scaleRatio = _graphics.PreferredBackBufferWidth/1920f;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _screenFactory = new ScreenFactory();
            Services.AddService(typeof (IScreenFactory), _screenFactory);

            Thread.CurrentThread.CurrentUICulture =
                CultureInfo.GetCultureInfo(ApplicationSettings.Instance.SelectedLanguage);

            ScreenManager = new ScreenManager(this);
            Components.Add(ScreenManager);

            UserSettings = new SettingsManager(this);

            GuiManager = new Manager(this, _graphics);
            GuiManager.Skin = new Skin(GuiManager, "Default");
            GuiManager.AutoCreateRenderTarget = true;

            GuiManager.Initialize();
            GuiManager.ShowSoftwareCursor = true;

            ScreenManager.ScriptLoader.LoadPlugins();

            UserData.RegisterAssembly();
            UserData.RegisterType<Button>();
            UserData.RegisterType<Control>();
            UserData.RegisterType<Container>();
            UserData.RegisterType<ComboBox>();
            UserData.RegisterType<GroupPanel>();
            UserData.RegisterType<ImageBox>();
            UserData.RegisterType<GroupBox>();
            UserData.RegisterType<Label>();
            UserData.RegisterType<Panel>();
            UserData.RegisterType<SideBar>();
            UserData.RegisterType<TextBox>();
            UserData.RegisterType<CheckBox>();
            UserData.RegisterType<TrackBar>();
            UserData.RegisterType<Window>();

            UserData.RegisterType<Color>();
            UserData.RegisterType<Texture2D>();
            UserData.RegisterType<SerializableTexture2D>();

            UserData.RegisterType<BackgroundScene>();
            UserData.RegisterType<CreateGameScreen>();
            UserData.RegisterType<AirlinerGameScreen>();
            UserData.RegisterType<TurnLength>();
            UserData.RegisterType<UserDetails>();
            UserData.RegisterType<AirlineDetails>();
            UserData.RegisterType<ImageButton>();
            UserData.RegisterType<MoneyLabel>();
            UserData.RegisterType<NotifyIcon>();
            UserData.RegisterType<HeaderLabel>();
            UserData.RegisterType<Savegame>();
            UserData.RegisterType<MusicPlayer>();
            UserData.RegisterType<GameDatabaseSelectionDialog>();

            AddInitialScreens();

            //Create FMOD System
            Factory.System_Create(out _fmodsystem);

            //Initialise FMOD
            _fmodsystem.init(32, INITFLAGS.NORMAL, (IntPtr)null);

            _fmodsystem.createChannelGroup("mychannel", out _fmodChannelGroup);

            Window.Position = new Point(0,0);

            base.Initialize();
        }

        private void AddInitialScreens()
        {
            ScreenManager.AddScreen(new BackgroundScene());
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var files = Directory.GetFiles(Path.Combine("Content", "Music"), "*.ogg");
            Shuffle(files);

            SoundManager.Instance.MusicVolume = ApplicationSettings.Instance.MusicVolume;
            SoundManager.Instance.SoundEffectVolume = ApplicationSettings.Instance.FxVolume;

            SoundManager.Instance.TrackQueue = new Queue<MusicTrack>();

            foreach (var file in files)
            {
                MusicTrack musicTrack = new MusicTrack();
                musicTrack.Path = file;

                var last = file.Split(Path.DirectorySeparatorChar).Last();
                var imagePath = Path.Combine("Content", "Music", last.Substring(0, last.Length - 4) + ".jpg");
                if (File.Exists(imagePath))
                {
                    musicTrack.ImagePath = imagePath;
                }
                var splitted = last.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (splitted.Length == 2)
                {
                    musicTrack.Artist = Transform(splitted[0]);
                    musicTrack.Title = Transform(splitted[1]).Substring(0, splitted[1].Length - 4);
                }

                SoundManager.Instance.TrackQueue.Enqueue(musicTrack);
            }

            SoundManager.Instance.PlayerStateChanged += MusicPlayerStateChanged;
            SoundManager.Instance.MusicVolumeChanged += MusicPlayerVolumeChanged;

        }

        private void MusicPlayerVolumeChanged(object sender, EventArgs e)
        {
            if (_fmodchannel != null)
            {
                var musicVolume = (float)SoundManager.Instance.MusicVolume;
                if (Math.Abs(musicVolume) > 0.00f)
                {
                    _fmodchannel.setVolume(musicVolume / 100);
                }
                ApplicationSettings.Instance.MusicVolume = SoundManager.Instance.MusicVolume;
            }
        }

        private void MusicPlayerStateChanged(object sender, SoundPlayerEventArgs eventArgs)
        {
            switch (eventArgs.NewState)
            {
                case SoundPlayerState.Playing:
                    bool isPaused;
                    _fmodchannel.getPaused(out isPaused);
                    if (isPaused)
                    {
                        _fmodchannel.setPaused(false);
                    }
                    else
                    {
                        _fmodsystem.playSound(_fmodsound, _fmodChannelGroup, false, out _fmodchannel);                        
                    }
                    break;
                case SoundPlayerState.Paused:
                    _fmodchannel.setPaused(true);
                    break;
                case SoundPlayerState.Stopped:
                    _fmodchannel.stop();
                    break;
            }
        }

        private string Transform(string value)
        {
            return value.Replace("_", " ");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F7))
            {
                foreach (var gameScreen in ScreenManager.GetScreens())
                {
                    gameScreen.ReloadInterface();
                }
            }

            if (ApplicationSettings.Instance.MusicEnabled)
            {
                if (_fmodchannel != null)
                {
                    _fmodchannel.isPlaying(out _playing);
                    SoundManager.Instance.PlayingMusic = _playing;
                }

                if (!_playing && SoundManager.Instance.TrackQueue.Count > 0)
                {
                    var musicTrack = SoundManager.Instance.TrackQueue.Dequeue();
                    _fmodsystem.createSound(musicTrack.Path, MODE.DEFAULT, out _fmodsound);
                    uint length;
                    _fmodsound.getLength(out length, TIMEUNIT.MS);
                    musicTrack.Length = length;
                    _fmodsystem.playSound(_fmodsound, _fmodChannelGroup, false, out _fmodchannel);
                    _fmodchannel.setVolume((float)SoundManager.Instance.MusicVolume/100);
                    SoundManager.Instance.CurrentTrack = musicTrack;
                }

                if (_playing)
                {
                    uint position;
                    _fmodchannel.getPosition(out position, TIMEUNIT.MS);
                    SoundManager.Instance.CurrentPosition = position;
                }
            }

            GuiManager.Update(gameTime);
            UserSettings.Tick(gameTime);

            base.Update(gameTime);
        }

        protected override void EndDraw()
        {
            GuiManager.EndDraw();
            base.EndDraw();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);

            GuiManager.BeginDraw(gameTime);
            GuiManager.Draw(gameTime);

            base.Draw(gameTime);
        }

        static void Shuffle<T>(T[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n; i++)
            {
                // NextDouble returns a random number between 0 and 1.
                // ... It is equivalent to Math.random() in Java.
                int r = i + (int)(Random.NextDouble() * (n - i));
                T t = array[r];
                array[r] = array[i];
                array[i] = t;
            }
        }
    }
}