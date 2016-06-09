using System;
using System.Collections.Generic;
using System.Text;

using Model.GameObject;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using Fledermaus.Screens;
using OpenTK.Input;

namespace Model.GameObjectVisual
{

    public class RoomVisual : GameObjectVisual
    {
        private Color color = Color.Black;
        private Color activeColor = Color.Cyan;

        private GameObjectVisual selectedGameObject;

        private EditMode editMode;


        private PlayerVisual playerVisual;
        private List<LightRayVisual> lightRayVisuals;


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
                if (selectedGameObject != null)
                    selectedGameObject.IsSelected = false;
                if (value != null)
                    value.IsSelected = true;
                selectedGameObject = value;
            }
        }

        internal EditMode EditMode
        {
            get
            {
                return editMode;
            }

            set
            {
                editMode = value;
            }
        }

        public List<LightRayVisual> LightRayVisuals
        {
            get
            {
                return lightRayVisuals;
            }

            set
            {
                lightRayVisuals = value;
            }
        }

        public RoomVisual()
        {
            lightRayVisuals = new List<LightRayVisual>();
            editMode = EditMode.Ready;
            Width = 1.9f;
            Height = 1.9f;
        }

        public override void Draw()
        {
        }

        public override void DoLogic()
        {

        }
        public override void ProcessMouseMove(MouseMoveEventArgs e)
        {

            if (EditMode == EditMode.Edit)
            {

                if (this.PlayerVisual.isPointInScreen(e.Position)) { 
                    this.PlayerVisual.IsSelected = true;
                }
                else
                    this.PlayerVisual.IsSelected = false;
           

            }
            else if (EditMode == EditMode.Position)
            {
                Vector2 relPos = new Vector2((e.Position.X / (float)Fledermaus.MyApplication.GameWindow.Width) * 2.0f - 1.0f,
                ((e.Position.Y / (float)Fledermaus.MyApplication.GameWindow.Height) * 2.0f - 1.0f) * -1);

                playerVisual.Data.Position = relPos;
            }
        }
        public override void ProcessMouseButtonDown(MouseButtonEventArgs e)
        {
            if(e.Button== MouseButton.Left) { 
                if (editMode == EditMode.Edit)
                {
                    if (PlayerVisual != null)
                        if (PlayerVisual.IsSelected)
                            SelectedGameObject = PlayerVisual;

                    if(SelectedGameObject!=null)
                        EditMode = EditMode.Position;
                }
                else if (editMode == EditMode.Position)
                {
                
                    SelectedGameObject = null;
                    EditMode = EditMode.Edit;
                }
            }
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
                DrawSquare(Data.Position, tmp);
                GL.Color4(color);
            }
            else
                GL.Color4(color);

            DrawSquare(Data.Position, Data.RelativeBounds);

            DrawSquare(offset+Data.Position, Data.RelativeBounds);

            if(PlayerVisual!=null)
                PlayerVisual.Draw(offset, scale);
            foreach (var ray in lightRayVisuals)
                ray.Draw(offset, scale);
            
        }
    }
}
