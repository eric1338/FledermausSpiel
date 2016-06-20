﻿using Fledermaus.Data;
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
			// TODO: anders
			Center = new Vector2(-0.7f, -0.5f);

			menuButtons.Clear();
            AddMenuButton("new game", CreateStartMenu, true);
            AddMenuButton("highscores", delegate () { SwitchToScreen(new HighscoreScreen()); });
            AddMenuButton("level editor", delegate () { SwitchToScreen(new LevelEditorScreen()); });
            AddMenuButton("exit", MyApplication.Exit);
        }

        private void CreateStartMenu()
        {
            menuButtons.Clear();
            AddMenuButton("how to play", OpenTutorialScreen, true);
            AddMenuButton("Level 1", StartLevel1);
            AddMenuButton("Level 2", StartLevel2, false, !PlayerData.Instance.IsLevelLocked("Level 2"));
            AddMenuButton("Level 3", StartLevel3, false, !PlayerData.Instance.IsLevelLocked("Level 3"));
			AddMenuButton("back", GoToMainMenu);
        }

        private void CreateLevelEditorMenu(LevelEditorScreen les)
        {
            menuButtons.Clear();
            AddMenuButton("Resume", delegate { SwitchToScreen(les); }, true);
            AddMenuButton("Save As", delegate { openSaveAsDialog(les.Level); });
            AddMenuButton("Load", delegate { CreateLoadMenu(new MainMenuScreen(les)); } );
            AddMenuButton("exit", GoToMainMenu);
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


		private void OpenTutorialScreen()
		{
			SwitchToScreen(new TutorialScreen());
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

			SwitchToScreen(gameScreen);
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
			{
                AddMenuButton(file.Split('\\').Last().Split('.').First(), delegate {  }, true);
			}

            AddMenuButton("Back", delegate { SwitchToScreen(backScreen); }, true);
        }


    }
}
