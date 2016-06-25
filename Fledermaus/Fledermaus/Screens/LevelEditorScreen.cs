using System;

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
using System.Linq;

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

        private GameLogic _gameLogic = new GameLogic(true);
        private GameGraphics _gameGraphics = new GameGraphics(true);
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
            // useLevel1();
            // createVisualStartupLevel();

            createStartup();
        }

        private void createStartup()
        {
            _level = Levels.CreateLevel1();
            _gameLogic.Level = _level;

            foreach (var _room in _level.Rooms)
            {
                _room.Obstacles.Clear();
                _room.Mirrors.Clear();
                _room.LightRays.Clear();
                LevelVisual.Rooms.Add(new RoomVisual()
                {
                    _Room = _room,
                    PlayerVisual = new PlayerVisual()
                    {
                        Data = new Player()
                        {
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
                    Data = new Model.GameObject.Room()
                    {

                        Column = _room.Column,
                        Row = _room.Row,
                        RelativeBounds = new List<Vector2>() {  new Vector2( Konfiguration.Round(-1.0f), Konfiguration.Round(1.0f)),
                                                            new Vector2( Konfiguration.Round(-1.0f), Konfiguration.Round(-1.0f)),
                                                            new Vector2( Konfiguration.Round(1.0f), Konfiguration.Round(-1.0f)),
                                                            new Vector2( Konfiguration.Round(1.0f), Konfiguration.Round(1.0f))
                        },
                    },
                    ExitVisual = new ExitVisual()
                    {
                        Data = new Exit()
                        {
                            Position = _room.Exit.Point1 + 0.5f * (-_room.Exit.Point1 + _room.Exit.Point3),
                            RelativeBounds = new List<Vector2>() { -(_room.Exit.Point1+0.5f*(-_room.Exit.Point1+_room.Exit.Point3))+_room.Exit.Point1,
                                                                   -(_room.Exit.Point1+0.5f*(-_room.Exit.Point1+_room.Exit.Point3)) + _room.Exit.Point2,
                                                                   -(_room.Exit.Point1+0.5f*(-_room.Exit.Point1+_room.Exit.Point3)) + _room.Exit.Point3,
                                                                   -(_room.Exit.Point1+0.5f*(-_room.Exit.Point1+_room.Exit.Point3)) + _room.Exit.Point4,
 /*                               0.5f *(-_room.Exit.Point1+_room.Exit.Point3)-_room.Exit.Point1 ,
                                                                    0.5f*(-_room.Exit.Point1+_room.Exit.Point3)-_room.Exit.Point2,
                                                                     0.5f*(-_room.Exit.Point1+_room.Exit.Point3)-_room.Exit.Point3,
                                                                   0.5f*(-_room.Exit.Point1+_room.Exit.Point3)- _room.Exit.Point4 ,*/
                            },
                        },

                    },
                });
                if (Math.Abs(LevelVisual.Rooms[LevelVisual.Rooms.Count - 1].ExitVisual.Data.RelativeBounds[0].X) < Math.Abs(LevelVisual.Rooms[LevelVisual.Rooms.Count - 1].ExitVisual.Data.RelativeBounds[0].Y))
                {
                    levelVisual.Rooms[LevelVisual.Rooms.Count - 1].ExitVisual.IsHorizontal = false;
                }
                else
                    levelVisual.Rooms[LevelVisual.Rooms.Count - 1].ExitVisual.IsHorizontal = true;

                //        System.Diagnostics.Debug.WriteLine(0.5f * (-_room.Exit.Point1 + _room.Exit.Point3));

               /*     LevelVisual.Rooms[LevelVisual.Rooms.Count - 1].LightRayVisuals.Add(
                        new LightRayVisual()
                        {
                            Data = new LightRay()
                            {
                                Position = _room.LightRays.First().Origin,
                                RayDirection = _room.LightRays.First().FirstDirection,
                                RelativeBounds = new List<Vector2>() {  new Vector2( Konfiguration.Round(-.02f), Konfiguration.Round(.02f)),
                                                            new Vector2( Konfiguration.Round(-.02f), Konfiguration.Round(-.02f)),
                                                            new Vector2( Konfiguration.Round(.02f), Konfiguration.Round(-.02f)),
                                                            new Vector2( Konfiguration.Round(.02f), Konfiguration.Round(.02f))
                                }
                            }

                        });
                   */




                foreach (var roomV in levelVisual.Rooms)
                    level.Rooms.Add(roomV.Data as Model.GameObject.Room);

                _gameGraphics.Level = _level;

                //           createStartupLevel();
                //            createVisualStartupLevel();

            }
            /*            for (int i = 0; i < RowsToZero + (RowsToZero - 1); i++) {
                            for(int j = 0;j<ColumnsToZero + (ColumnsToZero - 1); j++)
                            {
                                levelVisual.Rooms.Add(new RoomVisual()
                                {

                                    Data = new Model.GameObject.Room()
                                    {
                                        Row = i + 1,
                                        Column = j + 1,
                                        RelativeBounds = new List<Vector2>() {  new Vector2( Konfiguration.Round(-1.0f), Konfiguration.Round(1.0f)),
                                                                    new Vector2( Konfiguration.Round(1.0f), Konfiguration.Round(1.0f)),
                                                                    new Vector2( Konfiguration.Round(1.0f), Konfiguration.Round(-1.0f)),
                                                                    new Vector2( Konfiguration.Round(-1.0f), Konfiguration.Round(-1.0f)) },

                                    },
                                    PlayerVisual = new PlayerVisual()
                                    {

                                        Data = new Player() {
                                            Position = Vector2.Zero,

                                            RelativeBounds = new List<Vector2>() {  new Vector2(0.0f, 0.03f),
                                                                                    new Vector2(0.025f, 0.01f),
                                                                                    new Vector2(0.07f, 0.02f),

                                                                                    new Vector2(0.02f, -0.045f),
                                                                                    new Vector2(0.0f, -0.03f),
                                                                                    new Vector2(-0.02f, -0.045f),

                                                                                    new Vector2(-0.07f, 0.02f),
                                                                                    new Vector2(-0.025f, 0.01f)
                                            },
                                        },
                                    },
                                    ExitVisual = new ExitVisual()
                                    {

                                        Data = new Exit()
                                        {
                                            Position = new Vector2(1.0f, .0f),
                                            RelativeBounds = new List<Vector2>() {
                                                    new Vector2(-0.105f, 0.045f),
                                                    new Vector2(0.105f, 0.045f),
                                                    new Vector2(0.105f, -0.045f),
                                                    new Vector2(-0.105f, -0.045f),
                                            },
                                        },
                                        IsHorizontal = true,
                                    }
                                });
                                var _level2 = Serializer.getLevelAs_Level(Serializer.getLevelVisualAsLevel(LevelVisual));
                                levelVisual.Rooms.Last()._Room = _level2.Rooms.Last();
                                //_level.CurrentRoom = _level.Rooms.First();

                            }
                        }
                        var _level = Serializer.getLevelAs_Level(Serializer.getLevelVisualAsLevel(LevelVisual));
                        _level.CurrentRoom = _level.Rooms.First();
                        _gameGraphics.Level = _level;
                        _gameLogic.Level = _level;
                        */
        }

        private void useLevel1()
        {
            _level = Levels.CreateLevel1();
            _gameLogic.Level = _level;

            foreach (var _room in _level.Rooms)
            {
                LevelVisual.Rooms.Add(new RoomVisual()
                {
                    _Room = _room,
                    PlayerVisual = new PlayerVisual()
                    {
                        Data = new Player()
                        {
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
                    Data = new Model.GameObject.Room()
                    {

                        Column = _room.Column,
                        Row = _room.Row,
                        RelativeBounds = new List<Vector2>() {  new Vector2( Konfiguration.Round(-1.0f), Konfiguration.Round(1.0f)),
                                                            new Vector2( Konfiguration.Round(-1.0f), Konfiguration.Round(-1.0f)),
                                                            new Vector2( Konfiguration.Round(1.0f), Konfiguration.Round(-1.0f)),
                                                            new Vector2( Konfiguration.Round(1.0f), Konfiguration.Round(1.0f))
                        },
                    },
                    ExitVisual = new ExitVisual()
                    {
                        Data = new Exit()
                        {
                            Position = _room.Exit.Point1 + 0.5f * (-_room.Exit.Point1 + _room.Exit.Point3),
                            RelativeBounds = new List<Vector2>() { -(_room.Exit.Point1+0.5f*(-_room.Exit.Point1+_room.Exit.Point3))+_room.Exit.Point1,
                                                                   -(_room.Exit.Point1+0.5f*(-_room.Exit.Point1+_room.Exit.Point3)) + _room.Exit.Point2,
                                                                   -(_room.Exit.Point1+0.5f*(-_room.Exit.Point1+_room.Exit.Point3)) + _room.Exit.Point3,
                                                                   -(_room.Exit.Point1+0.5f*(-_room.Exit.Point1+_room.Exit.Point3)) + _room.Exit.Point4,
 /*                               0.5f *(-_room.Exit.Point1+_room.Exit.Point3)-_room.Exit.Point1 ,
                                                                    0.5f*(-_room.Exit.Point1+_room.Exit.Point3)-_room.Exit.Point2,
                                                                     0.5f*(-_room.Exit.Point1+_room.Exit.Point3)-_room.Exit.Point3,
                                                                   0.5f*(-_room.Exit.Point1+_room.Exit.Point3)- _room.Exit.Point4 ,*/
                            },
                        },

                    },
                });
                if (Math.Abs(LevelVisual.Rooms[LevelVisual.Rooms.Count - 1].ExitVisual.Data.RelativeBounds[0].X) < Math.Abs(LevelVisual.Rooms[LevelVisual.Rooms.Count - 1].ExitVisual.Data.RelativeBounds[0].Y))
                {
                    levelVisual.Rooms[LevelVisual.Rooms.Count - 1].ExitVisual.IsHorizontal = false;
                }
                else
                    levelVisual.Rooms[LevelVisual.Rooms.Count - 1].ExitVisual.IsHorizontal = true;

                //        System.Diagnostics.Debug.WriteLine(0.5f * (-_room.Exit.Point1 + _room.Exit.Point3));
                foreach (var lightray in _room.LightRays)
                {
                    LevelVisual.Rooms[LevelVisual.Rooms.Count - 1].LightRayVisuals.Add(
                        new LightRayVisual()
                        {
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
                foreach (var _mirror in _room.Mirrors)
                {

                    LevelVisual.Rooms[LevelVisual.Rooms.Count - 1].MirrorVisuals.Add(new MirrorVisual()
                    {
                        Data = new Mirror()
                        {
                            Position = _mirror.RailPosition1 + 0.5f * (-_mirror.RailPosition1 + _mirror.RailPosition2),
                            RailStart = _mirror.RailPosition1 - (_mirror.RailPosition1 + 0.5f * (-_mirror.RailPosition1 + _mirror.RailPosition2)),
                            RailEnd = _mirror.RailPosition2 - (_mirror.RailPosition1 + 0.5f * (-_mirror.RailPosition1 + _mirror.RailPosition2)),
                            InitAngle = _mirror.StartingAngle,
                            MinRotation = _mirror.MinimumRotation,
                            MaxRotation = _mirror.MaximumRotation,
                            StartingRelativePosition = _mirror.StartingRelativePosition,
                            RelativeBounds = new List<Vector2>() {  new Vector2( Konfiguration.Round(-.02f), Konfiguration.Round(.02f)),
                                                            new Vector2( Konfiguration.Round(-.02f), Konfiguration.Round(-.02f)),
                                                            new Vector2( Konfiguration.Round(.02f), Konfiguration.Round(-.02f)),
                                                            new Vector2( Konfiguration.Round(.02f), Konfiguration.Round(.02f)) },
                        },
                    });
                }
                foreach (var obstacle in _room.Obstacles)
                {
                    levelVisual.Rooms[LevelVisual.Rooms.Count - 1].ObstacleVisuals.Add(new ObstacleVisual()
                    {
                        Data = new Obstacle()
                        {
                            Position = obstacle.Point1 + 0.5f * (-obstacle.Point1 + obstacle.Point3),
                            RelativeBounds = new List<Vector2>() { -(obstacle.Point1+0.5f*(-obstacle.Point1+obstacle.Point3)) + obstacle.Point1,
                                                                   -(obstacle.Point1+0.5f*(-obstacle.Point1+obstacle.Point3)) + obstacle.Point2,
                                                                   -(obstacle.Point1+0.5f*(-obstacle.Point1+obstacle.Point3)) + obstacle.Point3,
                                                                   -(obstacle.Point1+0.5f*(-obstacle.Point1+obstacle.Point3)) + obstacle.Point4,
                            }
                        }
                    });
                }

                foreach (var roomV in levelVisual.Rooms)
                    level.Rooms.Add(roomV.Data as Model.GameObject.Room);

                _gameGraphics.Level = _level;

                //           createStartupLevel();
                //            createVisualStartupLevel();

            }
        }


        private void createSideMenu()
        {

            sideMenu = new MenuScreen(false);
            sideMenu.ShowBorder = true;
            sideMenu.Padding = .03f;
            sideMenu.BorderWidth = 0.01f;
            //sideMenu.HorizontalAlignment = HorizontalAlignment.Center;
            sideMenu.MaxHeight = 2.0f;
            sideMenu.menuButtons.Add(
                new ButtonTexture(Resources.LightRay,
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
                new ButtonTexture(Resources.obstacle1,
                delegate () {
                    var obst = new ObstacleVisual()
                    {
                        Data = new Model.GameObject.Obstacle()
                        {
                            RelativeBounds = new List<Vector2>()
                        {
                            new Vector2( Konfiguration.Round(-.2f), Konfiguration.Round(.2f)),
                            new Vector2( Konfiguration.Round(-.2f), Konfiguration.Round(-.2f)),
                            new Vector2( Konfiguration.Round(.2f), Konfiguration.Round(-.2f)),
                            new Vector2( Konfiguration.Round(.2f), Konfiguration.Round(.2f))
                        }
                        },

                    };
                    selectedRoom.ObstacleVisuals.Add(obst);
                    _level.CurrentRoom.AddObstacle(new GameObjects.Obstacle(obst.Data.Position+obst.Data.RelativeBounds[0], obst.Data.Position + obst.Data.RelativeBounds[2]));
                },
                false)
            );
            sideMenu.menuButtons.Add(
    new ButtonTexture(Resources.Mirror,
    delegate () {
        var mirrorV = new MirrorVisual()
        {
            Data = new Model.GameObject.Mirror()
            {
                Position = Vector2.Zero,
                RailStart = new Vector2(-0.215f, 0.045f),
                RailEnd = new Vector2(0.215f, -0.045f),
                InitAngle = 0.4f,
                StartingRelativePosition = 0.5f,
                MaxRotation = 0.7f,
                MinRotation = -0.7f,
                RelativeBounds = new List<Vector2>(){
                            new Vector2( Konfiguration.Round(-.2f), Konfiguration.Round(.2f)),
                            new Vector2( Konfiguration.Round(-.2f), Konfiguration.Round(-.2f)),
                            new Vector2( Konfiguration.Round(.2f), Konfiguration.Round(-.2f)),
                            new Vector2( Konfiguration.Round(.2f), Konfiguration.Round(.2f))
                }
            },

        };
        selectedRoom.MirrorVisuals.Add(mirrorV);
        _level.CurrentRoom.AddMirror(new GameObjects.Mirror(mirrorV.Data.Position+((Mirror)mirrorV.Data).RailStart, mirrorV.Data.Position + ((Mirror)mirrorV.Data).RailEnd, ((Mirror)mirrorV.Data).InitAngle, ((Mirror)mirrorV.Data).StartingRelativePosition));
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
                    Column = 0,
                    Row=1,
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
                    Exit = new Exit() {
                        Position = new Vector2(1.0f,.0f),
                        RelativeBounds = new List<Vector2>()
                        {
                            new Vector2(-0.105f, 0.045f),
                            new Vector2(0.105f, 0.045f),
                            new Vector2(0.105f, -0.045f),
                            new Vector2(-0.105f, -0.045f),
                        },
                    },

                },

            };
            roomV.PlayerVisual = new PlayerVisual()
            {
                Data = ((Model.GameObject.Room)roomV.Data).Player,
            };
            roomV.ExitVisual = new ExitVisual()
            {
                Data = ((Model.GameObject.Room)roomV.Data).Exit
            };

            LevelVisual.Rooms.Add(roomV);
/*
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
            LevelVisual.Rooms.Add(roomV2);*/

            var _level = Serializer.getLevelAs_Level(Serializer.getLevelVisualAsLevel(LevelVisual));
            _level.CurrentRoom = _level.Rooms.First();
            _gameGraphics.Level = _level;
            _gameLogic.Level = _level;

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
           /* room.SolarPanel = new SolarPanel()
            {
                Position = new Vector2(Konfiguration.Round(-0.45f), Konfiguration.Round(-0.4f))
            };*/

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

      //      foreach (var room in levelVisual.Rooms)
       //         room.Draw(-Offset, Scale);
            for (int i = levelVisual.Rooms.Count - 1; i >= 0; i--)
                levelVisual.Rooms[i].Draw(-Offset, Scale);

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
                if(Scale!=0.2f)
               ShowSideMenu = !ShowSideMenu;
            }
        }

        public override void ProcessKeyDown(Key key) {
            if (key == Key.Escape)
            {
                if(Scale != 0.2f) {
                    showSideMenu = false;
                    ZoomTo = 0.2f;

                    MoveTo =  new Vector2(.0f,.0f);
                    editMode = EditMode.Selection;
                }
                else
				{
                    SwitchToScreen(new MainMenuScreen(this));
				}
            }
            else if (key == Key.Delete)
            {
                if (selectedRoom.EditMode == EditMode.Position)
                {
                    if (selectedRoom.SelectedGameObject.GetType().Equals(typeof(MirrorVisual)))
                    {
                        selectedRoom.MirrorVisuals.Remove(selectedRoom.SelectedGameObject as MirrorVisual);
                        selectedRoom._Room.Mirrors.Remove(selectedRoom._Room.Mirrors.Where(m => m.RailPosition1.Equals(selectedRoom.SelectedGameObject.Data.Position + ((Mirror)selectedRoom.SelectedGameObject.Data).RailStart)).First());
                    }
                    else if (selectedRoom.SelectedGameObject.GetType().Equals(typeof(ObstacleVisual)))
                    { 
                        selectedRoom.ObstacleVisuals.Remove(selectedRoom.SelectedGameObject as ObstacleVisual);
                        selectedRoom._Room.Obstacles.Remove(selectedRoom._Room.Obstacles.Where(o => o.Point1.Equals(selectedRoom.SelectedGameObject.Data.Position + selectedRoom.SelectedGameObject.Data.RelativeBounds[0])).First());
                    }
                    else if (selectedRoom.SelectedGameObject.GetType().Equals(typeof(LightRayVisual))) {
                        selectedRoom.LightRayVisuals.Remove(selectedRoom.SelectedGameObject as LightRayVisual);
                        selectedRoom._Room.LightRays.Remove(selectedRoom._Room.LightRays.Where(o => o.Origin.Equals(selectedRoom.SelectedGameObject.Data.Position)).First());
                     }
                    else
                    {
                        editMode = EditMode.Edit;
                        return;
                    }
                    selectedRoom.EditMode = EditMode.Edit;
                    selectedRoom.SelectedGameObject = null;
                    
                }

            }
        }
    }
}
