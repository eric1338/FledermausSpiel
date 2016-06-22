﻿using System;
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
        private Fledermaus.Room _room;
        private EditMode editMode;


        private PlayerVisual playerVisual;
        private ExitVisual exitVisual;
        private List<LightRayVisual> lightRayVisuals;
        private List<ObstacleVisual> obstacleVisuals;


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

        public RoomVisual()
        {
            //           _room = new Fledermaus.Room();
            if (playerVisual != null)
                playerVisual.Data.Position = _room.Player.Position;

            lightRayVisuals = new List<LightRayVisual>();
            ObstacleVisuals = new List<ObstacleVisual>();
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
                                obstacle.IsSelected = true;
                                if (obstacle.isPointinBound(e.Position))
                                    obstacle.IsBoundSelected = true;
                                else
                                    obstacle.IsBoundSelected = false;
                            }

                            else
                            {
                                obstacle.IsSelected = false;
                            }
                        }
                    }

            }
            else if (EditMode == EditMode.Position)
            {
                Vector2 relPos = new Vector2(((e.Position.X / BasicGraphics.WindowWidth) * 2.0f - 1.0f)/BasicGraphics.GetXScale(),
                                 ((e.Position.Y / BasicGraphics.WindowHeight) * 2.0f - 1.0f) * -1);

                if (relPos.X > 1.0f)
                    relPos = new Vector2(1.0f, relPos.Y);
                else if(relPos.X<-1.0f)
                    relPos = new Vector2(-1.0f, relPos.Y);


                if (selectedGameObject.GetType().Equals(typeof(PlayerVisual)))
                    selectedGameObject.Data.Position = relPos;
                if (selectedGameObject.GetType().Equals(typeof(ExitVisual))) {
                    var absTop = (-relPos + new Vector2(.0f,1.0f)).Length;
                    var absBot = (-relPos + new Vector2(.0f, -1.0f)).Length;
                    var absRight = (-relPos + new Vector2(1.0f, .0f)).Length;
                    var absLeft = (-relPos + new Vector2(-1.0f,.0f )).Length;

                    var highest = new List<float>() { absBot, absLeft, absRight, absTop }.Min();
                    if (highest == absTop) {
                        selectedGameObject.Data.Position = new Vector2(relPos.X, 1.0f);//relPos;
                        exitVisual.IsHorizontal = true;
                    }
                    else if (highest == absBot)
                    {
                        selectedGameObject.Data.Position = new Vector2(relPos.X, -1.0f);//relPos;
                        exitVisual.IsHorizontal = true;
                    }
                    else if (highest == absLeft)
                    {
                        selectedGameObject.Data.Position = new Vector2(-1.0f,relPos.Y);//relPos;
                        exitVisual.IsHorizontal = false;
                    }
                    else if (highest == absRight)
                    {
                        selectedGameObject.Data.Position = new Vector2(1.0f, relPos.Y);//relPos;
                        exitVisual.IsHorizontal = false;
                    }
                    /*
                    if (Math.Abs(relPos.X) >= 0.9f)
                        exitVisual.IsHorizontal = false;
                    else if(Math.Abs(relPos.Y) >= 0.9f)
                        exitVisual.IsHorizontal = true;
                    */


                }
                else if (selectedGameObject.GetType().Equals(typeof(LightRayVisual)))
                {
                    if(((LightRayVisual)selectedGameObject).IsDirectionSelected)
                        ((LightRay)selectedGameObject.Data).RayDirection = relPos- ((LightRay)selectedGameObject.Data).Position;
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
                        if (obst.IsSelected)
                            SelectedGameObject = obst;
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

            Data.Position = (2 * new Vector2(_room.Column - ColumnsToZero, -1 * (_room.Row - RowsToZero)));

            var _gameGraphics = new GameGraphics();
			
            _gameGraphics.SetDrawSettings(offset * Scale + Data.Position * Scale, Scale/**0.9f*/, 1.0f);


            if (playerVisual != null)
                _room.Player.Position = playerVisual.Data.Position;
            if (exitVisual != null) {
                System.Diagnostics.Debug.WriteLine("Exit Position: "+ exitVisual.Data.Position);
                _room.Exit = new Fledermaus.GameObjects.RectangularGameObject(exitVisual.Data.Position + exitVisual.Data.RelativeBounds[0], exitVisual.Data.Position + exitVisual.Data.RelativeBounds[2]);
 
            }
            for (int i= 0;i < _room.LightRays.Count;i++)
            {
                _room.LightRays[i].Origin = lightRayVisuals[i].Data.Position;
                _room.LightRays[i].FirstDirection = ((LightRay)lightRayVisuals[i].Data).RayDirection;
            }

            for (int i = 0; i < _room.Obstacles.Count; i++)
            {
                _room.Obstacles[i] = Fledermaus.GameObjects.Obstacle.CreateRectangular(ObstacleVisuals[i].Data.Position + ObstacleVisuals[i].Data.RelativeBounds[0], ObstacleVisuals[i].Data.Position + ObstacleVisuals[i].Data.RelativeBounds[2]);
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
            foreach (var ray in lightRayVisuals)
                ray.Draw(offset + Data.Position, scale);
            foreach (var obst in ObstacleVisuals)
                obst.Draw(offset + Data.Position, scale);

        }
    }
}
