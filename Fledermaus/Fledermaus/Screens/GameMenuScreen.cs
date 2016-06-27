using OpenTK;
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

			Center = new Vector2(-0.7f, -0.5f);

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
			SwitchToScreen(_gameScreen);
			_gameScreen.ResumeGame();
		}

		private void ExitGame()
		{
			SwitchToScreen(new MainMenuScreen());
		}
	}
}
