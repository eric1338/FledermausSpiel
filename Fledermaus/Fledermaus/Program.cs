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
		private static GameWindow gameWindow = new GameWindow();

		static AABR player = new AABR(0.0f, -0.8f, 0.05f, 0.05f);

		static List<AABR> enemies = new List<AABR>();

		[STAThread]
		public static void Main()
		{
			gameWindow.KeyDown += GameWindow_KeyDown;
			gameWindow.KeyUp += GameWindow_KeyUp;
			gameWindow.RenderFrame += GameWindow_RenderFrame;
			gameWindow.Run(60.0);
		}

		private static void GameWindow_KeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
		{

		}

		private static void GameWindow_KeyUp(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
		{

		}

		private static void GameWindow_RenderFrame(object sender, FrameEventArgs e)
		{

			GL.Clear(ClearBufferMask.ColorBufferBit);

			GL.Begin(PrimitiveType.Triangles);
			GL.Vertex2(0.8f, 0.8f);
			GL.Vertex2(0.9f, 0.8f);
			GL.Vertex2(-0.7f, -0.7f);
			GL.End();
			GL.Begin(PrimitiveType.Triangles);
			GL.Vertex2(0.8f, 0.8f);
			GL.Vertex2(-0.8f, -0.7f);
			GL.Vertex2(-0.7f, -0.7f);
			GL.End();

			GL.Color3((player.CenterX + 1) / 2, (player.CenterY + 1) / 2, 0.8);

			DrawAABR(player);

			gameWindow.SwapBuffers();
		}

		private static void DrawAABR(AABR bounds)
		{
			GL.Begin(PrimitiveType.Quads);
			GL.Vertex2(bounds.X, bounds.Y);
			GL.Vertex2(bounds.MaxX, bounds.Y);
			GL.Vertex2(bounds.MaxX, bounds.MaxY);
			GL.Vertex2(bounds.X, bounds.MaxY);
			GL.End();
		}
	}
}