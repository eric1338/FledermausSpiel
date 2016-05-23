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

            testRoom.RoomBounds = new RectangularGameObject(new Vector2(-1f, 1f), new Vector2(1f, -1f));

			testRoom.Player = new Player(new Vector2(0.9f, -0.8f));

			RectangularGameObject exit = new RectangularGameObject(new Vector2(-1.04f, -0.6f), new Vector2(-0.975f, -0.8f));

			testRoom.SolarPanel = new RectangularGameObject(new Vector2(-1.04f, 0.2f), new Vector2(-0.975f, 0.0f));
			testRoom.Exit = exit;
			testRoom.AddLightRay(new LightRay(new Vector2(0.99f, 0.99f), new Vector2(-0.16f, -0.28f)));
			testRoom.AddLightRay(new LightRay(new Vector2(0.89f, 0.89f), new Vector2(-0.26f, -0.38f)));

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
			//testRoom.AddNPC(n1);

			NPC n2 = new NPC(new Vector2(0.1f, 0.1f));
			n2.Affection = 0;
			//testRoom.AddNPC(n2);

			Level level = new Level();

			testRoom.Row = 1;
			testRoom.Column = 1;
			level.AddRoom(testRoom);

			Room testRoom1 = CreateTestRoom1(0, 2);
			testRoom1.Row = 0;
			testRoom1.Column = 1;
			level.AddRoom(testRoom1);

			Room testRoom2 = CreateTestRoom1(0.6f, 3);
			testRoom2.Row = 0;
			testRoom2.Column = 0;
			level.AddRoom(testRoom2);

			Room testRoom3 = CreateTestRoom1(1.2f, 0);
			testRoom3.Row = 1;
			testRoom3.Column = 0;
			level.AddRoom(testRoom3);

			testRoom.Index = 0;
			testRoom1.Index = 1;
			testRoom2.Index = 2;
			testRoom3.Index = 3;

			testRoom.AddRoomTransitionTrigger(exit, 1);

			level.CurrentRoom = testRoom;

			return level;
		}

		private static Room CreateTestRoom1(float x, int nextRoom)
		{
			Room testRoom1 = new Room();

			testRoom1.RoomBounds = new RectangularGameObject(new Vector2(-1f, 1f), new Vector2(1f, -1f));

			testRoom1.Player = new Player(new Vector2(0.9f, -0.8f));

			testRoom1.SolarPanel = new RectangularGameObject(new Vector2(-1.04f, 0.2f), new Vector2(-0.975f, 0.0f));

			RectangularGameObject exit = new RectangularGameObject(new Vector2(-1.04f, -0.6f), new Vector2(-0.975f, -0.8f));

			testRoom1.Exit = new RectangularGameObject(new Vector2(-1.04f, -0.6f), new Vector2(-0.975f, -0.8f));

			testRoom1.AddLightRay(new LightRay(new Vector2(0.99f, -0.89f), new Vector2(-0.16f, -0.28f)));
			testRoom1.AddLightRay(new LightRay(new Vector2(0.89f, -0.79f), new Vector2(-0.26f, -0.38f)));

			Obstacle o = new Obstacle();
			o.AddFirstPoint(new Vector2(-0.7f + x, 0.4f));
			o.AddFollowingPoint(new Vector2(-0.6f + x, 0.48f));
			o.AddLastPoint(new Vector2(-0.66f + x, 0.22f));

			testRoom1.AddObstacle(o);

			testRoom1.AddRoomTransitionTrigger(exit, nextRoom);

			return testRoom1;
		}

	}
}
