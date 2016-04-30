using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using Framework;
using System.Collections.Generic;

namespace Fledermaus
{
	class MyApplication
	{

		/*
		* Relative oder absolute Koordinaten?
		*
		*/

		[STAThread]
		public static void Main()
		{
			MyGameWindow win = new MyGameWindow();

			win.CurrentScreen = new GameScreen();

			win.Run(60.0);
		}

	}
}