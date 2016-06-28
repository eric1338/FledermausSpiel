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
    public class ObstacleVisual : GameObjectVisual
    {
        private Color color = Color.DarkGray;
        private Color activeColor = Color.Cyan;

        private readonly float boundBound = .03f;
        private bool isBoundSelected;

        public bool IsBoundSelected
        {
            get
            {
                return isBoundSelected;
            }

            set
            {
                isBoundSelected = value;
            }
        }

        public ObstacleVisual()
        {
            Width = 0.1f;
            Height = 0.1f;
        }

        public override void Draw()
        {

        }

        public override void DoLogic()
        {

        }

        public bool isPointinBound(System.Drawing.Point point)
        {
            var ret = false;

            Vector2 relPos = new Vector2(((point.X / (float)Fledermaus.MyApplication.GameWindow.Width) * 2.0f - 1.0f)/** Fledermaus.BasicGraphics.GetXScale()*/,
                  ((point.Y / (float)Fledermaus.MyApplication.GameWindow.Height) * 2.0f - 1.0f) * -1);


            var left = Scale * (Data.Position.X + Data.RelativeBounds[0].X - boundBound);
            var right = Scale * (Data.Position.X + Data.RelativeBounds[0].X + boundBound);
            var top = Scale * (Data.Position.Y + Data.RelativeBounds[0].Y + boundBound);
            var bottm = Scale * (Data.Position.Y + Data.RelativeBounds[0].Y - boundBound);

            // Skalierung
            left *= Fledermaus.BasicGraphics.GetXScale();
            right *= Fledermaus.BasicGraphics.GetXScale();

            if (relPos.X > left && relPos.X < right)
            {

                if (relPos.Y < top && relPos.Y > bottm)
                {
                    ret = true;
                }
            }

            return ret;
        }

        internal void Draw(Vector2 offset, float scale)
        {
            var tmp = new List<Vector2>();
            foreach (var bound in Data.RelativeBounds)
                tmp.Add(scale * bound );
            if (IsSelected)
            {
                GL.Color3(Color.Cyan);
            }
            else {
                GL.Color3(Color.RosyBrown);
            }
            DrawSquare(scale*(offset+Data.Position), tmp);

            if (IsBoundSelected)
            {
                GL.Color3(Color.Cyan);
            }
            else
            {
                GL.Color3(Color.RosyBrown);
            }
            DrawSquare(scale * (offset + Data.Position+Data.RelativeBounds[0]), new List<Vector2>() {scale* new Vector2(-boundBound,boundBound),
                                                                                                     scale*  new Vector2(boundBound,boundBound),
                                                                                                     scale*  new Vector2(boundBound,-boundBound),
                                                                                                     scale*  new Vector2(-boundBound,-boundBound),
            });

        }
    }
}
