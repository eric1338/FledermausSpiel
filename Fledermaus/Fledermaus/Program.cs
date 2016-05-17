using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using Framework;
using System.Collections.Generic;
using Fledermaus.Utils;
using Fledermaus.Screens;

namespace Fledermaus
{
	class MyApplication
	{

		[STAThread]
		public static void Main()
		{
			MyGameWindow win = new MyGameWindow();

            //win.CurrentScreen = new GameScreen(win);
            win.CurrentScreen = new MainMenuScreen(win);

			win.Run(60.0);
		}

	}
}