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
    public class RoomVisual : GameObjectVisual
    {
        private Color color = Color.DarkGray;
        private Color activeColor = Color.Cyan;

        private GameObjectVisual selectedGameObject;



        private PlayerVisual playerVisual;



        public PlayerVisual PlayerVisual
        {
            get
            {
                return playerVisual;
            }

            set
            {
                playerVisual = value;
            }
        }

        public GameObjectVisual SelectedGameObject
        {
            get
            {
                return selectedGameObject;
            }

            set
            {
                selectedGameObject = value;
            }
        }

        public RoomVisual()
        {
            Width = 1.9f;
            Height = 1.9f;
        }

        public override void Draw()
        {
        }

        public override void DoLogic()
        {

        }

        internal void Draw(Vector2 offset, float scale)
        {
            Scale = scale;

            if (IsSelected)
            {
                GL.Color4(activeColor);
                var tmp = new List<Vector2>();
                foreach (var bound in Data.RelativeBounds)
                    tmp.Add(bound * 1.02f);
                DrawSquare(Data.Position, tmp/*Data.RelativeBounds*1.01f*/);
                GL.Color4(color);
            }
            else
                GL.Color4(color);

            //           if ( ((Room)this.Data).Player != null)

            DrawSquare(Data.Position, Data.RelativeBounds);
            /*           if (IsSelected)
                           GL.Color4(activeColor);
                       else
                           GL.Color4(color);*/

            //           if ( ((Room)this.Data).Player != null)

            DrawSquare(offset+Data.Position, Data.RelativeBounds);

            //GL.Color3(Color.Beige);
            //DrawSquare(offset + ((Room)Data).Player.InitialPosition, ((Room)Data).Player.RelativeBounds);
            if(PlayerVisual!=null)
                PlayerVisual.Draw(offset, scale);
            
        }
    }
}
