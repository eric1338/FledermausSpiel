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

			Data.PlayerData.WriteXML();

			// TODO: anders
			Data.PlayerData.Instance.CreateLevelHighscores("Level 1", 5);
			Data.PlayerData.Instance.CreateLevelHighscores("Level 2", 5);
			Data.PlayerData.Instance.CreateLevelHighscores("Level 3", 5);

			windowState = WindowState.Normal;

			int width;
			int height;

            if (windowState == WindowState.Fullscreen)
			{
				width = SystemInformation.VirtualScreen.Width;
				height = SystemInformation.VirtualScreen.Height;
			}
            else
			{
				width = 1280;
				height = 800;
			}

			System.Diagnostics.Debug.WriteLine(width + " / " + height);

			gameWindow = new MyGameWindow(width, height);

			//win.CurrentScreen = new GameScreen(win);
			gameWindow.CurrentScreen = new MainMenuScreen();

            if (windowState == WindowState.Fullscreen) gameWindow.WindowState = WindowState.Fullscreen;

			BasicGraphics.WindowWidth = gameWindow.Width;
			BasicGraphics.WindowHeight = gameWindow.Height;

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