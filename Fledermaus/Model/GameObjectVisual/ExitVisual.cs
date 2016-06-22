using System;
using System.Collections.Generic;
using System.Text;

using Model.GameObject;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using Fledermaus.Screens;

namespace Model.GameObjectVisual
{
    public class ExitVisual : GameObjectVisual
    {
        private Color color = Color.DarkGray;
        private Color activeColor = Color.Cyan;

        private bool? isHorizontal = null;

        public bool? IsHorizontal
        {
            get
            {
                return isHorizontal;
            }

            set
            {
                if (isHorizontal == null)
                    isHorizontal = value;
                else if (isHorizontal != value)
                    toggleHorizontal();

            }
        }

        public ExitVisual()
        {
            Width = 0.1f;
            Height = 0.1f;
        }
        public void toggleHorizontal() {
            
            //   if (isHorizontal)
            var mat3 = Matrix3.CreateRotationZ(-90);
            System.Diagnostics.Debug.WriteLine(mat3);
            var mat2 = new Matrix2(.0f,-1.0f,1.0f,.0f);
   /*         else
                mat3 = Matrix3.CreateRotationZ(-90.0f);*/

            var mat = new Matrix2(mat3.M11, mat3.M12, mat3.M21, mat3.M22);
            for (int i =0;i<Data.RelativeBounds.Count;i++) {
                var result = new Vector2();
                result.X = mat2.M11 * Data.RelativeBounds[i].X + mat2.M12 * Data.RelativeBounds[i].Y;
                result.Y = mat2.M21 * Data.RelativeBounds[i].X + mat2.M22 * Data.RelativeBounds[i].Y;
                Data.RelativeBounds[i] = result;

                
            }
            isHorizontal = !isHorizontal;
        }

        public override void Draw()
        {

        }

        public override void DoLogic()
        {

        }

        internal void Draw(Vector2 offset, float scale)
        {
            var tmp = new List<Vector2>();
            foreach (var bound in Data.RelativeBounds)
                tmp.Add(scale * bound);
            if (IsSelected)
            {
                GL.Color3(Color.Cyan);
                
                
                DrawSquare(scale*(offset+Data.Position), tmp);
            }
            else {
                GL.Color3(Color.RosyBrown);

                DrawSquare(scale *(offset+Data.Position), tmp);
            }
        }
    }
}
