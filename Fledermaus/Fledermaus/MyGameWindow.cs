using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus
{
	class MyGameWindow : GameWindow
	{

		// TODO: woanders hin

		private Level level = Levels.CreateTestLevel();
		private GameLogic gameLogic = new GameLogic();
		private GameGraphics gameGraphics = new GameGraphics();
		private Inputs inputs = new Inputs();
		
		public MyGameWindow() : base(700, 600)
		{
			RenderFrame += MyGameWindow_RenderFrame;
			UpdateFrame += MyGameWindow_UpdateFrame;
			KeyUp += MyGameWindow_KeyUp;
			KeyDown += MyGameWindow_KeyDown;
			KeyPress += MyGameWindow_KeyPress;
		}

		private void MyGameWindow_KeyPress(object sender, KeyPressEventArgs e)
		{
		}

		private void MyGameWindow_KeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
		{
			inputs.SetKeyPressing(e.Key);
			inputs.SetKeyPressed(e.Key);
		}

		private void MyGameWindow_KeyUp(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
		{
			inputs.SetKeyReleased(e.Key);
		}

		private void MyGameWindow_UpdateFrame(object sender, FrameEventArgs e)
		{
			gameLogic.MakeActions(inputs, level);
			gameLogic.DoLogic(level);
		}

		private void MyGameWindow_RenderFrame(object sender, FrameEventArgs e)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);

			gameGraphics.DrawLevel(level);

			SwapBuffers();
		}
	}
}
