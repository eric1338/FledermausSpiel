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
    public class PlayerVisual : GameObjectVisual
    {
        private Color color = Color.DarkGray;
        private Color activeColor = Color.Cyan;


        public PlayerVisual()
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
            if (IsSelected)
            {
                GL.Color3(Color.Cyan);
                var tmp = new List<Vector2>();
                foreach (var bound in Data.RelativeBounds)
                    tmp.Add(bound * 1.1f);
                DrawSquare(Data.Position, tmp/*Data.RelativeBounds*1.01f*/);
            }
            GL.Color3(Color.Beige);
            //DrawSquare(_scale * player.InitialPosition, scaleBounds(player.RelativeBounds)); if ( ((Room)this.Data).Player != null)

            DrawSquare(offset+Data.Position, Data.RelativeBounds);

        }
    }
}
