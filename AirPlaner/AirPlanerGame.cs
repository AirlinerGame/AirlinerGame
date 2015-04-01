using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading;
using AirPlaner.Config.Entity;
using AirPlaner.Game.Screen;
using AirPlaner.IO.Settings;
using AirPlaner.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TomShane.Neoforce.Controls;

namespace AirPlaner
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class AirPlanerGame : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _backgroundTexture2D;
        private readonly float _scaleRatio;

        private ScreenManager _screenManager;
        private ScreenFactory _screenFactory;
        public Manager GuiManager { get; set; }

        public Dictionary<int, Language> AvailableLanguages { get; set; }

        public AirPlanerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            AvailableLanguages = new Dictionary<int, Language>
            {
                {0, new Language {CultureCode = "en", Name = "English"}},
                {1, new Language {CultureCode = "de", Name = "Deutsch"}}
            };

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

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

            _screenManager = new ScreenManager(this);
            Components.Add(_screenManager);
            GuiManager = new Manager(this, _graphics);
            GuiManager.Skin = new Skin(GuiManager, "Purple");
            GuiManager.AutoCreateRenderTarget = true;

            GuiManager.Initialize();
            GuiManager.ShowSoftwareCursor = true;

            AddInitialScreens();


            base.Initialize();
        }

        private void AddInitialScreens()
        {
            _screenManager.AddScreen(new BackgroundScene());
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _backgroundTexture2D = Content.Load<Texture2D>("AirlinerBG");
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
            //Keyboard.Update(gameTime);

            GuiManager.Update(gameTime);

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
    }
}