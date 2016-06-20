using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.Screens
{
	class HighscoreScreen : MenuScreen
	{

		private string _currentLevel = "Level 1";

		public HighscoreScreen()
		{
			DrawTitleImage = false;

			AddMainMenuButton();
		}

		public override void DoLogic()
		{
			
		}

		public override void Draw()
		{
			base.Draw();

			BasicGraphics.SetColor(BasicGraphics.Colors.DefaultText);

			BasicGraphics.DrawText("test", new Vector2(-0.6f, 0.4f), 0.08f, new Vector3(1f, 0.8f, 0.6f));
		}

	}
}
