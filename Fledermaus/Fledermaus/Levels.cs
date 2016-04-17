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

			float lX = -0.9f;
			float rX = 0.9f;
			float tY = 0.9f;
			float bY = -0.9f;

			level.Room = new Room(lX, rX, tY, bY);

			level.Player = new Player(0.8f, -0.7f);

			level.SolarPanel = new SolarPanel(lX, 0.0f);
			level.Exit = new Exit(lX, -0.6f);
			level.LightRay = new LightRay(new Vector2(rX, tY), new Vector2(-0.1f, -0.18f));

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
