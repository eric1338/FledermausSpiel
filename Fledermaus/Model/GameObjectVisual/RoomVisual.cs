using System;
using System.Collections.Generic;
using System.Text;

using Model.GameObject;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using Fledermaus.Screens;
using OpenTK.Input;
using Fledermaus;
using System.Linq;

namespace Model.GameObjectVisual
{

    public class RoomVisual : GameObjectVisual
    {
        private const int RowsToZero = 2;
        private const int ColumnsToZero = 3;

        private Color color = Color.Black;
        private Color activeColor = Color.Cyan;

        private GameObjectVisual selectedGameObject;
        private Fledermaus.Room _room = new Fledermaus.Room() {
            Player = new Fledermaus.GameObjects.Player(Vector2.Zero),
 
        };
        private EditMode editMode;


        private PlayerVisual playerVisual;
        private ExitVisual exitVisual;
        private List<LightRayVisual> lightRayVisuals;
        private List<ObstacleVisual> obstacleVisuals;
        private List<MirrorVisual> mirrorVisuals;


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

        public Fledermaus.Room _Room
        {
            get
            {
                return _room;
            }

            set
            {
                _room = value;
            }
        }

        public ExitVisual ExitVisual
        {
            get
            {
                return exitVisual;
            }

            set
            {
                exitVisual = value;
            }
        }

        public List<ObstacleVisual> ObstacleVisuals
        {
            get
            {
                return obstacleVisuals;
            }

            set
            {
                obstacleVisuals = value;
            }
        }

        public List<MirrorVisual> MirrorVisuals
        {
            get
            {
                return mirrorVisuals;
            }

            set
            {
                mirrorVisuals = value;
            }
        }

        public RoomVisual()
        {
            //           _room = new Fledermaus.Room();
            if (playerVisual != null)
                playerVisual.Data.Position = _room.Player.Position;

            lightRayVisuals = new List<LightRayVisual>();
            ObstacleVisuals = new List<ObstacleVisual>();
            MirrorVisuals = new List<MirrorVisual>();
            editMode = EditMode.Ready;
            Width = 2.0f;
            Height = 2.0f;
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
                if (playerVisual != null)
                    if (this.PlayerVisual.isPointInScreen(e.Position))
                    {
                        this.PlayerVisual.IsSelected = true;
                        //SelectedGameObject = PlayerVisual;
                    }
                    else
                    {
                        this.PlayerVisual.IsSelected = false;

                        if (exitVisual.isPointInScreen(e.Position))
                        {
                            exitVisual.IsSelected = true;
                            //SelectedGameObject = PlayerVisual;
                        }
                        else
                            exitVisual.IsSelected = false;

                        
                    
                        foreach (var lightRay in lightRayVisuals)
                        {
                            if (lightRay.isPointInScreen(e.Position))
                            {
                                lightRay.IsSelected = true;
                                //SelectedGameObject = lightRay;
                            }
                            else if (lightRay.isPointinDirection(e.Position)) {

                                lightRay.IsDirectionSelected = true;
                                //SelectedGameObject = lightRay;
                            }
                            else { 
                                lightRay.IsSelected = false;
                                lightRay.IsDirectionSelected = false;
                                //lightRay.IsDirectionSelected = false;
                            }
                        }
                        foreach (var obstacle in ObstacleVisuals)
                        {
                            if (obstacle.isPointInScreen(e.Position)|| obstacle.isPointinBound(e.Position))
                            {
                                //obstacle.IsSelected = true;
                                if(obstacle.isPointInScreen(e.Position))
                                    obstacle.IsSelected = true;
                                else
                                    obstacle.IsSelected = false;

                                if (obstacle.isPointinBound(e.Position))
                                    obstacle.IsBoundSelected = true;
                                else
                                    obstacle.IsBoundSelected = false;
                            }

                            else
                            {
                                obstacle.IsSelected = false;
                                obstacle.IsBoundSelected = false;
                            }
                        }
                        foreach (var mirrorV in mirrorVisuals) {
                            if (mirrorV.isPointInScreen(e.Position))
                                mirrorV.IsSelected = true;
                            else
                                mirrorV.IsSelected = false;

                            if (mirrorV.isPointInMirrorPosition(e.Position))
                                mirrorV.IsMirrorPositionSelected = true;
                            else
                                mirrorV.IsMirrorPositionSelected = false;
                            if (mirrorV.isPointInMirrorRotation(e.Position))
                                mirrorV.IsMirrorRotationSelected = true;
                            else
                                mirrorV.IsMirrorRotationSelected = false;
                            if (mirrorV.isPointInMirrorMaxRotation(e.Position))
                                mirrorV.IsMirrorMaxRotationSelected = true;
                            else
                                mirrorV.IsMirrorMaxRotationSelected = false;
                            if (mirrorV.isPointInMirrorMinRotation(e.Position))
                                mirrorV.IsMirrorMinRotationSelected = true;
                            else
                                mirrorV.IsMirrorMinRotationSelected = false;
                            if (mirrorV.isPointInRailStart(e.Position))
                                mirrorV.IsRailStartSelected = true;
                            else
                                mirrorV.IsRailStartSelected = false;

                        }
                    }

            }
            else if (EditMode == EditMode.Position)
            {
                Vector2 relPos = new Vector2(((e.Position.X / (float)Fledermaus.MyApplication.GameWindow.Width) * 2.0f - 1.0f)/BasicGraphics.GetXScale(),
                                 ((e.Position.Y / (float)Fledermaus.MyApplication.GameWindow.Height) * 2.0f - 1.0f) * -1);

                if (relPos.X > 1.0f)
                    relPos = new Vector2(1.0f, relPos.Y);
                else if(relPos.X<-1.0f)
                    relPos = new Vector2(-1.0f, relPos.Y);


                if (selectedGameObject.GetType().Equals(typeof(PlayerVisual)))
                    selectedGameObject.Data.Position = relPos;

                if (selectedGameObject.GetType().Equals(typeof(ExitVisual)))
                {
                    var absTop = (-relPos + new Vector2(.0f, 1.0f)).Length;
                    var absBot = (-relPos + new Vector2(.0f, -1.0f)).Length;
                    var absRight = (-relPos + new Vector2(1.0f, .0f)).Length;
                    var absLeft = (-relPos + new Vector2(-1.0f, .0f)).Length;

                    var highest = new List<float>() { absBot, absLeft, absRight, absTop }.Min();

                    if (highest == absTop)
                    {
                        selectedGameObject.Data.Position = new Vector2(relPos.X, 1.0f - selectedGameObject.Data.RelativeBounds[0].Y);//relPos;
                        exitVisual.IsHorizontal = true;
                    }
                    else if (highest == absBot)
                    {
                        selectedGameObject.Data.Position = new Vector2(relPos.X, -1.0f + selectedGameObject.Data.RelativeBounds[0].Y);//relPos;
                        exitVisual.IsHorizontal = true;
                    }
                    else if (highest == absLeft)
                    {
                        selectedGameObject.Data.Position = new Vector2(-1.0f + selectedGameObject.Data.RelativeBounds[0].X, relPos.Y);//relPos;
                        exitVisual.IsHorizontal = false;
                    }
                    else if (highest == absRight)
                    {
                        selectedGameObject.Data.Position = new Vector2(1.0f - selectedGameObject.Data.RelativeBounds[0].X, relPos.Y);//relPos;
                        exitVisual.IsHorizontal = false;
                    }

                }
                else if (selectedGameObject.GetType().Equals(typeof(LightRayVisual)))
                {
                    if (((LightRayVisual)selectedGameObject).IsDirectionSelected)
                        ((LightRay)selectedGameObject.Data).RayDirection = relPos - ((LightRay)selectedGameObject.Data).Position;
                    else
                        selectedGameObject.Data.Position = relPos;
                }
                else if (selectedGameObject.GetType().Equals(typeof(ObstacleVisual)))
                {
                    if (((ObstacleVisual)selectedGameObject).IsBoundSelected)
                    {
                        var scale = (relPos - selectedGameObject.Data.Position).Length / selectedGameObject.Data.RelativeBounds[0].Length;
                        for (int i = 0; i < selectedGameObject.Data.RelativeBounds.Count; i++)
                            selectedGameObject.Data.RelativeBounds[i] *= scale;
                    }
                    else
                        selectedGameObject.Data.Position = relPos;
                }
                else if (selectedGameObject.GetType().Equals(typeof(MirrorVisual))) {
                        
                    if (((MirrorVisual)selectedGameObject).IsMirrorPositionSelected) {
                        var railStart = (((Mirror)selectedGameObject.Data).Position+((Mirror)selectedGameObject.Data).RailStart);
                        if (selectedGameObject.isPointInScreen(e.Position)) {
                            var re = relPos - selectedGameObject.Data.Position;
                            var relPosMirror = .0f;
                            if (Math.Abs(selectedGameObject.Data.RelativeBounds[0].X) >= Math.Abs(selectedGameObject.Data.RelativeBounds[0].Y))
                                relPosMirror = (Math.Abs(selectedGameObject.Data.RelativeBounds[0].X) + re.X )/(-((Mirror)selectedGameObject.Data).RailStart + ((Mirror)selectedGameObject.Data).RailEnd).Length;
                            else { 
                                if(((Mirror)selectedGameObject.Data).RailStart.Y < ((Mirror)selectedGameObject.Data).RailEnd.Y)
                                    relPosMirror = (Math.Abs(selectedGameObject.Data.RelativeBounds[0].Y) + re.Y) / (-((Mirror)selectedGameObject.Data).RailStart + ((Mirror)selectedGameObject.Data).RailEnd).Length;
                                else
                                    relPosMirror = (Math.Abs(selectedGameObject.Data.RelativeBounds[0].Y) - re.Y) / (-((Mirror)selectedGameObject.Data).RailStart + ((Mirror)selectedGameObject.Data).RailEnd).Length;
                            }       

                            ((Mirror)selectedGameObject.Data).StartingRelativePosition = relPosMirror;
                        }
                    }
                    else if (((MirrorVisual)selectedGameObject).IsMirrorRotationSelected)
                    {
                        var dir = (-(((Mirror)selectedGameObject.Data).RailStart) + ((Mirror)selectedGameObject.Data).RailEnd);

                        var mirPos = ((Mirror)selectedGameObject.Data).RailStart + dir * ((Mirror)selectedGameObject.Data).StartingRelativePosition;
                        //RailStartDir
                        var u =  mirPos + ((Mirror)selectedGameObject.Data).RailStart;
                        //RelPosDir
                        var v = relPos - (selectedGameObject.Data.Position + mirPos);
                        var dot = Vector2.Dot(Vector2.Normalize(u), Vector2.Normalize(v));
                        //var len = u.Length * v.Length;
                        float angle = (float)(float)(Math.Acos(dot /*/ len*/));
                        var v3 = new Vector3(v.X, v.Y, .0f);
                        var u3 = new Vector3(u.X,u.Y,.0f);

                        //point below Raildirection
                        if(Vector3.Cross(v3, u3).Z<0)
                                angle = -1 * angle;

                           if (angle< ((Mirror)selectedGameObject.Data).MaxRotation&& angle> ((Mirror)selectedGameObject.Data).MinRotation)
                                ((Mirror)selectedGameObject.Data).InitAngle = angle;

                    }
                    else if (((MirrorVisual)selectedGameObject).IsMirrorMaxRotationSelected)
                    {
                        var dir = (-(((Mirror)selectedGameObject.Data).RailStart) + ((Mirror)selectedGameObject.Data).RailEnd);

                        var mirPos = ((Mirror)selectedGameObject.Data).RailStart + dir * ((Mirror)selectedGameObject.Data).StartingRelativePosition;
                        //RailStartDir
                        var u = -1*(mirPos + ((Mirror)selectedGameObject.Data).RailStart);
                        //RelPosDir
                        var v = relPos - (selectedGameObject.Data.Position + mirPos);
                        var dot = Vector2.Dot(Vector2.Normalize(u), Vector2.Normalize(v));
                        //var len = u.Length * v.Length;
                        float angle = (float)(float)(Math.Acos(dot /*/ len*/));
                        var v3 = new Vector3(v.X, v.Y, .0f);
                        var u3 = new Vector3(u.X, u.Y, .0f);

                        //point below Raildirection
                        if (Vector3.Cross(v3, u3).Z < 0)
                            angle = -1 * angle;

     //                  if (angle < ((Mirror)selectedGameObject.Data).MaxRotation && angle > ((Mirror)selectedGameObject.Data).MinRotation)
                         if(angle<Math.PI && /*angle>0 && */angle> ((Mirror)selectedGameObject.Data).MinRotation) { 
                            ((Mirror)selectedGameObject.Data).MaxRotation = angle;
                            if (((Mirror)selectedGameObject.Data).InitAngle > angle)
                                ((Mirror)selectedGameObject.Data).InitAngle = angle;
                        }
                    }
                    else if (((MirrorVisual)selectedGameObject).IsMirrorMinRotationSelected)
                    {
                        var dir = (-(((Mirror)selectedGameObject.Data).RailStart) + ((Mirror)selectedGameObject.Data).RailEnd);

                        var mirPos = ((Mirror)selectedGameObject.Data).RailStart + dir * ((Mirror)selectedGameObject.Data).StartingRelativePosition;
                        //RailStartDir
                        var u = -1 * (mirPos + ((Mirror)selectedGameObject.Data).RailStart);
                        //RelPosDir
                        var v = relPos - (selectedGameObject.Data.Position + mirPos);
                        var dot = Vector2.Dot(Vector2.Normalize(u), Vector2.Normalize(v));
                        //var len = u.Length * v.Length;
                        float angle = (float)(float)(Math.Acos(dot /*/ len*/));
                        var v3 = new Vector3(v.X, v.Y, .0f);
                        var u3 = new Vector3(u.X, u.Y, .0f);

                        //point below Raildirection
                        if (Vector3.Cross(v3, u3).Z < 0)
                            angle = -1 * angle;

                        //                  if (angle < ((Mirror)selectedGameObject.Data).MaxRotation && angle > ((Mirror)selectedGameObject.Data).MinRotation)
                        if (angle < Math.PI &&  angle < ((Mirror)selectedGameObject.Data).MaxRotation) {
                            ((Mirror)selectedGameObject.Data).MinRotation = angle;
                            if (((Mirror)selectedGameObject.Data).InitAngle < angle)
                                ((Mirror)selectedGameObject.Data).InitAngle = angle;
                        }
                    }
                    else if (((MirrorVisual)selectedGameObject).IsRailStartSelected)
                    {
                        var rs = relPos - selectedGameObject.Data.Position;
                        if(Math.Abs(selectedGameObject.Data.Position.X+rs.X)<1.0f && Math.Abs(selectedGameObject.Data.Position.Y + rs.X) < 1.0f
                            && Math.Abs(selectedGameObject.Data.Position.X  -rs.X) < 1.0f && Math.Abs(selectedGameObject.Data.Position.Y - rs.X) < 1.0f) {
                        ((Mirror)selectedGameObject.Data).RailStart = rs;
                        ((Mirror)selectedGameObject.Data).RailEnd = -rs;
                        }

                    }
                    else {
                        if (relPos.X+(selectedGameObject.Data.RelativeBounds.Min(b =>b.X))>-1.0f && relPos.X + (selectedGameObject.Data.RelativeBounds.Max(b => b.X)) < 1.0f &&
                            relPos.Y + (selectedGameObject.Data.RelativeBounds.Min(b => b.Y)) > -1.0f && relPos.Y + (selectedGameObject.Data.RelativeBounds.Max(b => b.Y)) < 1.0f)
                            selectedGameObject.Data.Position = relPos;
                    }
                }
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
                    if (exitVisual != null)
                        if (exitVisual.IsSelected)
                            SelectedGameObject = exitVisual;
                    foreach (var lr in lightRayVisuals) { 
                        if(lr.IsSelected)
                            SelectedGameObject = lr;
                        else if(lr.IsDirectionSelected)
                            SelectedGameObject = lr;
                    }
                    foreach (var obst in ObstacleVisuals)
                    {
                        if (obst.IsSelected|| obst.IsBoundSelected)
                            SelectedGameObject = obst;
                    }
                    foreach (var mirror in mirrorVisuals)
                    {
                        if (mirror.IsSelected || mirror.IsMirrorPositionSelected|| mirror.IsMirrorRotationSelected || mirror.IsMirrorMaxRotationSelected|| mirror.IsMirrorMinRotationSelected || mirror.IsRailStartSelected )
                            SelectedGameObject = mirror;

                    }
                    if (SelectedGameObject!=null)
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

            Data.Position = (2 * new Vector2(((GameObject.Room)Data).Column - ColumnsToZero, -1 * (((GameObject.Room)Data).Row - RowsToZero)));
            if (_room == null)
                return;
            var _gameGraphics = new GameGraphics(true);
			
            _gameGraphics.SetDrawSettings(offset * Scale + Data.Position * Scale, Scale/**0.9f*/, 1.0f);


            if (playerVisual != null)
                _room.Player.Position = playerVisual.Data.Position;
            if (exitVisual != null) {
                //System.Diagnostics.Debug.WriteLine("Exit Position: "+ exitVisual.Data.Position);
                _room.Exit = new Fledermaus.GameObjects.RectangularGameObject(exitVisual.Data.Position + exitVisual.Data.RelativeBounds[0], exitVisual.Data.Position + exitVisual.Data.RelativeBounds[2]);
 
            }
            
           // _room.LightRays.Clear();
            for (int i= 0;i <LightRayVisuals.Count;i++)
            {
                
                //_room.AddLightRay(new Fledermaus.GameObjects.LightRay(lightRayVisuals[i].Data.Position, ((LightRay)lightRayVisuals[i].Data).RayDirection) );

                     _room.LightRays[i].Origin =lightRayVisuals[i].Data.Position ;
                      _room.LightRays[i].FirstDirection =((LightRay)lightRayVisuals[i].Data).RayDirection ;
            }

            for (int i = 0; i < _room.Obstacles.Count; i++)
            {
                _room.Obstacles[i] = Fledermaus.GameObjects.Obstacle.CreateRectangular(ObstacleVisuals[i].Data.Position + ObstacleVisuals[i].Data.RelativeBounds[0], ObstacleVisuals[i].Data.Position + ObstacleVisuals[i].Data.RelativeBounds[2]);
            }
            for (int i = 0; i < _room.Mirrors.Count; i++)
            {
                _room.Mirrors[i] = new Fledermaus.GameObjects.Mirror(MirrorVisuals[i].Data.Position+ ((Mirror)MirrorVisuals[i].Data).RailStart,
                                                                     MirrorVisuals[i].Data.Position + ((Mirror)MirrorVisuals[i].Data).RailEnd,
                                                                     ((Mirror)MirrorVisuals[i].Data).InitAngle,
                                                                     ((Mirror)MirrorVisuals[i].Data).StartingRelativePosition

               );
                _room.Mirrors[i].MinimumRotation = ((Mirror)MirrorVisuals[i].Data).MinRotation;
                _room.Mirrors[i].MaximumRotation = ((Mirror)MirrorVisuals[i].Data).MaxRotation;
            }
            
            _gameGraphics.DrawRoom(_room, true);
            

            if (IsSelected)
            {
                GL.Color4(activeColor);
                var tmp = new List<Vector2>();
                foreach (var bound in Data.RelativeBounds)
                    tmp.Add(bound );
                DrawSquare(offset+Data.Position, tmp);
                GL.Color4(color);
            }
            else
                GL.Color4(color);

            //DrawSquare(Data.Position, Data.RelativeBounds);

           // DrawSquare(/*-offset+*/Data.Position, Data.RelativeBounds);

            if(PlayerVisual!=null)
                PlayerVisual.Draw(offset + Data.Position, scale);
            if (exitVisual != null)
                exitVisual.Draw(offset + Data.Position, scale);
            foreach (var rayV in lightRayVisuals)
                rayV.Draw(offset + Data.Position, scale);
            foreach (var obstV in ObstacleVisuals)
                obstV.Draw(offset + Data.Position, scale);
            foreach (var mirrorV in mirrorVisuals)
                mirrorV.Draw(offset + Data.Position, scale);

        }
    }
}
