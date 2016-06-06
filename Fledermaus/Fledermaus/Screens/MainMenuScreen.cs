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
            menuButtons.Add(new ButtonText("Test Level", delegate () { StartTestLevel(); }, true));
            menuButtons.Add(new ButtonText("Level 2", delegate () { }));
            menuButtons.Add(new ButtonText("Level 3", delegate () { }));
            menuButtons.Add(new ButtonText("Level 4", delegate () { }));
            menuButtons.Add(new ButtonText("Level 5", delegate () { }));
            menuButtons.Add(new ButtonText("Back", delegate () { CreateMainMenu(); }));
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
