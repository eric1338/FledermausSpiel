using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.Screens
{
	class TutorialScreen : MenuScreen
	{

		private List<string> lines = new List<string>();

		public TutorialScreen()
		{
			DrawTitleImage = false;

			lines.Add("your goal is to reach the exit with touching the light");
			lines.Add("");
			lines.Add("use wasd or the arrow keys to move around");
			lines.Add("");
			lines.Add("press f to focus and unfocus on a mirror");
			lines.Add("");
			lines.Add("once you are focused on a mirror, use wasd or the arrow keys");
			lines.Add("to move the mirror on the rail and q and e to rotate it");
			lines.Add("");
			lines.Add("use the mirrors to clear a path to the exit by reflecting");
			lines.Add("the light rays (right)");

			AddMainMenuButton();
		}

		public override void Draw()
		{
			base.Draw();

			DrawTitle("how to play");

			float x = -0.8f;
			float y = 0.4f;

			foreach (string line in lines)
			{
				BasicGraphics.DrawText(line, new Vector2(x, y), 0.08f);

				y -= (line.Length < 1 ? 0.05f : 0.1f);
			}
		}

	}
}
