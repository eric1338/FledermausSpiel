using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.Menu
{
    class MenuButton
    {
        float Width { get; set; }
        float Height { get; set; }
        public bool isSelected { get; set; }
        float Position { get; set; }


        public MenuButton(float yPosition) {
            Width = 0.2f;
            Height = 0.1f;
            isSelected = false;
            Position = yPosition;

        }

        public void draw(){
            if (isSelected)
                GL.Color3(0.0f, 1.0f, 0.0f);
            else
                GL.Color3(1.0f, 0.0f, 0.0f);

            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(-Width / 2, Position-(Height/2));
            GL.Vertex2( Width / 2, Position - (Height / 2));
            GL.Vertex2( Width / 2, Position + (Height / 2));
            GL.Vertex2(-Width / 2, Position + (Height / 2));
            GL.End();

        }
    }
}
