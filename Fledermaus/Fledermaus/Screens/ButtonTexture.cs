using Framework;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.Screens
{
    public class ButtonTexture : Button
    {
        private Texture texture;

        public ButtonTexture(Bitmap bitmap, DoAction doAction, bool selected = false) : base(doAction, selected)
        {

            Width = 0.3f;
            Height = 0.2f;

            texture = TextureLoader.FromBitmap(bitmap);
            //background clear color
            //GL.ClearColor(Color.Black);
            //for transparency in textures we use blending
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
        }

        public override void Draw()
        {
            //GL.Clear(ClearBufferMask.ColorBufferBit);

            if (isSelected)
                GL.Color3(Color.Yellow);
            //GL.Color3(0.0f, 1.0f, 0.0f);
            else
                GL.Color3(Color.White);



            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(Position.X - Width / 2, Position.Y - (Height / 2));
            GL.Vertex2(Position.X + Width / 2, Position.Y - (Height / 2));
            GL.Vertex2(Position.X + Width / 2, Position.Y + (Height / 2));
            GL.Vertex2(Position.X - Width / 2, Position.Y + (Height / 2));
            GL.End();

            //color is multiplied with texture color => white == no change to texture color
            GL.Color3(Color.White);

            // for transparency in textures we use blending
            GL.Enable(EnableCap.Blend);

            //the texture has to be enabled before use
            texture.BeginUse();
            GL.Begin(PrimitiveType.Quads);
            //when using textures we have to set a texture coordinate for each vertex
            //by using the TexCoord command BEFORE the Vertex command
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2((Position.X - Width / 2), (Position.Y - (Height / 2)));
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2((Position.X + Width / 2), (Position.Y - (Height / 2)));
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2((Position.X + Width / 2), (Position.Y + (Height / 2)));
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2((Position.X - Width / 2), (Position.Y + (Height / 2)));
            GL.End();
            //the texture is disabled, so no other draw calls use this texture
            texture.EndUse();

        }


    }
}
