using Fledermaus.Screens;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fledermaus
{
	class MyGameWindow : GameWindow
	{

		public Fledermaus.Screens.Screen CurrentScreen { get; set; }
		
		public MyGameWindow(int width,int height) : base(width,height/*800, 700*/)
		{

            //Width
			RenderFrame += MyGameWindow_RenderFrame;
			UpdateFrame += MyGameWindow_UpdateFrame;
			KeyUp += MyGameWindow_KeyUp;
			KeyDown += MyGameWindow_KeyDown;
		}

		private void MyGameWindow_KeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
		{
			CurrentScreen?.ProcessKeyDown(e.Key);
		}

		private void MyGameWindow_KeyUp(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
		{
			CurrentScreen?.ProcessKeyUp(e.Key);
		}

		private void MyGameWindow_UpdateFrame(object sender, FrameEventArgs e)
		{
			CurrentScreen?.DoLogic();
		}

		private void MyGameWindow_RenderFrame(object sender, FrameEventArgs e)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);

			CurrentScreen?.Draw();

			SwapBuffers();
		}
	}
}
