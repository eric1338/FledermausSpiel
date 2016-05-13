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
			Room testRoom = new Room();

            testRoom.RoomBounds = new RectangularGameObject(new Vector2(-0.9f, 0.9f), new Vector2(0.9f, -0.9f));

			testRoom.Player = new Player(new Vector2(0.8f, -0.7f));

			testRoom.SolarPanel = new RectangularGameObject(new Vector2(-0.94f, 0.2f), new Vector2(-0.875f, 0.0f));
			testRoom.Exit = new RectangularGameObject(new Vector2(-0.94f, -0.6f), new Vector2(-0.875f, -0.8f));
			testRoom.LightRay = new LightRay(new Vector2(0.89f, 0.89f), new Vector2(-0.16f, -0.28f));

			Mirror m = new Mirror(new Vector2(0.12f, -0.46f), new Vector2(0.81f, -0.63f));
			testRoom.AddMirror(m);

			Mirror m2 = new Mirror(new Vector2(-0.40f, 0.32f), new Vector2(-0.53f, 0.64f));
			testRoom.AddMirror(m2);

			Obstacle o = new Obstacle();
			o.AddFirstPoint(new Vector2(-0.2f, 0.4f));
			o.AddFollowingPoint(new Vector2(0.1f, 0.48f));
			o.AddFollowingPoint(new Vector2(0.2f, 0.36f));
			o.AddFollowingPoint(new Vector2(0.0f, 0.18f));
			o.AddLastPoint(new Vector2(-0.16f, 0.22f));

			testRoom.AddObstacle(o);

			Obstacle o2 = new Obstacle();
			o2.AddFirstPoint(new Vector2(0.8f, 0.8f));
			o2.AddFollowingPoint(new Vector2(0.6f, 0.8f));
			o2.AddFollowingPoint(new Vector2(0.6f, 0.6f));
			o2.AddLastPoint(new Vector2(0.8f, 0.6f));

			//level.AddObstacle(o2);

			NPC n1 = new NPC(new Vector2(-0.84f, 0.84f));
			n1.Affection = 60;
			testRoom.AddNPC(n1);

			NPC n2 = new NPC(new Vector2(0.1f, 0.1f));
			n2.Affection = 0;
			testRoom.AddNPC(n2);

			Level level = new Level();
			level.TestRoom = testRoom;

			return level;
		}

	}
}
