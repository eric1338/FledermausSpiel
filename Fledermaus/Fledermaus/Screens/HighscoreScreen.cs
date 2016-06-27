using Fledermaus.Data;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.Screens
{
	class HighscoreScreen : LevelResultScreen
	{

		private List<string> _selections = new List<string>();
		private int _selectedIndex = 0;

		public HighscoreScreen()
		{
			DrawTitleImage = false;

			List<string> levelNames = PlayerData.Instance.GetLevelNames();
			_selections = levelNames;

			Center = new Vector2(-0.1f, -0.4f);

			AddMenuButton("next level", SwitchToNextLevel, true);
			AddMenuButton("previous level", SwitchToPreviousLevel);
			AddMainMenuButton();

			SetTimeStrings();
		}

		private void SwitchToPreviousLevel()
		{
			if (_selectedIndex > 0) _selectedIndex--;
		}

		private void SwitchToNextLevel()
		{
			if (_selectedIndex + 1 < _selections.Count) _selectedIndex++;
		}

		private string GetCurrentLevel()
		{
			return _selections[_selectedIndex];
		}

		private void SetTimeStrings()
		{
			List<TimeString> newRoomTimeStrings = new List<TimeString>();
			LevelHighscores highscores = PlayerData.Instance.GetLevelHighscores(GetCurrentLevel());

			for (int i = 0; i < highscores.NumberOfRooms; i++)
			{
				newRoomTimeStrings.Add(new TimeString("Room " + (i + 1), highscores.GetTime(i), false));
			}

			RoomTimeStrings = newRoomTimeStrings;
			TotalTimeString = new TimeString("Level", highscores.TotalTime, false);
		}

		public override void DoLogic()
		{
			SetTimeStrings();
		}

		public override void Draw()
		{
			base.Draw();

			DrawTitle("Highscores");

			DrawTimeStrings();

			BasicGraphics.SetColor(BasicGraphics.Colors.DefaultText);
			BasicGraphics.DrawText(GetCurrentLevel(), new Vector2(-0.1f, 0.1f), 0.08f);
		}

	}
}
