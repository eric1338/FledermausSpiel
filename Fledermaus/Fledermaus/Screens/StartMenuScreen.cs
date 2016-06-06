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
    class StartMenuScreen : MenuScreen
    {

        public StartMenuScreen() : base()
        {
            menuButtons.Add(new ButtonText( "Test Level", StartTestLevel, true));
            menuButtons.Add(new ButtonText( "Level 1", StartLevel1));
            menuButtons.Add(new ButtonText( "Level 2", StartLevel2));
            menuButtons.Add(new ButtonText( "Level 3", StartLevel3));
			menuButtons.Add(new ButtonText( "Back", delegate () { MyApplication.GameWindow.CurrentScreen = new MainMenuScreen(); }));
        }

		private void StartTestLevel()
		{
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
    }
}
