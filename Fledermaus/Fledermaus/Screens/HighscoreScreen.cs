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

		private class LevelSelecter
		{

			private Vector2 _position { get; set; }

			private List<string> _selections = new List<string>();
			private int _selectedIndex = 0;

			private ButtonText _leftButton;
			private ButtonText _rightButton;

			public LevelSelecter(Vector2 position, List<string> selections)
			{
				_position = position;
				_selections = selections;

				_leftButton = new ButtonText("/", MoveLeft);
				_rightButton = new ButtonText("/", MoveRight);

				_leftButton.Position = _position + new Vector2(-0.2f, 0);
				_rightButton.Position = _position + new Vector2(0.2f, 0);
			}

			private void MoveLeft()
			{
				if (_selectedIndex > 0) _selectedIndex--;
			}

			private void MoveRight()
			{
				if (_selectedIndex + 1 < _selections.Count) _selectedIndex++;
			}

			public string GetCurrentSelection()
			{
				return _selections[_selectedIndex];
			}

			public void Draw()
			{
				_leftButton.Draw();
				_rightButton.Draw();

				BasicGraphics.SetColor(BasicGraphics.Colors.DefaultText);
				BasicGraphics.DrawCenteredText(GetCurrentSelection(), _position, 0.08f);

				//BasicGraphics.DrawLine(_position, new Vector2(_position.X, _position.Y + 0.1f), 0.01f);
			}

		}

		//private string _currentLevel = "Level 1";

		private LevelSelecter _levelSelecter;

		public HighscoreScreen()
		{
			DrawTitleImage = false;
			AddMainMenuButton();

			List<string> levelNames = new List<string> { "Level 1", "Level 2", "Level 3" };

			_levelSelecter = new LevelSelecter(new Vector2(0.4f, 0.0f), levelNames);

			SetTimeStrings();
		}

		private string GetCurrentLevel()
		{
			return _levelSelecter.GetCurrentSelection();
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
			TotalTimeString = new TimeString("Level", highscores.GetTotalTime(), false);
		}

		public override void DoLogic()
		{
			
		}

		public override void Draw()
		{
			base.Draw();

			DrawTitle("Highscores");

			DrawTimeStrings();

			_levelSelecter.Draw();
		}

	}
}
