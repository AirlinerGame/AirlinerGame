using System;
using AirPlaner.IO;
using Microsoft.Xna.Framework;

namespace AirPlaner.UI
{
    public class GUIManager
    {
        public string Name
        {
            get { return "GUIManager"; }
        }

        public AirPlanerGame Engine { get; set; }

        public GUIManager(AirPlanerGame engine)
        {
            Engine = engine;
        }

        public void LoadScene()
        {


            //Make Mouse visible
            Engine.IsMouseVisible = true;

            //Engine.Keyboard.KeyUp += keyboard_KeyboardKeyUp;
        }

        public void Render(GameTime time)
        {
            
        }

        public void Update(GameTime time)
        {
            //Calculate Cursor Stuff here
        }

        public void UnloadScene()
        {
            //Not necassery since GUIManager will always run
        }

        public void KeyboardKeyboardKeyUp(object sender, EventArgs eventArgs)
        {
            KeyboardEventArgs eventA = (KeyboardEventArgs) eventArgs;
        }

        //WinAPI Calls here
        /*public static Cursor LoadCustomCursor(string path)
        {
            IntPtr hCurs = LoadCursorFromFile(path);
            //if (hCurs == IntPtr.Zero) throw new Win32Exception();
            var curs = new Cursor(hCurs);
            // Note: force the cursor to own the handle so it gets released properly
            var fi = typeof (Cursor).GetField("ownHandle", BindingFlags.NonPublic | BindingFlags.Instance);
            if (fi != null) fi.SetValue(curs, true);
            return curs;
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LoadCursorFromFile(string path);*/
    }
}