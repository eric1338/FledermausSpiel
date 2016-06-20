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
	class LevelEndScreen : MenuScreen
	{

		private class TimeString
		{
			public string Context;
			public float Time;
			public bool IsNewRecord;

			public TimeString(string context, float time, bool isNewRecord)
			{
				Context = context;
				Time = time;
				IsNewRecord = isNewRecord;
			}
		}

		private List<TimeString> _roomTimeStrings = new List<TimeString>();
		private TimeString _totalTimeString;

		public LevelEndScreen(Level level)
		{
			// Test

			//_inputManager.AddSingleUserActionMapping(Key.Escape, UserAction.Confirm);

			if (level.Name == "Level 1") PlayerData.Instance.UnlockLevel("Level 2");
			if (level.Name == "Level 2") PlayerData.Instance.UnlockLevel("Level 3");

			LevelHighscores highscores = PlayerData.Instance.GetLevelHighscores(level.Name);

			for (int i = 0; i < level.Rooms.Count; i++)
			{
				float time = level.Times[i];

				bool isNewRecord = highscores.CheckSingleTime(i, time);

				_roomTimeStrings.Add(new TimeString("Room " + (i + 1), time, isNewRecord));
			}

			float totalTime = level.GetTotalTime();
			bool isNewTotalRecord = highscores.CheckTotalTime(totalTime);

			_totalTimeString = new TimeString("Total", totalTime, isNewTotalRecord);

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

			float y = 0.5f;

			foreach (TimeString timeString in _roomTimeStrings)
			{
				DrawTimeString(timeString, y);
				y -= 0.12f;
			}

			DrawTimeString(_totalTimeString, -0.5f);
		}

		private void DrawTimeString(TimeString timeString, float y)
		{
			string text = timeString.Context + ": " + Util.GetTimeString(timeString.Time);
			Vector3 color = timeString.IsNewRecord ? new Vector3(1f, 0.5f, 0f) : new Vector3(1f, 0.8f, 0.4f);

			BasicGraphics.DrawText(text, new Vector2(-0.5f, y), 0.08f, color);
		}

	}
}
