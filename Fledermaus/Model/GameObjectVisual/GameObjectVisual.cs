using Model.GameObject;
using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using Fledermaus.Screens;

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
            GL.Begin(PrimitiveType.Quads);
            foreach (var bound in bounds)
                GL.Vertex2(Scale*(position.X + bound.X), Scale*( position.Y + bound.Y));
            GL.End();
        }


    }
}
