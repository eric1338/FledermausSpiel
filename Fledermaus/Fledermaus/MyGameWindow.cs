using Fledermaus.Screens;
using OpenTK;
using OpenTK.Graphics;
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
        private Fledermaus.Screens.Screen currentScreen;
        private Vector2 windowScale;

        public Fledermaus.Screens.Screen CurrentScreen { get { return currentScreen; } set {
                
                currentScreen = value;

            } }

        public Vector2 WindowScale
        {
            get
            {
                return windowScale;
            }

            set
            {
                windowScale = value;
            }
        }

        public MyGameWindow(int width,int height) : base(width,height/*800, 700*/)
		{

            //Width
			RenderFrame += MyGameWindow_RenderFrame;
			UpdateFrame += MyGameWindow_UpdateFrame;
			KeyUp += MyGameWindow_KeyUp;
			KeyDown += MyGameWindow_KeyDown;
            MouseDown += MyGameWindow_MouseDown;
            MouseMove += MyGameWindow_MouseMove;
            MouseWheel += MyGameWindow_MouseWheel;
		}

        private void MyGameWindow_MouseWheel(object sender, OpenTK.Input.MouseWheelEventArgs e)
        {
            CurrentScreen?.ProcessMouseWheel(e);

        }

        private void MyGameWindow_MouseMove(object sender, OpenTK.Input.MouseMoveEventArgs e)
        {
            CurrentScreen?.ProcessMouseMove(e);
        }

        private void MyGameWindow_MouseDown(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            CurrentScreen?.ProcessMouseButtonDown(e);
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
			GL.Enable(EnableCap.Blend);

            if (ClientSize.Width >= ClientSize.Height)
			{
				if (ClientSize.Width != 0) windowScale = new Vector2(ClientSize.Height / ClientSize.Width, 1.0f);

				GL.Viewport(ClientSize);
			}

			CurrentScreen?.Draw();

			SwapBuffers();
		}
	}
}
