using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus
{
	class GameScreen : Screen
	{

		private GameLogic _gameLogic = new GameLogic();
		private GameGraphics _gameGraphics = new GameGraphics();

		private Level _level;

		public GameScreen()
		{
			_level = Levels.CreateTestLevel();

			_gameLogic.Level = _level;
			_gameGraphics.Level = _level;

			_inputManager.AddProlongedUserActionMapping(Key.W, UserAction.MoveUp);
			_inputManager.AddProlongedUserActionMapping(Key.A, UserAction.MoveLeft);
			_inputManager.AddProlongedUserActionMapping(Key.S, UserAction.MoveDown);
			_inputManager.AddProlongedUserActionMapping(Key.D, UserAction.MoveRight);
			_inputManager.AddProlongedUserActionMapping(Key.E, UserAction.RotateMirrorCW);
			_inputManager.AddProlongedUserActionMapping(Key.Q, UserAction.RotateMirrorCCW);

			_inputManager.AddSingleUserActionMapping(Key.F, UserAction.ToggleMirrorLock);
			_inputManager.AddSingleUserActionMapping(Key.G, UserAction.ToggleGodMode);
			_inputManager.AddSingleUserActionMapping(Key.N, UserAction.ResetLevel);

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
	}
}
