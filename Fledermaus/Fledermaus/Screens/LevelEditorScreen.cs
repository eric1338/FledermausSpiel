﻿using System;

using System.IO;

using System.Xml.Serialization;
using Model.GameObject;
using Model;
using System.Windows;
using OpenTK;
using System.Collections.Generic;
using OpenTK.Input;
using Model.GameObjectVisual;
using OpenTK.Graphics.OpenGL;

namespace Fledermaus.Screens
{
    enum EditMode
    {
        Selection = 0,
        Position = 1,
        Edit = 2
    }

    class LevelEditorScreen : Screen
    {

        private Level level;
        private LevelVisual levelVisual;
        private RoomVisual selectedRoom;
        //       private GameLogic gameLogic = new GameLogic();
        //       private GameGraphics gameGraphics = new GameGraphics();
        private MenuScreen sideMenu;
        private LevelEditorGameScreen gameScreen;

        private EditMode editMode;
        private Vector2 offset;

        private Model.GameObject.GameObject objectToPosition;

        protected float ZoomTo { get; set; }


        public Level Level
        {
            set { level = value; }
            get { return level; }
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

        public LevelVisual LevelVisual
        {
            get
            {
                return levelVisual;
            }

            set
            {
                levelVisual = value;
            }
        }

        public Vector2 MoveTo { get; private set; }

        public Vector2 Offset
        {
            get
            {
                return offset;
            }

            set
            {
                offset = value;
            }
        }

        public RoomVisual SelectedRoom
        {
            get
            {
                return selectedRoom;
            }

            set
            {
                if (selectedRoom != null)
                    selectedRoom.IsSelected = false;

                selectedRoom = value;
                selectedRoom.IsSelected = true;
            }
        }

        public LevelEditorScreen() : base()
        {
            editMode = EditMode.Selection;
            MoveTo = new Vector2(.0f, .0f);
            Offset = new Vector2(.0f, .0f);
            Scale = 0.2f;
            createSideMenu();
            createGameScreen();


            Level = new Level();
            LevelVisual = new LevelVisual();
 //           createStartupLevel();
            createVisualStartupLevel();
            //           var filename = "Level1.xml";
            //          saveLevel(Directory.GetCurrentDirectory() + "\\Levels\\", filename);

           // EditMode = EditMode.Selection;
            //InputManager.Clear();

        }

        private void createGameScreen()
        {
            float scale2 = 1.0f / 2.0f * (2.0f - sideMenu.MaxWidth);

            //gameScreen = new LevelEditorGameScreen(level);
            gameScreen = new LevelEditorGameScreen(levelVisual);

            gameScreen.ShowBorder = true;
            gameScreen.HorizontalAlignment = HorizontalAlignment.Center;
            gameScreen.MaxHeight = 2.0f * scale2;
            gameScreen.ContentWidth = 2.0f * scale2 - 2 * Padding - 2 * BorderWidth;
            gameScreen.Scale = scale2;// 2/100*(2.0f - sideMenu.MaxWidth-2*Padding-2*BorderWidth),
            gameScreen.Center = new Vector2((2.0f - 2.0f * scale2) / 2, (2.0f - 2.0f * scale2) / 2);
        }

        private void createSideMenu()
        {

            sideMenu = new MenuScreen();
 /*           sideMenu.ShowBorder = true;
            sideMenu.Padding = .03f;
            sideMenu.BorderWidth = 0.01f;
            sideMenu.HorizontalAlignment = HorizontalAlignment.Left;
            sideMenu.MaxHeight = 2.0f;
            sideMenu.menuButtons.Add(
                new ButtonTexture(Resources.Player,
                delegate () {
        
                },
                true)
            );
            sideMenu.menuButtons.Add(
                new ButtonTexture(Resources.Player,
                delegate () {

                },
                false)
            );*/

            /*
            sideMenu = new LevelEditorSideMenu(this);

            sideMenu.ShowBorder = true;
            sideMenu.Padding = .03f;
            sideMenu.BorderWidth = 0.01f;
            sideMenu.HorizontalAlignment = HorizontalAlignment.Left;
            sideMenu.MaxHeight = 2.0f;*/
        }

        public void setInputManager(InputManager inputManager)
        {
            this._inputManager = inputManager;
        }
        private void createVisualStartupLevel()
        {
            LevelVisual = new LevelVisual();

            var roomV = new Model.GameObjectVisual.RoomVisual()
            {
                Data = new Model.GameObject.Room() {
                    Position = new Vector2(Konfiguration.Round(.0f), Konfiguration.Round(.0f)),
                    RelativeBounds = new List<Vector2>() {  new Vector2( Konfiguration.Round(-.95f), Konfiguration.Round(.95f)),
                                                        new Vector2( Konfiguration.Round(-.95f), Konfiguration.Round(-.95f)),
                                                        new Vector2( Konfiguration.Round(.95f), Konfiguration.Round(-.95f)),
                                                        new Vector2( Konfiguration.Round(.95f), Konfiguration.Round(.95f))
                    },
                    Player = new Model.GameObject.Player()
                    {
                        InitialPosition = new Vector2(Konfiguration.Round(0.0f), Konfiguration.Round(-0.0f)),
                        RelativeBounds = new List<Vector2>() { new Vector2(Konfiguration.Round(-0.05f), Konfiguration.Round(-0.05f)),
                                                       new Vector2(Konfiguration.Round(0.05f), Konfiguration.Round(-0.05f)),
                                                       new Vector2(Konfiguration.Round(0.05f), Konfiguration.Round(0.05f)),
                                                       new Vector2(Konfiguration.Round(-0.05f), Konfiguration.Round(0.05f))},
                    },
                    

                },

            };
            roomV.PlayerVisual = new PlayerVisual()
            {
                Data = ((Model.GameObject.Room)roomV.Data).Player,
            };
            LevelVisual.Rooms.Add(roomV);

            var roomV2 = new Model.GameObjectVisual.RoomVisual()
            {
                Data = new Model.GameObject.Room()
                {
                    Position = new Vector2(Konfiguration.Round(2.0f), Konfiguration.Round(.0f)),
                    RelativeBounds = new List<Vector2>() {  new Vector2( Konfiguration.Round(-.95f), Konfiguration.Round(.95f)),
                                                        new Vector2( Konfiguration.Round(-.95f), Konfiguration.Round(-.95f)),
                                                        new Vector2( Konfiguration.Round(.95f), Konfiguration.Round(-.95f)),
                                                        new Vector2( Konfiguration.Round(.95f), Konfiguration.Round(.95f))
                    },
                    Player = new Model.GameObject.Player()
                    {
                        InitialPosition = new Vector2(Konfiguration.Round(2.0f), Konfiguration.Round(-0.0f)),
                        RelativeBounds = new List<Vector2>() { new Vector2(Konfiguration.Round(-0.05f), Konfiguration.Round(-0.05f)),
                                                       new Vector2(Konfiguration.Round(0.05f), Konfiguration.Round(-0.05f)),
                                                       new Vector2(Konfiguration.Round(0.05f), Konfiguration.Round(0.05f)),
                                                       new Vector2(Konfiguration.Round(-0.05f), Konfiguration.Round(0.05f))},
                    },

                },
            };
            LevelVisual.Rooms.Add(roomV2);


        }
        private void createStartupLevel()
        {

            Level = new Level();


            var room = new Model.GameObject.Room() {
                Position = new Vector2(Konfiguration.Round(.0f), Konfiguration.Round(.0f)),
                RelativeBounds = new List<Vector2>() {  new Vector2( Konfiguration.Round(-.95f), Konfiguration.Round(.95f)),
                                                        new Vector2( Konfiguration.Round(-.95f), Konfiguration.Round(-.95f)),
                                                        new Vector2( Konfiguration.Round(.95f), Konfiguration.Round(-.95f)),
                                                        new Vector2( Konfiguration.Round(.95f), Konfiguration.Round(.95f))
                }
            };
           
            Level.Rooms.Add(room);
            room.Player = new Model.GameObject.Player()
            {
                InitialPosition = new Vector2(Konfiguration.Round(0.8f), Konfiguration.Round(-0.7f)),
                RelativeBounds = new List<Vector2>() { new Vector2(Konfiguration.Round(-0.05f), Konfiguration.Round(-0.05f)),
                                                       new Vector2(Konfiguration.Round(0.05f), Konfiguration.Round(-0.05f)),
                                                       new Vector2(Konfiguration.Round(0.05f), Konfiguration.Round(0.05f)),
                                                       new Vector2(Konfiguration.Round(-0.05f), Konfiguration.Round(0.05f))},
            };

            room.LightRays.Add( new LightRay()
            {
                Position = new Vector2(Konfiguration.Round(0.89f), Konfiguration.Round(0.89f)),
                RayDirection = new Vector2(Konfiguration.Round(-0.16f), Konfiguration.Round(-0.28f)),
            });
            room.SolarPanel = new SolarPanel()
            {
                Position = new Vector2(Konfiguration.Round(-0.45f), Konfiguration.Round(-0.4f))
            };

            var room2 = new Model.GameObject.Room()
            {
                Position = new Vector2(Konfiguration.Round(-2.0f), Konfiguration.Round(.0f)),
                RelativeBounds = new List<Vector2>() {  new Vector2( Konfiguration.Round(-.95f), Konfiguration.Round(.95f)),
                                                        new Vector2( Konfiguration.Round(-.95f), Konfiguration.Round(-.95f)),
                                                        new Vector2( Konfiguration.Round(.95f), Konfiguration.Round(-.95f)),
                                                        new Vector2( Konfiguration.Round(.95f), Konfiguration.Round(.95f))
                }
            };
            Level.Rooms.Add(room2);

            var room3 = new Model.GameObject.Room()
            {
                Position = new Vector2(Konfiguration.Round(0.0f), Konfiguration.Round(2.0f)),
                RelativeBounds = new List<Vector2>() {  new Vector2( Konfiguration.Round(-.95f), Konfiguration.Round(.95f)),
                                                        new Vector2( Konfiguration.Round(-.95f), Konfiguration.Round(-.95f)),
                                                        new Vector2( Konfiguration.Round(.95f), Konfiguration.Round(-.95f)),
                                                        new Vector2( Konfiguration.Round(.95f), Konfiguration.Round(.95f))
                }
            };

            Level.Rooms.Add(room3);

            var room4 = new Model.GameObject.Room()
            {
                Position = new Vector2(Konfiguration.Round(2.0f), Konfiguration.Round(.0f)),
                RelativeBounds = new List<Vector2>() {  new Vector2( Konfiguration.Round(-.95f), Konfiguration.Round(.95f)),
                                                        new Vector2( Konfiguration.Round(-.95f), Konfiguration.Round(-.95f)),
                                                        new Vector2( Konfiguration.Round(.95f), Konfiguration.Round(-.95f)),
                                                        new Vector2( Konfiguration.Round(.95f), Konfiguration.Round(.95f))
                }
            };

            Level.Rooms.Add(room4);

            var room5 = new Model.GameObject.Room()
            {
                Position = new Vector2(Konfiguration.Round(0.0f), Konfiguration.Round(-2.0f)),
                RelativeBounds = new List<Vector2>() {  new Vector2( Konfiguration.Round(-.95f), Konfiguration.Round(.95f)),
                                                        new Vector2( Konfiguration.Round(-.95f), Konfiguration.Round(-.95f)),
                                                        new Vector2( Konfiguration.Round(.95f), Konfiguration.Round(-.95f)),
                                                        new Vector2( Konfiguration.Round(.95f), Konfiguration.Round(.95f))
                }
            };

            Level.Rooms.Add(room5);

            /*            room.Exits.Add(new Exit()
                        {
                            Position = new Vector2(Konfiguration.Round(-0.9075f), Konfiguration.Round(-0.7f)),
                            RelativeBounds = new List<Vector2>()
                            {
                                new Vector2(Konfiguration.Round(-0.0325f),Konfiguration.Round(-0.1f)),
                                new Vector2(Konfiguration.Round(0.0325f),Konfiguration.Round(-0.1f)),
                                new Vector2(Konfiguration.Round(0.0325f),Konfiguration.Round(0.1f)),
                                new Vector2(Konfiguration.Round(-0.0325f),Konfiguration.Round(0.1f)),
                            },
                        });*/

        }
        private List<Screen> getContentAsList() {
            return new List<Screen>() { sideMenu, gameScreen };
        }

        public override void DoLogic()
        {

            switch (EditMode)
            {
                case EditMode.Selection: sideMenu.DoLogic(); break;
                case EditMode.Position: break;
                case EditMode.Edit: break;

            }

            //           gameLogic.ProcessInput();
            //           gameLogic.DoLogic();
        }

        public override void Draw()
        {

            if (ZoomTo != .0f) {

                if (ZoomTo > Scale) { 
                    Scale += 0.02f;
                    if (Scale  >= ZoomTo) {
                        Scale = ZoomTo;
                        ZoomTo = .0f;
                        

                    }
                }
                else { 
                    Scale -= 0.02f;
                    if (Scale  <= ZoomTo) {
                        Scale = ZoomTo;
                        ZoomTo = .0f;
                       
                    }
                }
            }

            if (MoveTo.X != .0f ) {
                if(MoveTo.X > Offset.X)
                {
                    offset.X +=(MoveTo.X-Offset.X)/(10);
                    if (offset.X >= MoveTo.X) {
                        offset.X = MoveTo.X;
                        MoveTo = new Vector2(.0f, MoveTo.Y);
                    }
                }
                if (MoveTo.X < Offset.X)
                {
                    offset.X -= (MoveTo.X - Offset.X) / (10);
                    if (offset.X <= MoveTo.X)
                    {
                        offset.X = MoveTo.X;
                        MoveTo = new Vector2(.0f, MoveTo.Y);
                    }
                }
            }

            if (MoveTo.Y != .0f)
            {
                if (MoveTo.Y > Offset.Y)
                {
                    offset.Y += (MoveTo.Y - Offset.Y) / (10);
                    if (offset.Y >= MoveTo.Y)
                    {
                        offset.Y = MoveTo.Y;
                        MoveTo = new Vector2( MoveTo.X,.0f);
                    }
                }
                if (MoveTo.Y < Offset.Y)
                {
                    offset.Y -= (MoveTo.Y - Offset.Y) / (10);
                    if (offset.Y <= MoveTo.Y)
                    {
                        offset.Y = MoveTo.Y;
                        MoveTo = new Vector2( MoveTo.X,.0f);
                    }
                }
            }


            foreach (var room in levelVisual.Rooms)
                room.Draw(-Offset, Scale);

        }
        public override void ProcessMouseMove(MouseMoveEventArgs e)
        {
            if (editMode == EditMode.Selection)
            {
                foreach (var room in levelVisual.Rooms)
                {
                    if (room.isPointInScreen(e.Position))
                        SelectedRoom = room;
                }
            }
            else if (editMode == EditMode.Edit)
            {
                
                foreach (var room in levelVisual.Rooms)
                {
                    if (room.IsSelected)
                    {
                        if (room.PlayerVisual.isPointInScreen(e.Position))
                            room.PlayerVisual.IsSelected = true;
                        else
                            room.PlayerVisual.IsSelected = false;
                    }

                }
            }
            else if (EditMode == EditMode.Position) {
                Vector2 relPos = new Vector2((e.Position.X / (float)MyApplication.GameWindow.Width) * 2.0f - 1.0f,
                ((e.Position.Y / (float)MyApplication.GameWindow.Height) * 2.0f - 1.0f) * -1);

                objectToPosition.Position = relPos;
            }
            else { 
                selectedRoom.ProcessMouseMove(e);
            }
        }
        public override void ProcessMouseButtonDown(MouseButtonEventArgs e)
        {
            if (editMode == EditMode.Selection)
            {
                ZoomTo = 1.0f;
                foreach (var room in LevelVisual.Rooms)
                {
                    if (room.IsSelected)
                        MoveTo = room.Data.Position;
                }
                editMode = EditMode.Edit;
            }
            else if (editMode == EditMode.Edit) {
                foreach (var room in LevelVisual.Rooms)
                {
                    if(room.PlayerVisual!=null)
                    if (room.PlayerVisual.IsSelected)
                        objectToPosition = room.PlayerVisual.Data;
                }
                
                EditMode = EditMode.Position;
            }
            else if (editMode == EditMode.Position)
            {
                
                EditMode = EditMode.Edit;
            }

            //Scale = 1.0f;
     //       var _timer = new System.Threading.Thread(new System.Threading.ThreadStart(delegate {
   /*             Scale = 1.0f;
                System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

                watch.Start();
                for(float i = 0; i<0.8f; i+= watch.ElapsedMilliseconds/10000)
                // Long running operation
                    Scale += watch.ElapsedMilliseconds;// new System.Threading.Timer(delegate {
    
               // _timer.Change(Math.Max(0, TIME_INTERVAL_IN_MILLISECONDS - watch.ElapsedMilliseconds), Timeout.Infinite);
            }));
      */
            Vector2 relPos = new Vector2((e.Mouse.X / (float)MyApplication.GameWindow.Width) * 2.0f - 1.0f,
                  ((e.Mouse.Y / (float)MyApplication.GameWindow.Height) * 2.0f - 1.0f) * -1); 
            if (relPos.X > Scale*Center.X - Scale * MaxWidth / 2 && relPos.X < Scale*Center.X + Scale * MaxWidth / 2)
            {
                foreach (var content in getContentAsList())
                    if (relPos.Y < Scale*content.Center.Y + Scale*content.MaxHeight / 2 && relPos.Y > Scale*content.Center.Y - Scale * content.MaxHeight / 2)
                    {
                        content.ProcessMouseButtonDown(e);
                    }
            }
        }
    }
}
