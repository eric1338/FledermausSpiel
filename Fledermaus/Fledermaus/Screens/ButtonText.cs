using Framework;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fledermaus.Screens;
using System.Windows;
using OpenTK;

namespace Fledermaus.Screens
{
    class ButtonText : Button
    {
		
        protected readonly float boundingBoxHeight = .15f;
        protected readonly float boundingBoxWidth = .8f;

        // public delegate void DoAction();

        public string Text { get; set; }

		private bool isEnabled;

        public ButtonText(string buttonText, DoAction doAction, bool selected = false, bool enabled = true) : base(doAction, selected)
        {
            this.Text = buttonText;


            Width = buttonText.Length * boundingBoxWidth / 20;
            Height = boundingBoxHeight;

			isEnabled = enabled;

			if (!enabled) base.doAction = () => { };
        }

        public override void Draw()
        {
			/*        if (isSelected)
                        GL.Color3(0.0f, 1.0f, 0.0f);
                    else
                        GL.Color3(1.0f, 0.0f, 0.0f);

                    GL.Begin(PrimitiveType.Quads);
                    GL.Vertex2(-Width / 2, Position-(Height/2));
                    GL.Vertex2( Width / 2, Position - (Height / 2));
                    GL.Vertex2( Width / 2, Position + (Height / 2));
                    GL.Vertex2(-Width / 2, Position + (Height / 2));
                    GL.End();
                    */
			// GL.Clear(ClearBufferMask.ColorBufferBit);

			//color is multiplied with texture color white == no change
			// GL.Color3(Color.White);

			BasicGraphics.Colors color;

			if (!isEnabled && isSelected) color = BasicGraphics.Colors.SelectedDisabledText;
			else if (!isEnabled && !isSelected) color = BasicGraphics.Colors.DisabledText;
			else if (isEnabled && isSelected) color = BasicGraphics.Colors.SelectedText;
			else color = BasicGraphics.Colors.ButtonText;

			BasicGraphics.SetColor(color);

			//Vector2 position = new Vector2(Position.X - (Width / 2), Position.Y - (Height / 2));

			Vector2 position = new Vector2(Position.X, Position.Y);
			BasicGraphics.DrawText(Text, position, 0.08f);
        }
    }
}
