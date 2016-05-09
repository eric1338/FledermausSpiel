using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using Framework;
using System.Collections.Generic;
using Fledermaus.Screens;

namespace Fledermaus
{
	class MyApplication
	{

		/*
		* TODO: Mirror anpappen anpassen, Refactoring (static HasIntersection -> Util, anschl. Ray anpassen), Room.cs raus
		* Mirror Rückseite -> Spiegelung neu
		* Spiegelmethode neu (siehe Spiele2/slides/Shader Lighting)
		* bei GameObject-Klassen base(position) evtl raus
		*
		* Relative oder absolute Koordinaten?
		*
		*/

		[STAThread]
		public static void Main()
		{
			MyGameWindow win = new MyGameWindow();
           // win.WindowState = WindowState.Fullscreen;
            //win.CurrentScreen = new MainScreen(win);
			win.CurrentScreen = new GameScreen(win);

			win.Run(60.0);
		}

	}
}