using Framework;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fledermaus.Screens;

namespace Fledermaus.Screens
{
    class MenuButton
    {
        private readonly float boundingBoxWidth = .8f;
        private readonly float boundingBoxHeight = 1;
        private TextureFont font;
        

        // public delegate void DoAction();
        public delegate void DoAction(MyGameWindow _myGameWindow);
        public DoAction doAction;

        
        public float Width { get; set; }
        public float Height { get; set; }
 
        public float Position { get; set; }
        public float Translation { get; set; }

        public String Text { get; set; }
        public bool isSelected { get; set; }


        public MenuButton( String buttonText, DoAction doAction, bool selected=false) {
            this.Text = buttonText;
            this.doAction = doAction;
            //DoAction = callback;
            //Width = 0.2f;
            Width = buttonText.Length* boundingBoxWidth/20;
            Height = boundingBoxHeight/10;
            isSelected = selected;


            //load font
            font = new TextureFont(TextureLoader.FromBitmap(Resources.Fire), 10, 32, boundingBoxWidth, boundingBoxHeight, .7f);

            //background clear color
            GL.ClearColor(Color.Black);
            //for transparency in textures
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

        }
        public void showScreen()
        {


        }
        public void draw(){
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
            font.Print(-Width / 2, Position+Translation - (Height / 4), 0, 0.05f, Text);
            GL.Disable(EnableCap.Blend); // for transparency in textures

        }
    }
}
