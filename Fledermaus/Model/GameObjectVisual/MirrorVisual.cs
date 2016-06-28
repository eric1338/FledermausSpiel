using System;
using System.Collections.Generic;
using System.Text;

using Model.GameObject;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using Fledermaus.Screens;
using System.Linq;

namespace Model.GameObjectVisual
{
    public class MirrorVisual : GameObjectVisual
    {
        private readonly float boundBound = .02f;

        private Color color = Color.DarkGray;
        private Color activeColor = Color.Cyan;



        private bool isMirrorPositionSelected;
        private bool isMirrorRotationSelected;
        private bool isMirrorMaxRotationSelected;
        private bool isMirrorMinRotationSelected;
        private bool isRailStartSelected;

        public bool IsMirrorPositionSelected
        {
            get
            {
                return isMirrorPositionSelected;
            }

            set
            {
                isMirrorPositionSelected = value;
            }
        }

        public bool IsMirrorRotationSelected
        {
            get
            {
                return isMirrorRotationSelected;
            }

            set
            {
                isMirrorRotationSelected = value;
            }
        }

        public bool IsMirrorMaxRotationSelected
        {
            get
            {
                return isMirrorMaxRotationSelected;
            }

            set
            {
                isMirrorMaxRotationSelected = value;
            }
        }

        public bool IsMirrorMinRotationSelected
        {
            get
            {
                return isMirrorMinRotationSelected;
            }

            set
            {
                isMirrorMinRotationSelected = value;
            }
        }

        public bool IsRailStartSelected
        {
            get
            {
                return isRailStartSelected;
            }

            set
            {
                isRailStartSelected = value;
            }
        }



        public MirrorVisual()
        {
            Width = 0.1f;
            Height = 0.1f;
        }

        public bool isPointInMirrorPosition(System.Drawing.Point point)
        {
            
            var ret = false;

            Vector2 relPos = new Vector2(((point.X / (float)Fledermaus.MyApplication.GameWindow.Width) * 2.0f - 1.0f)/** Fledermaus.BasicGraphics.GetXScale()*/,
                  ((point.Y / (float)Fledermaus.MyApplication.GameWindow.Height) * 2.0f - 1.0f) * -1);

            var mirPos = Data.Position+ ((Mirror)Data).RailStart + (-((Mirror)Data).RailStart + ((Mirror)Data).RailEnd) * ((Mirror)Data).StartingRelativePosition;

            var left = Scale * (mirPos.X  - boundBound);
            var right = Scale * (mirPos.X +  boundBound);
            var top = Scale * (mirPos.Y +  boundBound);
            var bottm = Scale * (mirPos.Y  - boundBound);

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
        public bool isPointInRailStart(System.Drawing.Point point)
        {
            var ret = false;

            Vector2 relPos = new Vector2(((point.X / (float)Fledermaus.MyApplication.GameWindow.Width) * 2.0f - 1.0f)/** Fledermaus.BasicGraphics.GetXScale()*/,
                  ((point.Y / (float)Fledermaus.MyApplication.GameWindow.Height) * 2.0f - 1.0f) * -1);

            //var mirPos =  + (-((Mirror)Data).RailStart + ((Mirror)Data).RailEnd) * ((Mirror)Data).StartingRelativePosition;

            var railstart = Data.Position + ((Mirror)Data).RailStart;

            var left = Scale * (railstart.X - boundBound);
            var right = Scale * (railstart.X + boundBound);
            var top = Scale * (railstart.Y + boundBound);
            var bottm = Scale * (railstart.Y - boundBound);

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
 

        public bool isPointInMirrorRotation(System.Drawing.Point point)
        {

            var ret = false;

            Vector2 relPos = new Vector2(((point.X / (float)Fledermaus.MyApplication.GameWindow.Width) * 2.0f - 1.0f)/** Fledermaus.BasicGraphics.GetXScale()*/,
                  ((point.Y / (float)Fledermaus.MyApplication.GameWindow.Height) * 2.0f - 1.0f) * -1);

            var dir = (-(((Mirror)Data).RailStart) + ((Mirror)Data).RailEnd);
            dir.Normalize();
            var mirRot = Fledermaus.Utils.Util.GetRotatedVector(dir, ((Mirror)Data).InitAngle) * Mirror.MirrorLength;

            var mirPos = Data.Position + ((Mirror)Data).RailStart + (-((Mirror)Data).RailStart + ((Mirror)Data).RailEnd) * ((Mirror)Data).StartingRelativePosition;



            var left = Scale * (mirPos.X - mirRot.X - boundBound);
            var right = Scale * (mirPos.X - mirRot.X + boundBound);
            var top = Scale * (mirPos.Y - mirRot.Y + boundBound);
            var bottm = Scale * (mirPos.Y - mirRot.Y - boundBound);

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
        public bool isPointInMirrorMaxRotation(System.Drawing.Point point)
        {

            var ret = false;

            Vector2 relPos = new Vector2(((point.X / (float)Fledermaus.MyApplication.GameWindow.Width) * 2.0f - 1.0f)/** Fledermaus.BasicGraphics.GetXScale()*/,
                  ((point.Y / (float)Fledermaus.MyApplication.GameWindow.Height) * 2.0f - 1.0f) * -1);

            var dir = (-(((Mirror)Data).RailStart) + ((Mirror)Data).RailEnd);
            dir.Normalize();
            

            var mirPos = Data.Position + ((Mirror)Data).RailStart + (-((Mirror)Data).RailStart + ((Mirror)Data).RailEnd) * ((Mirror)Data).StartingRelativePosition;

            var MaxPos = mirPos + Fledermaus.Utils.Util.GetRotatedVector(dir, ((Mirror)Data).MaxRotation) * Mirror.MirrorLength;

            var left = Scale * (MaxPos.X - boundBound);
            var right = Scale * (MaxPos.X + boundBound);
            var top = Scale * (MaxPos.Y + boundBound);
            var bottm = Scale * (MaxPos.Y - boundBound);

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

        public bool isPointInMirrorMinRotation(System.Drawing.Point point)
        {

            var ret = false;

            Vector2 relPos = new Vector2(((point.X / (float)Fledermaus.MyApplication.GameWindow.Width) * 2.0f - 1.0f)/** Fledermaus.BasicGraphics.GetXScale()*/,
                  ((point.Y / (float)Fledermaus.MyApplication.GameWindow.Height) * 2.0f - 1.0f) * -1);

            var dir = (-(((Mirror)Data).RailStart) + ((Mirror)Data).RailEnd);
            dir.Normalize();


            var mirPos = Data.Position + ((Mirror)Data).RailStart + (-((Mirror)Data).RailStart + ((Mirror)Data).RailEnd) * ((Mirror)Data).StartingRelativePosition;

            var MinPos = mirPos + Fledermaus.Utils.Util.GetRotatedVector(dir, ((Mirror)Data).MinRotation) * Mirror.MirrorLength;

            var left = Scale * (MinPos.X - boundBound);
            var right = Scale * (MinPos.X + boundBound);
            var top = Scale * (MinPos.Y + boundBound);
            var bottm = Scale * (MinPos.Y - boundBound);

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

        public override void Draw()
        {

        }

        public override void DoLogic()
        {

        }

        internal void Draw(Vector2 offset, float scale)
        {
            var tmp = new List<Vector2> {scale* new Vector2(Konfiguration.Round(-boundBound), Konfiguration.Round(boundBound)),
                                                           scale*  new Vector2(Konfiguration.Round(-boundBound), Konfiguration.Round(-boundBound)),
                                                           scale*  new Vector2(Konfiguration.Round(boundBound), Konfiguration.Round(-boundBound)),
                                                           scale*  new Vector2(Konfiguration.Round(boundBound), Konfiguration.Round(boundBound)) };

        var dir = (-(((Mirror)Data).RailStart) + ((Mirror)Data).RailEnd);
            dir.Normalize();

            var mirRot = Fledermaus.Utils.Util.GetRotatedVector(dir, ((Mirror)Data).InitAngle) * Mirror.MirrorLength;
            var list = new List<Vector2>()
            {
                ((Mirror)Data).RailStart,
                ((Mirror)Data).RailEnd,
                mirRot,
                -mirRot,

            };

            Data.RelativeBounds = new List<Vector2>()
            {
                new Vector2(list.Min(x=>x.X),list.Max(x=>x.Y)),
                new Vector2(list.Max(x=>x.X),list.Max(x=>x.Y)),
                new Vector2(list.Max(x=>x.X),list.Min(x=>x.Y)),
                new Vector2(list.Min(x=>x.X),list.Min(x=>x.Y)),
            };
            var scaledList = new List<Vector2>();
            foreach (var bound in Data.RelativeBounds)
                scaledList.Add(scale*bound*1.1f);

            if (IsSelected)
                GL.Color3(Color.Cyan);
            else 
                GL.Color3(Color.Blue);
            DrawSquare(scale*(offset+Data.Position), scaledList);

            if (IsRailStartSelected)
                GL.Color3(Color.LightGray);
            else
                GL.Color3(Color.Gray);
            DrawSquare(scale * (offset + Data.Position+((Mirror)Data).RailStart), tmp);

            if (isMirrorPositionSelected)
                GL.Color3(Color.LightBlue);
            else
                GL.Color3(Color.Blue);
            var mirPos = ((Mirror)Data).RailStart + (-((Mirror)Data).RailStart + ((Mirror)Data).RailEnd) * ((Mirror)Data).StartingRelativePosition;
            DrawSquare(scale * (offset + Data.Position + mirPos), tmp);


            if (isMirrorRotationSelected)
                GL.Color3(Color.LightBlue);
            else
                GL.Color3(Color.Blue);
            DrawSquare(scale * (offset + Data.Position+mirPos-mirRot), tmp);

            if (isMirrorMinRotationSelected)
                GL.Color3(Color.Lime);
            else
                GL.Color3(Color.Green);
            var MinPos = mirPos+Fledermaus.Utils.Util.GetRotatedVector(dir, ((Mirror)Data).MinRotation) * Mirror.MirrorLength;
            DrawSquare(scale * (offset + Data.Position + MinPos), tmp);

            if (isMirrorMaxRotationSelected)
                GL.Color3(Color.Lime);
            else
                GL.Color3(Color.Green);
            var MaxPos = mirPos + Fledermaus.Utils.Util.GetRotatedVector(dir, ((Mirror)Data).MaxRotation) * Mirror.MirrorLength;
            DrawSquare(scale * (offset + Data.Position + MaxPos), tmp);


        }
    }
}
