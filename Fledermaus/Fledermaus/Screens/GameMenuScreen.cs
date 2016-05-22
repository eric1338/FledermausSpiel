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
			
			menuButtons.Add(new ButtonText(
				"Resume", 
				delegate () { ResumeGame(); }, 
				true
			));
			menuButtons.Add(new ButtonText("Save Game", delegate () { }));
			menuButtons.Add(new ButtonText("Load Game", delegate () { MyApplication.GameWindow.CurrentScreen = new LoadMenuScreen(); }));

			menuButtons.Add(new ButtonText("Main Menu", delegate () { MyApplication.GameWindow.CurrentScreen = new MainMenuScreen(); }));
			menuButtons.Add(new ButtonText("Configuration", delegate () { }));
			menuButtons.Add(new ButtonText("Credits", delegate () { }));
			menuButtons.Add(new ButtonText("Exit", delegate () { MyApplication.Exit(); }));
		}

		protected override void ProcessSingleUserActions()
		{
			foreach (UserAction userAction in _inputManager.GetSingleUserActionsAsList())
			{
				if (userAction == UserAction.MoveUp)
					ActiveButton--;
				else if (userAction == UserAction.MoveDown)
					ActiveButton++;
				else if (userAction == UserAction.Confirm)
					menuButtons[ActiveButton].doAction();
				else if (userAction == UserAction.OpenGameMenu) {
					ResumeGame();
				}

			}
		}

		private void ResumeGame()
		{
			MyApplication.GameWindow.CurrentScreen = _gameScreen;
			_gameScreen.ResumeGame();
		}
	}
}
