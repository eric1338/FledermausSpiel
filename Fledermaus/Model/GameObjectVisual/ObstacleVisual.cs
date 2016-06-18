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

        internal void Draw(Vector2 offset, float scale)
        {
            var tmp = new List<Vector2>();
            foreach (var bound in Data.RelativeBounds)
                tmp.Add(scale * bound * 1.1f);
            if (IsSelected)
            {
                GL.Color3(Color.Cyan);
                
                
                DrawSquare(scale*(offset+Data.Position), tmp);
            }
            else {
                GL.Color3(Color.RosyBrown);

                DrawSquare(scale*(offset+Data.Position), tmp);
            }
        }
    }
}
