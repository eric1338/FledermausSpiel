using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.Screens
{
	class GameMenuScreen : MenuScreen
	{

		private GameScreen _gameScreen;

		public GameMenuScreen(GameScreen gameScreen)
		{
			_gameScreen = gameScreen;

			AddMenuButton("resume", ResumeGame, true);
			AddMenuButton("exit", ExitGame);
		}

		protected override void ProcessSingleUserActions()
		{
			base.ProcessSingleUserActions();

			foreach (UserAction userAction in _inputManager.GetSingleUserActionsAsList())
			{
				if (userAction == UserAction.OpenGameMenu) {
					ResumeGame();
				}
			}
		}

		private void ResumeGame()
		{
			MyApplication.GameWindow.CurrentScreen = _gameScreen;
			_gameScreen.ResumeGame();
		}

		private void ExitGame()
		{
			MyApplication.GameWindow.CurrentScreen = new MainMenuScreen();
		}
	}
}
