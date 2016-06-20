using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using Model.GameObject;
using Fledermaus;

namespace Model.GameObjectVisual
{
    public class LightRayVisual : GameObjectVisual
    {
        private Color color = Color.Yellow;
        private Color activeColor = Color.Cyan;

        private Fledermaus.GameObjects.LightRay lightray;

        private bool isDirectionSelected = false;

        public bool IsDirectionSelected { get { return isDirectionSelected; } set { isDirectionSelected = value; } }

        public LightRayVisual()
        {
            lightray = new Fledermaus.GameObjects.LightRay(new Vector2(.0f, .0f), new Vector2(-0.3f, 0.1f));
            Width = 0.1f;
            Height = 0.1f;

           /* Data.RelativeBounds = new List<Vector2>() {  new Vector2( Konfiguration.Round(-.55f), Konfiguration.Round(.05f)),
                                                            new Vector2( Konfiguration.Round(-.90f), Konfiguration.Round(-.05f)),
                                                            new Vector2( Konfiguration.Round(.05f), Konfiguration.Round(-.05f)),
                                                            new Vector2( Konfiguration.Round(.05f), Konfiguration.Round(.05f)) };
*/
        }
        public bool isPointinDirection(System.Drawing.Point point)
        {
            var ret = false;

            Vector2 relPos = new Vector2((point.X / (float)Fledermaus.MyApplication.GameWindow.Width) * 2.0f - 1.0f,
                  ((point.Y / (float)Fledermaus.MyApplication.GameWindow.Height) * 2.0f - 1.0f) * -1);
            var left = Scale * (Data.Position.X+((LightRay)Data).RayDirection.X - Width / 2.0f);
            var right = Scale * (Data.Position.X + ((LightRay)Data).RayDirection.X + Width / 2.0f);
            var top = Scale * (Data.Position.Y + ((LightRay)Data).RayDirection.Y + Height / 2);
            var bottm = Scale * (Data.Position.Y + ((LightRay)Data).RayDirection.Y - Height / 2);

			// Skalierung
			left *= BasicGraphics.GetXScale();
			right *= BasicGraphics.GetXScale();

			if (relPos.X > left && relPos.X < right)
            {

                if (relPos.Y < top && relPos.Y > bottm)
                {
                    ret = true;
                }
            }

            return ret;
        }

        public override void Draw()
        {

        }

        public override void DoLogic()
        {

        }

        internal void Draw(Vector2 offset, float scale)
        {
            // lightray.Origin = offset + Data.Position;
            //lightray.FirstDirection = offset + ((GameObject.LightRay)Data).RayDirection;

            var tmp = new List<Vector2>();
            foreach (var bound in Data.RelativeBounds)
                tmp.Add(scale * bound * 1.1f);

            if (IsSelected)
            {
                GL.Color3(Color.Cyan);
                DrawSquare(scale * (offset + Data.Position), tmp);

            }
            else {
                GL.Color3(Color.Beige);

                DrawSquare(scale * (offset + Data.Position), tmp);


            }
            if (IsDirectionSelected)
            {
                GL.Color3(Color.Cyan);
                DrawSquare(scale * (offset + ((LightRay)Data).Position + ((LightRay)Data).RayDirection), tmp);
            }
            else
            {
                GL.Color3(Color.MediumOrchid);
                DrawSquare(scale * (offset + ((LightRay)Data).Position+((LightRay)Data).RayDirection), tmp);
            }

            //Fledermaus.GameGraphicsLevelEditor.DrawLightRay(new Fledermaus.GameObjects.LightRay(new Vector2(-0.2f, -0.8f), new Vector2(-0.3f, 0.1f)));
            
        }
    }
}