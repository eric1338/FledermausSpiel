using Fledermaus.GameObjects;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.Screens
{
	class GameScreen : Screen
	{

		private GameLogic _gameLogic = new GameLogic();
		private GameGraphics _gameGraphics = new GameGraphics();

		public GameScreen(Level level) : base()
		{
			_gameLogic.Level = level;
			_gameGraphics.Level = level;

			_gameLogic.GameScreen = this;
			
			_inputManager.AddProlongedUserActionMapping(Key.W, UserAction.MoveUp);
			_inputManager.AddProlongedUserActionMapping(Key.A, UserAction.MoveLeft);
			_inputManager.AddProlongedUserActionMapping(Key.S, UserAction.MoveDown);
			_inputManager.AddProlongedUserActionMapping(Key.D, UserAction.MoveRight);
			_inputManager.AddProlongedUserActionMapping(Key.E, UserAction.RotateMirrorCW);
			_inputManager.AddProlongedUserActionMapping(Key.Q, UserAction.RotateMirrorCCW);

			_inputManager.AddSingleUserActionMapping(Key.F, UserAction.ToggleMirrorLock);
			_inputManager.AddSingleUserActionMapping(Key.G, UserAction.ToggleGodMode);
			_inputManager.AddSingleUserActionMapping(Key.N, UserAction.ResetLevel);
			_inputManager.AddSingleUserActionMapping(Key.Escape, UserAction.OpenGameMenu);

            _gameLogic.InputManager = _inputManager;
        }

		public override void DoLogic()
		{
			_gameLogic.ProcessInput();
			_gameLogic.DoLogic();
		}

		public override void Draw()
		{
            _gameGraphics.DrawLevel();
		}

		public void OpenGameMenuScreen()
		{
			_gameLogic.PauseGame();

			// Pfui
			MyApplication.GameWindow.CurrentScreen = new GameMenuScreen(this);
		}

		public void ResumeGame()
		{
			_gameLogic.UnpauseGame();
		}

	}
}
