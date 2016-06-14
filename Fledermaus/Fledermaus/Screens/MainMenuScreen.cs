using Fledermaus.Data;
using Fledermaus.GameObjects;
using Fledermaus.Screens;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.Screens
{
    class MainMenuScreen : MenuScreen
    {


        public MainMenuScreen() : base()
        {
            CreateMainMenu();
        }

      
        private void CreateMainMenu()
        {
            menuButtons.Clear();
            menuButtons.Add(new ButtonText("Start Game", delegate() { CreateStartMenu(); /*MyApplication.GameWindow.CurrentScreen = new StartMenuScreen();*/ }, true));
            menuButtons.Add(new ButtonText("Load Game", delegate () { CreateLoadMenu(); }));
            menuButtons.Add(new ButtonText("Highscore", delegate () {  }));
            menuButtons.Add(new ButtonText("Level Editor", delegate () { MyApplication.GameWindow.CurrentScreen = new LevelEditorScreen(); }));
            menuButtons.Add(new ButtonText("Configuration", delegate () { }));
            menuButtons.Add(new ButtonText("Credits", delegate () { }));
            menuButtons.Add(new ButtonText("Exit", delegate () { MyApplication.Exit(); }));
        }
        private void CreateStartMenu ()
        {
            menuButtons.Clear();
 /*           menuButtons.Add(new ButtonText("Test Level", delegate () { StartTestLevel(); }, true));
            menuButtons.Add(new ButtonText("Level 2", delegate () { }));
            menuButtons.Add(new ButtonText("Level 3", delegate () { }));
            menuButtons.Add(new ButtonText("Level 4", delegate () { }));
            menuButtons.Add(new ButtonText("Level 5", delegate () { }));
            menuButtons.Add(new ButtonText("Back", delegate () { CreateMainMenu(); }));*/

            menuButtons.Add(new ButtonText("Test Level", StartTestLevel, true));
            menuButtons.Add(new ButtonText("Level 1", StartLevel1));
            menuButtons.Add(new ButtonText("Level 2", StartLevel2, false, !PlayerData.Instance.IsLevelLocked("Level 2")));
            menuButtons.Add(new ButtonText("Level 3", StartLevel3, false, !PlayerData.Instance.IsLevelLocked("Level 3")));
			menuButtons.Add(new ButtonText("Back", delegate () { MyApplication.GameWindow.CurrentScreen = new MainMenuScreen(); }));
        }

  /*      private void StartTestLevel()
        {
        }*/

        private void StartLevel1()
        {
            StartLevel(Levels.CreateLevel1());
        }

        private void StartLevel2()
        {
            StartLevel(Levels.CreateLevel2());
        }

        private void StartLevel3()
        {
            StartLevel(Levels.CreateLevel3());
        }

        private void StartLevel(Level level)
        {
            GameScreen gameScreen = new GameScreen(level);

            MyApplication.GameWindow.CurrentScreen = gameScreen;
        }
  
        private void StartTestLevel()
        {
            GameScreen gameScreen = new GameScreen(Levels.CreateTestLevel());

            MyApplication.GameWindow.CurrentScreen = gameScreen;
        }

        private void CreateLoadMenu()
        {
            menuButtons.Clear();
            menuButtons.Add(new ButtonText("Level 1", delegate () { MyApplication.GameWindow.CurrentScreen = new GameScreen(Levels.CreateTestLevel()); }, true));
            menuButtons.Add(new ButtonText("Level 2", delegate () { }));
            menuButtons.Add(new ButtonText("Back", delegate () { MyApplication.GameWindow.CurrentScreen = new MainMenuScreen(); }));
        }


    }
}
