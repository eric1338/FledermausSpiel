using Fledermaus.Data;
using Fledermaus.GameObjects;
using Fledermaus.Utils;
using Framework;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.Screens
{
	class LevelEndScreen : Screen
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

			_inputManager.AddSingleUserActionMapping(Key.Escape, UserAction.Confirm);

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
		}

		public override void DoLogic()
		{
			var test = _inputManager.GetSingleUserActionsAsList();

			if (test.Contains(UserAction.Confirm))
			{
				MyApplication.GameWindow.CurrentScreen = new MainMenuScreen();
			}
		}

		public override void Draw()
		{
			// TEST

			float y = 0.7f;

			foreach (TimeString timeString in _roomTimeStrings)
			{
				DrawTimeString(timeString, y);
				y -= 0.15f;
			}

			DrawTimeString(_totalTimeString, -0.3f);
		}

		private void DrawTimeString(TimeString timeString, float y)
		{
			TextureFont font = new TextureFont(TextureLoader.FromBitmap(Resources.Fire), 10, 32, 0.8f, 1, .7f);

			string text = timeString.Context + ": " + Util.GetTimeString(timeString.Time);

			if (timeString.IsNewRecord) GL.Color3(0.3f, 1f, 0f);
			else GL.Color3(1f, 1f, 1f);

			GL.Enable(EnableCap.Blend);
			font.Print(-0.5f, y, 0, 0.05f, text);
			GL.Disable(EnableCap.Blend); // for transparency in textures
		}

	}
}
