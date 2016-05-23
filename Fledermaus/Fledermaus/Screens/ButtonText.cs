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

namespace Fledermaus.Screens
{
    class ButtonText : Button
    {

        private TextureFont font;
        protected readonly float boundingBoxHeight = .15f;
        protected readonly float boundingBoxWidth = .8f;

        // public delegate void DoAction();

        public String Text { get; set; }


        public ButtonText(String buttonText, DoAction doAction, bool selected = false) : base(doAction, selected)
        {
            this.Text = buttonText;


            Width = buttonText.Length * boundingBoxWidth / 20;
            Height = boundingBoxHeight;//10;


            //load font
            font = new TextureFont(TextureLoader.FromBitmap(Resources.Fire), 10, 32, boundingBoxWidth, 1/*boundingBoxHeightHeight*15*/, .7f);

            //background clear color
            GL.ClearColor(Color.Black);
            //for transparency in textures
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

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
            if (isSelected)
                GL.Color3(Color.LightYellow);
            else
                GL.Color3(Color.LightSteelBlue);

            GL.Enable(EnableCap.Blend); // for transparency in textures
                                        //print string

            font.Print(Position.X - (Width / 2), Position.Y - (Height / 2), 0, 0.05f, Text);
            GL.Disable(EnableCap.Blend); // for transparency in textures

        }
    }
}
