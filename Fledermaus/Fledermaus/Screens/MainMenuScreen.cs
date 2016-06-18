using Fledermaus.Data;
using Fledermaus.GameObjects;
using Fledermaus.Screens;
using Microsoft.Win32;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fledermaus.Screens
{
    class MainMenuScreen : MenuScreen
    {


        public MainMenuScreen() : base()
        {
            CreateMainMenu();
        }
        public MainMenuScreen(LevelEditorScreen les) : base()
        {
            CreateLevelEditorMenu(les);
        }


        private void CreateMainMenu()
        {
            menuButtons.Clear();
            menuButtons.Add(new ButtonText("Start Game", delegate() { CreateStartMenu(); /*MyApplication.GameWindow.CurrentScreen = new StartMenuScreen();*/ }, true));
            menuButtons.Add(new ButtonText("Load Game", delegate () { CreateLoadMenu(new MainMenuScreen()); }));
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
        private void CreateLevelEditorMenu(LevelEditorScreen les)
        {
            menuButtons.Clear();
 
            menuButtons.Add(new ButtonText("Resume", delegate { MyApplication.GameWindow.CurrentScreen = les; }, true));
            menuButtons.Add(new ButtonText("Save As",delegate { openSaveAsDialog(les.Level); }));
            menuButtons.Add(new ButtonText("Load", delegate { CreateLoadMenu(new MainMenuScreen(les)); } ));
            menuButtons.Add(new ButtonText("Main Menu",delegate { MyApplication.GameWindow.CurrentScreen = new MainMenuScreen(); }));

        }


        private void openSaveAsDialog(Model.Level level)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();
            
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory() + @"\Levels\Custom\";
            if (!Directory.Exists(saveFileDialog1.InitialDirectory))
            {
                Directory.CreateDirectory(saveFileDialog1.InitialDirectory);
            }
            saveFileDialog1.Filter = "XML Level File|*.xml";
            saveFileDialog1.Title = "Save Level";
            saveFileDialog1.ShowDialog();

            var path = Directory.GetCurrentDirectory()+@"\Levels\Custom\";

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                Model.Serializer.saveLevel(level,path,saveFileDialog1.SafeFileName);
              /*  System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();


                fs.Close();*/
            }
        }

    


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

        private void CreateLoadMenu(Screen backScreen)
        {

            menuButtons.Clear();
            indexActiveButton = 0;

            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\Levels\Custom\"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Levels\Custom\");
            }
            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Levels\Custom\"))
                menuButtons.Add(new ButtonText(file.Split('\\').Last().Split('.').First(), delegate {  }, true));
            menuButtons.Add(new ButtonText("Back", delegate { MyApplication.GameWindow.CurrentScreen = backScreen; }, true));
        }


    }
}
