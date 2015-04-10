using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TomShane.Neoforce.Controls;

namespace AirPlaner.Screen
{
    public class ClosableGameScreen : GameScreen
    {
        public Window MainMenu { get; set; }

        public override void Activate(bool instancePreserved)
        {
            ScreenManager.ScriptLoader.Include("Content/UI/Modules/GameMainMenu.lua");
            MainMenu.Visible = false;
            MainMenu.AutoScroll = false;
            MainMenu.StayOnTop = true;

            base.Activate(instancePreserved);
        }

        public void MenuResumeGameButtonOnClick(object sender, EventArgs eventArgs)
        {
            MainMenu.Visible = false;
            MainMenu.Close();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                if (!MainMenu.Visible)
                {
                    MainMenu.Visible = true;
                    MainMenu.Show();
                    MainMenu.BringToFront();
                }
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void ReloadInterface()
        {
            base.ReloadInterface();

            ScreenManager.ScriptLoader.Include("Content/UI/Modules/GameMainMenu.lua");
            MainMenu.Visible = false;
        }
    }
}
