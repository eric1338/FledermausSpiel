using Model.GameObject;
using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using Fledermaus.Screens;
using System.Linq;

namespace Model.GameObjectVisual
{
    public abstract class GameObjectVisual : Screen
    {
        private GameObject.GameObject data;

        private bool isSelected;

        public GameObject.GameObject Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                isSelected = value;
            }
        }

        //public override void Draw();

        public override bool isPointInScreen(System.Drawing.Point point)
        {
            var ret = false;

            Vector2 relPos = new Vector2((point.X / (float)Fledermaus.MyApplication.GameWindow.Width) * 2.0f - 1.0f,
                  ((point.Y / (float)Fledermaus.MyApplication.GameWindow.Height) * 2.0f - 1.0f) * -1);
            var left = Scale * (Data.Position.X - Width / 2.0f);
            var right = Scale * (Data.Position.X + Width / 2.0f);
            var top = Scale * (Data.Position.Y + Height / 2);
            var bottm = Scale * (Data.Position.Y - Height / 2);
            if (relPos.X > left && relPos.X < right)
            {

                if (relPos.Y < top && relPos.Y > bottm)
                {
                    ret = true;
                }
            }

            return ret;
        }

        protected void DrawSquare(Vector2 position, List<Vector2> bounds)
        {
            /*          GL.Begin(PrimitiveType.Quads);
                      foreach (var bound in bounds)
                          GL.Vertex2(Scale*(position.X + bound.X), Scale*( position.Y + bound.Y));
                      GL.End();*/
            var x_small = 0.0f;
            var x_large = 0.0f;

            var y_small = 0.0f;
            var y_large = 0.0f;
            foreach (var bound in bounds) {
                if (bound.X < x_small)
                    x_small = bound.X;
                else if (bound.X > x_large)
                    x_large = bound.X;

                if (bound.Y < y_small)
                    y_small = bound.Y;
                else if (bound.Y > y_large)
                    y_large = bound.Y;



            }
            var modi = 1.0f;
            GL.Begin(PrimitiveType.Lines);
            for(int i = 0; i < bounds.Count - 1; i++) {
                GL.Vertex2(Scale * (position.X + modi * bounds[i].X),  Scale * (position.Y + modi *bounds[i].Y));
                GL.Vertex2( Scale * (position.X + modi *bounds[i+1].X),  Scale * (position.Y + modi *bounds[i+1].Y));
            }
            GL.Vertex2( Scale * (position.X + modi *bounds[bounds.Count-1].X),  Scale * (position.Y + modi *bounds[bounds.Count - 1].Y));
            GL.Vertex2( Scale * (position.X + modi *bounds[0].X),  Scale * (position.Y + modi *bounds[0].Y));
            /*           foreach (var bound in bounds)
                           GL.Vertex2(Scale * (position.X + bound.X), Scale * (position.Y + bound.Y));*/
            GL.End();
        }


    }
}
