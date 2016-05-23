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
        private static MyGameWindow gameWindow;

        public static MyGameWindow GameWindow
        {
            get { return gameWindow; }
        }

        [STAThread]
        public static void Main()
        {
            windowState = WindowState.Normal;
            //windowState = WindowState.Normal;

            if (windowState == WindowState.Fullscreen)
                gameWindow = new MyGameWindow(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
            else
                gameWindow = new MyGameWindow(800, 700);


            //win.CurrentScreen = new GameScreen(win);
            gameWindow.CurrentScreen = new MainMenuScreen();

            if (windowState == WindowState.Fullscreen) gameWindow.WindowState = WindowState.Fullscreen;

            System.Diagnostics.Debug.WriteLine("win.width" + gameWindow.Width);
            System.Diagnostics.Debug.WriteLine("win.Height" + gameWindow.Height);

            gameWindow.Run(60.0);

        }

        public static void Exit()
        {
            gameWindow.Exit();
        }


	}
}