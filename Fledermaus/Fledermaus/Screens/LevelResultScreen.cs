using Fledermaus.Utils;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.Screens
{
	class LevelResultScreen : MenuScreen
	{

		protected class TimeString
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

		protected List<TimeString> RoomTimeStrings;
		protected TimeString TotalTimeString;

		protected float TimeTableY = 0.3f;

		protected void DrawTimeStrings()
		{
			if (RoomTimeStrings == null || TotalTimeString == null) return;

			float y = TimeTableY;

			foreach (TimeString timeString in RoomTimeStrings)
			{
				DrawTimeString(timeString, y);
				y -= 0.12f;
			}

			DrawTimeString(TotalTimeString, -0.45f);
		}

		private void DrawTimeString(TimeString timeString, float y)
		{
			float time = timeString.Time;

			string text = timeString.Context + ": " + (time < 0 ? "/" : Util.GetTimeString(time));

			if (timeString.IsNewRecord) BasicGraphics.SetColor(BasicGraphics.Colors.SpecialText);
			else BasicGraphics.SetColor(BasicGraphics.Colors.DefaultText);

			BasicGraphics.DrawText(text, new Vector2(-0.8f, y), 0.08f);
		}

	}
}
