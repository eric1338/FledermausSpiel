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

			float lX = -0.64f;
			float rX = 0.64f;
			float tY = 0.64f;
			float bY = -0.669f;

            //level.Room = new Room(lX, rX, tY, bY);

            level.Room = new Room(new Vector2(-0.9f, -0.9f), new Vector2(0.9f, -0.9f), new Vector2(0.9f, 0.9f), new Vector2(-0.9f, 0.9f));

			level.Player = new Player(0.8f, -0.7f);

			level.SolarPanel = new SolarPanel(lX, 0.0f);
			level.Exit = new Exit(lX, -0.6f);
			level.LightRay = new LightRay(new Vector2(0.49f, 0.49f), new Vector2(-0.1f, -0.28f));

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
