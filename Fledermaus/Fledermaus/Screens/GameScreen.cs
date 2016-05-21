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

		//private GameLogic _gameLogic = new GameLogic();
		//private GameGraphics _gameGraphics = new GameGraphics();

		//private Level _level;

		public GameScreen() :base()
		{
			//_level = Levels.CreateTestLevel();

			//GameLogic.Level = _level;
			//GameGraphics.Level = _level;

            InputManager.Clear();
			InputManager.AddProlongedUserActionMapping(Key.W, UserAction.MoveUp);
            InputManager.AddProlongedUserActionMapping(Key.A, UserAction.MoveLeft);
            InputManager.AddProlongedUserActionMapping(Key.S, UserAction.MoveDown);
            InputManager.AddProlongedUserActionMapping(Key.D, UserAction.MoveRight);
            InputManager.AddProlongedUserActionMapping(Key.E, UserAction.RotateMirrorCW);
            InputManager.AddProlongedUserActionMapping(Key.Q, UserAction.RotateMirrorCCW);

            InputManager.AddSingleUserActionMapping(Key.F, UserAction.ToggleMirrorLock);
            InputManager.AddSingleUserActionMapping(Key.G, UserAction.ToggleGodMode);
            InputManager.AddSingleUserActionMapping(Key.N, UserAction.ResetLevel);
            InputManager.AddSingleUserActionMapping(Key.Escape, UserAction.Cancel);
            //_gameLogic.InputManager = _inputManager;
        }

		public override void DoLogic()
		{
			GameLogic.ProcessInput();
			GameLogic.DoLogic();
		}

		public override void Draw()
		{
            GameGraphics.DrawLevel();
		}
	}
}
