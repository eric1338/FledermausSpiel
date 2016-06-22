using Fledermaus.Data;
using Fledermaus.GameObjects;
using Fledermaus.Utils;
using Framework;
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
	class LevelEndScreen : LevelResultScreen
	{

		public LevelEndScreen(Level level)
		{
			// Test

			//_inputManager.AddSingleUserActionMapping(Key.Escape, UserAction.Confirm);

			if (level.Name == "Level 1") PlayerData.Instance.UnlockLevel("Level 2");
			if (level.Name == "Level 2") PlayerData.Instance.UnlockLevel("Level 3");

			LevelHighscores highscores = PlayerData.Instance.GetLevelHighscores(level.Name);

			List<TimeString> roomTimeStrings = new List<TimeString>();

			for (int i = 0; i < level.Rooms.Count; i++)
			{
				float time = level.Times[i];
				bool isNewRecord = highscores.CheckSingleTime(i, time);

				roomTimeStrings.Add(new TimeString("Room " + (i + 1), time, isNewRecord));
			}

			RoomTimeStrings = roomTimeStrings;

			float totalTime = level.GetTotalTime();
			bool isNewTotalRecord = highscores.CheckTotalTime(totalTime);

			TotalTimeString = new TimeString("Level", totalTime, isNewTotalRecord);

			// TODO: Serialisieren?

			AddMainMenuButton("continue");
		}

		/*
		public override void DoLogic()
		{
			var test = _inputManager.GetSingleUserActionsAsList();

			if (test.Contains(UserAction.Confirm))
			{
				MyApplication.GameWindow.CurrentScreen = new MainMenuScreen();
			}
		}
		*/

		public override void Draw()
		{
			base.Draw();

			DrawTitle("Level Complete");

			DrawTimeStrings();
		}

	}
}
