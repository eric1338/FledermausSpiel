using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using Framework;
using System.Collections.Generic;
using Fledermaus.Utils;
using Fledermaus.Screens;
using System.Windows.Forms;

namespace Fledermaus
{
	static class MyApplication
	{
        private static WindowState windowState;
        private static MyGameWindow win;

        public static MyGameWindow GameWindow
        {
            get { return win; }
        }
        [STAThread]
        public static void Main()
        {
            windowState = WindowState.Fullscreen;
            //windowState = WindowState.Normal;

            if (windowState == WindowState.Fullscreen)
                win = new MyGameWindow(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
            else
                win = new MyGameWindow(800, 700);


            //win.CurrentScreen = new GameScreen(win);
            win.CurrentScreen = new MainMenuScreen();
            if(windowState == WindowState.Fullscreen)
                win.WindowState = WindowState.Fullscreen;
            System.Diagnostics.Debug.WriteLine("win.width" + win.Width);
            System.Diagnostics.Debug.WriteLine("win.Height" + win.Height);

            win.Run(60.0);

        }
        public static void Exit()
        {
            win.Exit();
        }


	}
}