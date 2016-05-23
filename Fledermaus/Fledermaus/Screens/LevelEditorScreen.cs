using System;

using System.IO;

using System.Xml.Serialization;
using Model.GameObject;
using Model;
using System.Windows;
using OpenTK;
using System.Collections.Generic;

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
        //       private GameLogic gameLogic = new GameLogic();
        //       private GameGraphics gameGraphics = new GameGraphics();
        private LevelEditorSideMenu sideMenu;
        private LevelEditorGameScreen gameScreen;

        private EditMode editMode;

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
                if (value == EditMode.Selection)
                    sideMenu.IsSelected = true;
                else
                    sideMenu.IsSelected = false;
            }
        }

        public LevelEditorScreen() : base()
        {
            sideMenu = new LevelEditorSideMenu(this);

            sideMenu.ShowBorder = true;
            sideMenu.Padding = .03f;
            sideMenu.BorderWidth = 0.01f;
            sideMenu.HorizontalAlignment = HorizontalAlignment.Left;
            sideMenu.MaxHeight = 2.0f;

            float scale2 = 1.0f / 2.0f * (2.0f - sideMenu.MaxWidth);

            gameScreen = new LevelEditorGameScreen();
            gameScreen.ShowBorder = true;
            gameScreen.HorizontalAlignment = HorizontalAlignment.Center;
            gameScreen.MaxHeight = 2.0f * scale2;
            gameScreen.ContentWidth = 2.0f * scale2 - 2 * Padding - 2 * BorderWidth;
            gameScreen.Scale = scale2;// 2/100*(2.0f - sideMenu.MaxWidth-2*Padding-2*BorderWidth),
            gameScreen.Center = new Vector2((2.0f - 2.0f * scale2) / 2, (2.0f - 2.0f * scale2) / 2);

            Level = new Level();
 //           createTestLevel();
 //           var filename = "Level1.xml";
  //          saveLevel(Directory.GetCurrentDirectory() + "\\Levels\\", filename);

            EditMode = EditMode.Selection;
            //InputManager.Clear();

        }
        public void setInputManager(InputManager inputManager)
        {
            this._inputManager = inputManager;
        }
        private void createTestLevel()
        {
            Level = new Level();
            Level.Player = new Player()
            {
                InitialPosition = new Vector2(Konfiguration.Round(0.8f), Konfiguration.Round(-0.7f)),
                RelativeBounds = new List<Vector2>() { new Vector2(Konfiguration.Round(-0.05f), Konfiguration.Round(-0.05f)),
                                                       new Vector2(Konfiguration.Round(0.05f), Konfiguration.Round(-0.05f)),
                                                       new Vector2(Konfiguration.Round(0.05f), Konfiguration.Round(0.05f)),
                                                       new Vector2(Konfiguration.Round(-0.05f), Konfiguration.Round(0.05f))},
            };
            Level.LightRay = new LightRay()
            {
                Position = new Vector2(Konfiguration.Round(0.89f), Konfiguration.Round(0.89f)),
                RayDirection = new Vector2(Konfiguration.Round(-0.16f), Konfiguration.Round(-0.28f)),
            };
            Level.SolarPanel = new SolarPanel()
            {
                Position = new Vector2(Konfiguration.Round(-0.45f), Konfiguration.Round(-0.4f))
            };
            Level.Exit = new Exit()
            {
                Position = new Vector2(Konfiguration.Round(-0.9075f), Konfiguration.Round(-0.7f)),
                RelativeBounds = new List<Vector2>()
                {
                    new Vector2(Konfiguration.Round(-0.0325f),Konfiguration.Round(-0.1f)),
                    new Vector2(Konfiguration.Round(0.0325f),Konfiguration.Round(-0.1f)),
                    new Vector2(Konfiguration.Round(0.0325f),Konfiguration.Round(0.1f)),
                    new Vector2(Konfiguration.Round(-0.0325f),Konfiguration.Round(0.1f)),
                },
            };
        }

        private bool saveLevel(String filePath, String fileName)
        {

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            XmlSerializer x = new XmlSerializer(typeof(Level));
            TextWriter writer = new StreamWriter(filePath + fileName);
            x.Serialize(writer, level);

            return true;
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
            sideMenu.Draw();
            gameScreen.Draw(level);
            //           gameGraphics.DrawLevel();
        }
    }
}
