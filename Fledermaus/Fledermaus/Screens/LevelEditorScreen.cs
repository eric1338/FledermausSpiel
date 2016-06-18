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
        Edit = 2,
            Ready = 3,
    }

    class LevelEditorScreen : Screen
    {
        private const int RowsToZero = 2;
        private const int ColumnsToZero = 3;

        private GameLogic _gameLogic = new GameLogic();
        private GameGraphics _gameGraphics = new GameGraphics();
        private Fledermaus.GameObjects.Level _level = new GameObjects.Level("new");


        private Level level;
        private LevelVisual levelVisual;
        private RoomVisual selectedRoom;
        //       private GameLogic gameLogic = new GameLogic();
        //       private GameGraphics gameGraphics = new GameGraphics();
        private MenuScreen sideMenu;
        //private LevelEditorGameScreen gameScreen;

        private EditMode editMode;
        private Vector2 offset;
        private bool showSideMenu;

        //private Model.GameObject.GameObject objectToPosition;

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

        public bool ShowSideMenu
        {
            get
            {
                return showSideMenu;
            }

            set
            {
                showSideMenu = value;
            }
        }

        public LevelEditorScreen() : base()
        {

            editMode = EditMode.Selection;
            MoveTo = new Vector2(.0f, .0f);
            Offset = new Vector2(.0f, .0f);
            Scale = 0.2f;
            createSideMenu();
            //          createGameScreen();
            Level = new Level();
            LevelVisual = new LevelVisual();
            
            _level =Levels.CreateLevel1();
            _gameLogic.Level = _level;
            foreach (var _room in _level.Rooms) {
                LevelVisual.Rooms.Add(new RoomVisual() {
                    _Room = _room,
                    PlayerVisual = new PlayerVisual() {
                        Data = new Player() {
                            //InitialPosition = _room.Player.Position,
                            Position = _room.Player.Position,
                            RelativeBounds = new List<Vector2>() {  new Vector2(0.0f, 0.03f),
                                                                    new Vector2(0.025f, 0.01f),
                                                                    new Vector2(0.07f, 0.02f),

                                                                    new Vector2(0.02f, -0.045f),
                                                                    new Vector2(0.0f, -0.03f),
                                                                    new Vector2(-0.02f, -0.045f),

                                                                    new Vector2(-0.07f, 0.02f),
                                                                    new Vector2(-0.025f, 0.01f)
                        },
            }
                    },
                    Data = new Model.GameObject.Room() {
                        
                        Column = _room.Column,
                        Row = _room.Row,
                        RelativeBounds = new List<Vector2>() {  new Vector2( Konfiguration.Round(-.95f), Konfiguration.Round(.95f)),
                                                            new Vector2( Konfiguration.Round(-.95f), Konfiguration.Round(-.95f)),
                                                            new Vector2( Konfiguration.Round(.95f), Konfiguration.Round(-.95f)),
                                                            new Vector2( Konfiguration.Round(.95f), Konfiguration.Round(.95f))
                        },
                    }
                });
                foreach (var lightray in _room.LightRays) {
                    LevelVisual.Rooms[LevelVisual.Rooms.Count - 1].LightRayVisuals.Add(
                        new LightRayVisual() {
                            Data = new LightRay()
                            {
                                Position = lightray.Origin,
                                RayDirection = lightray.FirstDirection,
                                RelativeBounds = new List<Vector2>() {  new Vector2( Konfiguration.Round(-.02f), Konfiguration.Round(.02f)),
                                                            new Vector2( Konfiguration.Round(-.02f), Konfiguration.Round(-.02f)),
                                                            new Vector2( Konfiguration.Round(.02f), Konfiguration.Round(-.02f)),
                                                            new Vector2( Konfiguration.Round(.02f), Konfiguration.Round(.02f))
                                }
                            }
                
                        });
                } 
                foreach(var mirror in _room.Mirrors)
                {
                    
                    var mirV = new MirrorVisual() {
                        Data = new Mirror()
                        {

                        },
                    };
                }   
                foreach(var obstacle in _room.Obstacles)
                {
                    var obstV = new ObstacleVisual()
                    {
                        
                    };
                }
            }
            foreach (var roomV in levelVisual.Rooms)
                level.Rooms.Add(roomV.Data as Model.GameObject.Room);

            _gameGraphics.Level = _level;

 //           createStartupLevel();
//            createVisualStartupLevel();

        }

  /*      private void createGameScreen()
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
        }*/

        private void createSideMenu()
        {

            sideMenu = new MenuScreen();
            sideMenu.ShowBorder = true;
            sideMenu.Padding = .03f;
            sideMenu.BorderWidth = 0.01f;
            //sideMenu.HorizontalAlignment = HorizontalAlignment.Center;
            sideMenu.MaxHeight = 2.0f;
            sideMenu.menuButtons.Add(
                new ButtonTexture(Resources.Player,
                delegate () {
                    var lrv = new LightRayVisual()
                    {
                        Data = new Model.GameObject.LightRay()
                        {
                            RayDirection = new Vector2(.2f, .0f),
                            RelativeBounds = new List<Vector2>()
                        {
                            new Vector2( Konfiguration.Round(-.02f), Konfiguration.Round(.02f)),
                            new Vector2( Konfiguration.Round(-.02f), Konfiguration.Round(-.02f)),
                            new Vector2( Konfiguration.Round(.02f), Konfiguration.Round(-.02f)),
                            new Vector2( Konfiguration.Round(.02f), Konfiguration.Round(.02f))
                        }
                        },

                    };
                    selectedRoom.LightRayVisuals.Add(lrv);
                    _level.CurrentRoom.AddLightRay(new GameObjects.LightRay(lrv.Data.Position,((LightRay)lrv.Data).RayDirection));




                },
                true)
            );
            sideMenu.menuButtons.Add(
                new ButtonTexture(Resources.Player,
                delegate () {

                },
                false)
            );
            sideMenu.Center = new Vector2(-1.0f + sideMenu.MaxWidth/2, .0f);

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
                        //InitialPosition = new Vector2(Konfiguration.Round(0.0f), Konfiguration.Round(-0.0f)),
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
                        //InitialPosition = new Vector2(Konfiguration.Round(2.0f), Konfiguration.Round(-0.0f)),
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
                //InitialPosition = new Vector2(Konfiguration.Round(0.8f), Konfiguration.Round(-0.7f)),
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

        public override void DoLogic()
        {
            _gameLogic.DoLogic();

            if (ShowSideMenu)
                sideMenu.DoLogic();

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

            if (MoveTo.X != Offset.X/*.0f*/ ) {
                if(MoveTo.X > Offset.X)
                {
                    offset.X += Math.Abs(MoveTo.X-Offset.X)/(10);
                    if (offset.X >= MoveTo.X) {
                        offset.X = MoveTo.X;
                        MoveTo = new Vector2(.0f, MoveTo.Y);
                    }
                }
                if (MoveTo.X < Offset.X)
                {
                    offset.X -= Math.Abs(MoveTo.X - Offset.X) / (10);
                    if (offset.X <= MoveTo.X)
                    {
                        offset.X = MoveTo.X;
                        MoveTo = new Vector2(.0f, MoveTo.Y);
                    }
                }
            }

            if (MoveTo.Y !=Offset.Y/* .0f*/)
            {
                if (MoveTo.Y > Offset.Y)
                {
                    offset.Y += Math.Abs(MoveTo.Y - Offset.Y) / (10);
                    if (offset.Y >= MoveTo.Y)
                    {
                        offset.Y = MoveTo.Y;
                        MoveTo = new Vector2( MoveTo.X,.0f);
                    }
                }
                if (MoveTo.Y < Offset.Y)
                {
                    offset.Y -= Math.Abs(MoveTo.Y - Offset.Y) / (10);
                    if (offset.Y <= MoveTo.Y)
                    {
                        offset.Y = MoveTo.Y;
                        MoveTo = new Vector2( MoveTo.X,.0f);
                    }
                }
            }

            foreach (var room in levelVisual.Rooms)
                room.Draw(-Offset, Scale);


            if (ShowSideMenu)
                sideMenu.Draw();

           
        }
        public override void ProcessMouseMove(MouseMoveEventArgs e)
        {
            if (showSideMenu)
                if (sideMenu.isPointInScreen(e.Position)) {
                    sideMenu.ProcessMouseMove(e);
                    return;
                }

            if (editMode == EditMode.Selection)
            {
                foreach (var room in levelVisual.Rooms)
                {
                    if (room.isPointInScreen(e.Position))
                       SelectedRoom = room;
                }
            }
            else
            {
                selectedRoom.ProcessMouseMove(e);
            }
            
        }
        public override void ProcessMouseButtonDown(MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                if (showSideMenu)
                    if(sideMenu.isPointInScreen(e.Position))
                        sideMenu.ProcessMouseButtonDown(e);
                if (editMode == EditMode.Selection)
                {
                    ZoomTo = 1.0f;
                    foreach (var room in LevelVisual.Rooms)
                    {
                        if (room.IsSelected) {
                            MoveTo = room.Data.Position;
                        _level.CurrentRoom = room._Room;
                        }
                    }
                    editMode = EditMode.Edit;

                    if (SelectedRoom.EditMode == EditMode.Ready)
                        SelectedRoom.EditMode = editMode;
                }
                else
                {
                    SelectedRoom.ProcessMouseButtonDown(e);
                }
            }
            else if (e.Button == MouseButton.Right)
            {
               ShowSideMenu = !ShowSideMenu;
            }
        }

        public override void ProcessKeyDown(Key key) {
            if (key == Key.Escape)
            {
                if(Scale != 0.2f) { 
                    ZoomTo = 0.2f;

                    MoveTo =  new Vector2(.0f,.0f);
                    editMode = EditMode.Selection;
                }
                else 
                    MyApplication.GameWindow.CurrentScreen = new MainMenuScreen(this);
            }
        }
    }
}
