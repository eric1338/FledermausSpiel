using Fledermaus.GameObjects;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus
{
	class Levels
	{

		public static Level CreateTestLevel()
		{
			Level level = new Level();

            level.Room = new RectangularGameObject(-0.9f, 0.9f, 0.9f, -0.9f);

			level.Player = new Player(0.8f, -0.7f);

			level.SolarPanel = new SolarPanel(-0.94f, 0.2f, -0.89f, 0.0f);
			level.Exit = new Exit(-0.94f, -0.6f, -0.89f, -0.8f);
			level.LightRay = new LightRay(new Vector2(0.89f, 0.89f), new Vector2(-0.16f, -0.28f));

			Mirror m = new Mirror();
			m.RailPosition1 = new Vector2(-0.32f, -0.46f);
			m.RailPosition2 = new Vector2(-0.61f, -0.63f);
			level.AddMirror(m);

			Mirror m2 = new Mirror();
			m2.RailPosition1 = new Vector2(-0.40f, 0.32f);
			m2.RailPosition2 = new Vector2(-0.53f, 0.64f);
			level.AddMirror(m2);

			return level;
		}

	}
}
