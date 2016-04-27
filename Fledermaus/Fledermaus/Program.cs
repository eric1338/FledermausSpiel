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

		// TODO: RAD zu Degree

		/*
		* Relative oder absolute Koordinaten?
		*
		* Bei Spiegelbewegung: evtl W & S + A & D 
		* MoveUp() bzw MoveLeft() dynamisch (GetLeftPoint() { p1.x < p2.x ... } (bzw. .y)
		*
		*/

		[STAThread]
		public static void Main()
		{
			MyGameWindow win = new MyGameWindow();

			win.Run(60.0);
		}

	}
}