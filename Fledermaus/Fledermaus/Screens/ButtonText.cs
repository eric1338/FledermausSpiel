﻿using Framework;
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
            Height = boundingBoxHeight;//10;

			

            //background clear color
            GL.ClearColor(Color.Black);
            //for transparency in textures
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

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

			if (!isEnabled) GL.Color3(Color.Gray);
			else if (isSelected) GL.Color3(Color.LightYellow);
			else GL.Color3(Color.Orange);

			//Vector2 position = new Vector2(Position.X - (Width / 2), Position.Y - (Height / 2));
			Vector2 position = new Vector2(Position.X, Position.Y);
			BasicGraphics.DrawText(Text, position, 0.08f);
        }
    }
}
