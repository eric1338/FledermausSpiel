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

            level.Room = new RectangularGameObject(new Vector2(-0.9f, 0.9f), new Vector2(0.9f, -0.9f));

			level.Player = new Player(0.8f, -0.7f);

			level.SolarPanel = new SolarPanel(new Vector2(-0.94f, 0.2f), new Vector2(-0.89f, 0.0f));
			level.Exit = new Exit(new Vector2(-0.94f, -0.6f), new Vector2(-0.89f, -0.8f));
			level.LightRay = new LightRay(new Vector2(0.89f, 0.89f), new Vector2(-0.16f, -0.28f));

			Mirror m = new Mirror();
			m.RailPosition1 = new Vector2(0.12f, -0.46f);
			m.RailPosition2 = new Vector2(0.81f, -0.63f);
			level.AddMirror(m);

			Mirror m2 = new Mirror();
			m2.RailPosition1 = new Vector2(-0.40f, 0.32f);
			m2.RailPosition2 = new Vector2(-0.53f, 0.64f);
			level.AddMirror(m2);

			Obstacle o = new Obstacle();
			o.AddFirstPoint(new Vector2(-0.4f, 0.2f));
			o.AddFollowingPoint(new Vector2(-0.1f, 0.28f));
			o.AddFollowingPoint(new Vector2(0.0f, 0.16f));
			o.AddFollowingPoint(new Vector2(-0.2f, -0.08f));
			o.AddLastPoint(new Vector2(-0.28f, 0.12f));

			level.AddObstacle(o);

			Obstacle o2 = new Obstacle();
			o2.AddFirstPoint(new Vector2(0.8f, 0.8f));
			o2.AddFollowingPoint(new Vector2(0.6f, 0.8f));
			o2.AddFollowingPoint(new Vector2(0.6f, 0.6f));
			o2.AddLastPoint(new Vector2(0.8f, 0.6f));

			//level.AddObstacle(o2);

			return level;
		}

	}
}
