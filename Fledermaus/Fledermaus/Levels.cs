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
			Level level = CreateLevel1();

			return level;
		}

		private static Room GetStdRoom()
		{
			Room room = new Room();
			room.RoomBounds = new RectangularGameObject(new Vector2(-1f, 1f), new Vector2(1f, -1f));
			room.SolarPanel = new RectangularGameObject(new Vector2(101f, 101f), new Vector2(100f, 100f));

			return room;
		}

		private static void SetStdValues(Room room)
		{
			room.RoomBounds = new RectangularGameObject(new Vector2(-1f, 1f), new Vector2(1f, -1f));
			room.SolarPanel = new RectangularGameObject(new Vector2(101f, 101f), new Vector2(100f, 100f));
		}

		private static Level CreateLevel1()
		{
			Level level1 = new Level();

			Room r1 = CreateL1R1();
			r1.Index = 1;
			r1.Row = 2;
			r1.Column = 3;
			r1.AddRoomTransitionTrigger(r1.Exit, 2);

			Room r2 = CreateL1R2();
			r2.Index = 2;
			r2.Row = 1;
			r2.Column = 3;
			r2.AddRoomTransitionTrigger(r2.Exit, 3);

			Room r3 = CreateL1R3();
			r3.Index = 3;
			r3.Row = 1;
			r3.Column = 2;
			r3.AddRoomTransitionTrigger(r3.Exit, 4);

			Room r4 = CreateL1R4();
			r4.Index = 4;
			r4.Row = 2;
			r4.Column = 2;
			r4.AddRoomTransitionTrigger(r4.Exit, 5);

			Room r5 = CreateL1R5();
			r5.Index = 5;
			r5.Row = 2;
			r5.Column = 1;

			level1.AddRoom(r1);
			level1.AddRoom(r2);
			level1.AddRoom(r3);
			level1.AddRoom(r4);
			level1.AddRoom(r5);

			level1.CurrentRoom = r1;

			return level1;
		}

		private static Level CreateLevel2()
		{
			Level level2 = new Level();

			Room r1 = CreateL2R1();
			r1.Index = 1;
			r1.Row = 2;
			r1.Column = 1;
			r1.AddRoomTransitionTrigger(r1.Exit, 2);

			Room r2 = CreateL2R2();
			r2.Index = 2;
			r2.Row = 1;
			r2.Column = 1;
			r2.AddRoomTransitionTrigger(r2.Exit, 3);

			Room r3 = CreateL2R3();
			r3.Index = 3;
			r3.Row = 1;
			r3.Column = 2;
			r3.AddRoomTransitionTrigger(r3.Exit, 4);

			Room r4 = CreateL2R4();
			r4.Index = 4;
			r4.Row = 2;
			r4.Column = 2;
			r4.AddRoomTransitionTrigger(r4.Exit, 5);

			Room r5 = CreateL2R5();
			r5.Index = 5;
			r5.Row = 3;
			r5.Column = 2;

			level2.AddRoom(r1);
			level2.AddRoom(r2);
			level2.AddRoom(r3);
			level2.AddRoom(r4);
			level2.AddRoom(r5);

			level2.CurrentRoom = r1;

			return level2;
		}

		private static Level CreateLevel3()
		{
			Level level3 = new Level();

			Room r1 = CreateL2R1();
			r1.Index = 1;
			r1.Row = 2;
			r1.Column = 1;
			r1.AddRoomTransitionTrigger(r1.Exit, 2);

			Room r2 = CreateL2R2();
			r2.Index = 2;
			r2.Row = 1;
			r2.Column = 1;
			r2.AddRoomTransitionTrigger(r2.Exit, 3);

			Room r3 = CreateL3R3();
			r3.Index = 3;
			r3.Row = 2;
			r3.Column = 1;
			r3.AddRoomTransitionTrigger(r3.Exit, 4);

			Room r4 = CreateL3R4();
			r4.Index = 4;
			r4.Row = 3;
			r4.Column = 1;
			r4.AddRoomTransitionTrigger(r4.Exit, 5);

			Room r5 = CreateL3R5();
			r5.Index = 5;
			r5.Row = 3;
			r5.Column = 2;

			//level3.AddRoom(r1);
			//level3.AddRoom(r2);
			level3.AddRoom(r3);
			level3.AddRoom(r4);
			level3.AddRoom(r5);

			level3.CurrentRoom = r3;

			return level3;
		}

		// Level 1

		private static Room CreateL1R1()
		{
			Room room = GetStdRoom();

			room.Player = new Player(new Vector2(-0.9f, -0.9f));

			room.AddLightRay(new LightRay(new Vector2(-0.3f, 0.98f), new Vector2(0.1f, -0.4f)));

			Mirror m1 = new Mirror(new Vector2(-0.4f, -0.2f), new Vector2(0.0f, -0.3f));
			m1.SetRotationBounds(0.0f, 0.9f);
			room.AddMirror(m1);

			room.AddObstacle(Obstacle.CreateRectangular(new Vector2(0.6f, -0.6f), new Vector2(0.75f, -0.75f)));

			room.Exit = new RectangularGameObject(new Vector2(0.65f, 1.03f), new Vector2(0.90f, 0.97f));

			return room;
		}

		private static Room CreateL1R2()
		{
			Room room = GetStdRoom();

			room.Player = new Player(new Vector2(0.9f, -0.9f));

			room.AddLightRay(new LightRay(new Vector2(-0.98f, -0.2f), new Vector2(0.32f, 0.1f)));

			Mirror m1 = new Mirror(new Vector2(0.0f, -0.25f), new Vector2(0.1f, 0.4f), 0.5f, 0.2f);
			m1.SetRotationBounds(0.45f, 0.55f);
			room.AddMirror(m1);
			Mirror m2 = new Mirror(new Vector2(0.3f, 0.6f), new Vector2(0.55f, 0.25f));
			room.AddMirror(m2);

			room.Exit = new RectangularGameObject(new Vector2(-1.03f, 0.9f), new Vector2(-0.97f, 0.65f));

			return room;
		}

		private static Room CreateL1R3()
		{
			Room room = GetStdRoom();

			room.Player = new Player(new Vector2(0.9f, 0.9f));

			room.AddLightRay(new LightRay(new Vector2(0.98f, 0.45f), new Vector2(-0.8f, 0.1f)));

			Mirror m1 = new Mirror(new Vector2(-0.5f, 0.3f), new Vector2(-0.3f, 0.8f), 0.5f, 0.9f);
			m1.SetRotationBounds(0.4f, 0.8f);
			room.AddMirror(m1);
			Mirror m2 = new Mirror(new Vector2(-0.8f, 0.0f), new Vector2(-0.55f, -0.4f));
			m2.SetRotationBounds(0.0f, 0.7f);
			room.AddMirror(m2);

			room.AddObstacle(Obstacle.CreateRectangular(new Vector2(0.2f, -0.4f), new Vector2(0.4f, -0.8f)));

			room.Exit = new RectangularGameObject(new Vector2(0.65f, -0.97f), new Vector2(0.9f, -1.03f));

			return room;
		}

		private static Room CreateL1R4()
		{
			Room room = GetStdRoom();

			room.Player = new Player(new Vector2(0.9f, 0.9f));

			room.AddLightRay(new LightRay(new Vector2(0.3f, 0.98f), new Vector2(-0.1f, -0.12f)));
			//room.AddLightRay(new LightRay(new Vector2(0.5f, 0.98f), new Vector2(-0.1f, -0.9f)));
			room.AddLightRay(new LightRay(new Vector2(-0.98f, 0.4f), new Vector2(0.2f, -0.1f)));
			room.AddLightRay(new LightRay(new Vector2(0.98f, -0.4f), new Vector2(-1.0f, -0.05f)));

			Mirror m1 = new Mirror(new Vector2(-0.11f, 0.6f), new Vector2(0.4f, 0.82f), -0.3f, 0.8f);
			m1.SetRotationBounds(-0.32f, 0.0f);
			room.AddMirror(m1);
			Mirror m2 = new Mirror(new Vector2(-0.82f, 0.72f), new Vector2(-0.63f, -0.08f), -0.4f, 0.2f);
			m2.SetRotationBounds(-0.5f, 0.0f);
			room.AddMirror(m2);
			Mirror m3 = new Mirror(new Vector2(-0.36f, -0.1f), new Vector2(-0.3f, -0.6f), 1.0f, 0.2f);
			m3.SetRotationBounds(0.8f, 1.6f);
			room.AddMirror(m3);
			Mirror m4 = new Mirror(new Vector2(0.63f, -0.08f), new Vector2(0.58f, -0.8f), 0.6f, 0.25f);
			m4.SetRotationBounds(0.4f, 0.8f);
			room.AddMirror(m4);

			room.AddObstacle(Obstacle.CreateRectangular(new Vector2(-0.42f, 0.72f), new Vector2(-0.28f, 0.58f)));

			room.Exit = new RectangularGameObject(new Vector2(-1.03f, -0.65f), new Vector2(-0.97f, -0.9f));

			return room;
		}

		private static Room CreateL1R5()
		{
			Room room = GetStdRoom();

			room.Player = new Player(new Vector2(0.9f, -0.9f));

			room.AddLightRay(new LightRay(new Vector2(-0.98f, 0.2f), new Vector2(0.5f, 0.1f)));
			room.AddLightRay(new LightRay(new Vector2(-0.98f, 0.0f), new Vector2(0.65f, -0.1f)));
			room.AddLightRay(new LightRay(new Vector2(0.2f, 0.98f), new Vector2(0.1f, -0.20f)));

			Mirror m1 = new Mirror(new Vector2(-0.8f, -0.3f), new Vector2(-0.62f, 0.55f), 0.5f, 0.1f);
			room.AddMirror(m1);
			Mirror m2 = new Mirror(new Vector2(-0.2f, 0.2f), new Vector2(-0.28f, -0.4f), -0.9f, 0.5f);
			m2.SetRotationBounds(-1.4f, -0.7f);
			room.AddMirror(m2);
			Mirror m3 = new Mirror(new Vector2(0.0f, 0.8f), new Vector2(0.62f, 0.58f), 0.5f, 0.2f);
			room.AddMirror(m3);
			Mirror m4 = new Mirror(new Vector2(0.24f, 0.0f), new Vector2(0.8f, 0.1f), 0.8f, 0.2f);
			m4.SetRotationBounds(0.5f, 1.0f);
			room.AddMirror(m4);

			room.AddObstacle(Obstacle.CreateRectangular(new Vector2(-0.05f, -0.28f), new Vector2(0.2f, -0.6f)));
			room.AddObstacle(Obstacle.CreateRectangular(new Vector2(-0.45f, 0.8f), new Vector2(-0.2f, 0.55f)));

			room.Exit = new RectangularGameObject(new Vector2(0.65f, 1.03f), new Vector2(0.9f, 0.97f));

			return room;
		}

		// Level 2

		private static Room CreateL2R1()
		{
			Room room = GetStdRoom();

			room.Player = new Player(new Vector2(0.9f, -0.9f));

			room.AddLightRay(new LightRay(new Vector2(-0.1f, -0.98f), new Vector2(0.1f, 0.15f)));

			Mirror m1 = new Mirror(new Vector2(0.3f, -0.2f), new Vector2(0.44f, -0.7f), 0.6f, 0.8f);
			m1.SetRotationBounds(0.5f, 0.8f);
			room.AddMirror(m1);
			Mirror m2 = new Mirror(new Vector2(0.55f, 0.23f), new Vector2(0.6f, 0.58f), 0.05f, 0.4f);
			m2.SetRotationBounds(-0.8f, 0.1f);
			room.AddMirror(m2);

			room.AddObstacle(Obstacle.CreateRectangular(new Vector2(-0.36f, 0.7f), new Vector2(-0.16f, 0.5f)));
			room.AddObstacle(Obstacle.CreateRectangular(new Vector2(-0.42f, -0.05f), new Vector2(-0.22f, -0.25f)));
			room.AddObstacle(Obstacle.CreateRectangular(new Vector2(-0.56f, -0.63f), new Vector2(-0.36f, -0.83f)));

			room.Exit = new RectangularGameObject(new Vector2(-0.9f, 1.03f), new Vector2(-0.65f, 0.97f));

			return room;
		}

		private static Room CreateL2R2()
		{
			Room room = GetStdRoom();

			room.Player = new Player(new Vector2(-0.9f, -0.9f));

			room.Exit = new RectangularGameObject(new Vector2(0.97f, 0.65f), new Vector2(1.03f, 0.9f));

			return room;
		}

		private static Room CreateL2R3()
		{
			Room room = GetStdRoom();

			room.Player = new Player(new Vector2(-0.9f, 0.9f));

			room.AddLightRay(new LightRay(new Vector2(-0.98f, 0.2f), new Vector2(0.29f, 0.1f)));
			room.AddLightRay(new LightRay(new Vector2(0.98f, -0.58f), new Vector2(-0.2f, 0.1f)));

			Mirror m1 = new Mirror(new Vector2(-0.82f, 0.38f), new Vector2(-0.18f, 0.42f), -0.15f, 0.3f);
			m1.SetRotationBounds(-0.2f, 0.5f);
			room.AddMirror(m1);
			Mirror m2 = new Mirror(new Vector2(0.58f, 0.78f), new Vector2(0.72f, 0.19f), -0.5f, 0.4f);
			m2.SetRotationBounds(-0.9f, 0.1f);
			room.AddMirror(m2);
			Mirror m3 = new Mirror(new Vector2(0.05f, -0.22f), new Vector2(0.68f, -0.19f));
			room.AddMirror(m3);
			Mirror m4 = new Mirror(new Vector2(-0.64f, -0.1f), new Vector2(-0.58f, -0.54f));
			room.AddMirror(m4);

			room.Exit = new RectangularGameObject(new Vector2(0.65f, -0.97f), new Vector2(0.9f, -1.03f));

			return room;
		}

		private static Room CreateL2R4()
		{
			Room room = GetStdRoom();

			room.Player = new Player(new Vector2(0.9f, 0.9f));

			room.Exit = new RectangularGameObject(new Vector2(-0.65f, -0.97f), new Vector2(-0.9f, -1.03f));

			return room;
		}

		private static Room CreateL2R5()
		{
			Room room = GetStdRoom();

			room.Player = new Player(new Vector2(-0.9f, 0.9f));

			room.AddLightRay(new LightRay(new Vector2(-0.98f, 0.6f), new Vector2(1.0f, 0.025f)));
			room.AddLightRay(new LightRay(new Vector2(-0.98f, -0.84f), new Vector2(1.0f, 0.06f)));

			Mirror m1 = new Mirror(new Vector2(0.64f, 0.85f), new Vector2(0.7f, 0.44f), -0.4f, 0.1f);
			m1.SetRotationBounds(-0.9f, -0.3f);
			room.AddMirror(m1);
			Mirror m2 = new Mirror(new Vector2(0.52f, -0.26f), new Vector2(0.76f, 0.02f), -0.36f, 0.75f);
			m2.SetRotationBounds(-0.45f, 0.1f);
			room.AddMirror(m2);
			Mirror m3 = new Mirror(new Vector2(-0.48f, 0.0f), new Vector2(-0.28f, -0.34f), -0.94f, 0.9f);
			m3.SetRotationBounds(-1.2f, -0.7f);
			room.AddMirror(m3);
			Mirror m4 = new Mirror(new Vector2(-0.83f, 0.08f), new Vector2(-0.77f, -0.86f), -0.4f, 0.1f);
			room.AddMirror(m4);

			room.Exit = new RectangularGameObject(new Vector2(0.97f, -0.65f), new Vector2(1.03f, -0.9f));

			return room;
		}

		// Level 3

		private static Room CreateL3R1()
		{
			Room room = GetStdRoom();

			room.Player = new Player(new Vector2(0.9f, 0.9f));

			room.Exit = new RectangularGameObject(new Vector2(-0.9f, -0.97f), new Vector2(-0.65f, -1.03f));

			return room;
		}

		private static Room CreateL3R2()
		{
			Room room = GetStdRoom();

			room.Player = new Player(new Vector2(0.9f, 0.9f));

			room.Exit = new RectangularGameObject(new Vector2(-0.9f, -0.97f), new Vector2(-0.65f, -1.03f));

			return room;
		}

		private static Room CreateL3R3()
		{
			Room room = GetStdRoom();

			room.Player = new Player(new Vector2(0.9f, 0.9f));

			room.AddLightRay(new LightRay(new Vector2(-0.2f, 0.98f), new Vector2(0.1f, -0.1f)));
			room.AddLightRay(new LightRay(new Vector2(-0.5f, 0.4f), new Vector2(0.13f, -0.1f)));
			room.AddLightRay(new LightRay(new Vector2(-0.98f, 0.05f), new Vector2(0.1f, -0.1f)));
			room.AddLightRay(new LightRay(new Vector2(-0.98f, -0.65f), new Vector2(1.0f, -0.03f)));

			Mirror m1 = new Mirror(new Vector2(0.0f, 0.44f), new Vector2(0.6f, 0.36f), 0.5f, 0.8f);
			room.AddMirror(m1);
			Mirror m2 = new Mirror(new Vector2(-0.45f, -0.14f), new Vector2(-0.2f, -0.2f), 0.5f, 0.2f);
			room.AddMirror(m2);
			Mirror m3 = new Mirror(new Vector2(0.4f, -0.75f), new Vector2(0.78f, -0.6f), 0.5f, 0.25f);
			room.AddMirror(m3);
			Mirror m4 = new Mirror(new Vector2(-0.45f, -0.8f), new Vector2(0.18f, -0.82f), 0.5f, 0.8f);
			room.AddMirror(m4);

			room.AddObstacle(Obstacle.CreateRectangular(new Vector2(-0.6f, 0.6f), new Vector2(-0.4f, 0.4f)));

			room.Exit = new RectangularGameObject(new Vector2(-0.9f, -0.97f), new Vector2(-0.65f, -1.03f));

			return room;
		}

		private static Room CreateL3R4()
		{
			Room room = GetStdRoom();

			room.Player = new Player(new Vector2(-0.9f, 0.9f));

			room.AddLightRay(new LightRay(new Vector2(-0.8f, 0.1f), new Vector2(0.1f, 0.3f)));
			room.AddLightRay(new LightRay(new Vector2(-0.4f, 0.98f), new Vector2(-0.1f, -0.65f)));
			room.AddLightRay(new LightRay(new Vector2(-0.7f, -0.8f), new Vector2(0.1f, 0.2f)));
			room.AddLightRay(new LightRay(new Vector2(0.98f, 0.44f), new Vector2(-0.9f, -0.1f)));

			Mirror m1 = new Mirror(new Vector2(-0.94f, 0.54f), new Vector2(-0.6f, 0.36f));
			m1.RelativePosition = 0.2f;
			room.AddMirror(m1);
			Mirror m2 = new Mirror(new Vector2(-0.76f, -0.14f), new Vector2(-0.43f, 0.0f));
			m2.RelativePosition = 0.65f;
			room.AddMirror(m2);
			Mirror m3 = new Mirror(new Vector2(-0.72f, -0.32f), new Vector2(-0.34f, -0.56f));
			m3.RelativePosition = 0.2f;
			room.AddMirror(m3);
			Mirror m4 = new Mirror(new Vector2(-0.9f, -0.6f), new Vector2(-0.5f, -0.7f));
			m4.RelativePosition = 0.2f;
			room.AddMirror(m4);
			Mirror m5 = new Mirror(new Vector2(0.28f, 0.86f), new Vector2(0.38f, 0.52f));
			m5.RelativePosition = 0.4f;
			room.AddMirror(m5);
			Mirror m6 = new Mirror(new Vector2(0.39f, 0.34f), new Vector2(0.41f, 0.36f));
			m6.RelativePosition = 0.5f;
			room.AddMirror(m6);
			Mirror m7 = new Mirror(new Vector2(0.23f, -0.32f), new Vector2(0.6f, -0.43f));
			m7.RelativePosition = 0.7f;
			room.AddMirror(m7);

			room.AddObstacle(Obstacle.CreateRectangular(new Vector2(-1.0f, 0.1f), new Vector2(-0.8f, -0.1f)));
			room.AddObstacle(Obstacle.CreateRectangular(new Vector2(-0.8f, -0.8f), new Vector2(-0.6f, -1.0f)));
			room.AddObstacle(Obstacle.CreateRectangular(new Vector2(-0.2f, -0.6f), new Vector2(0.0f, -0.8f)));
			room.AddObstacle(Obstacle.CreateRectangular(new Vector2(-0.1f, 0.7f), new Vector2(0.1f, 0.5f)));
			room.AddObstacle(Obstacle.CreateRectangular(new Vector2(0.6f, 0.74f), new Vector2(0.8f, 0.54f)));

			room.Exit = new RectangularGameObject(new Vector2(0.97f, -0.65f), new Vector2(1.03f, -0.9f));

			return room;
		}

		private static Room CreateL3R5()
		{
			Room room = GetStdRoom();

			room.Player = new Player(new Vector2(-0.9f, -0.9f));

			Obstacle obstacle = new Obstacle();
			obstacle.AddFirstPoint(new Vector2(-0.7f, 0.1f));
			obstacle.AddFollowingPoint(new Vector2(-0.6f, 0.08f));
			obstacle.AddLastPoint(new Vector2(-0.66f, 0.22f));

			room.AddObstacle(obstacle);

			room.AddLightRay(new LightRay(new Vector2(0.05f, 0.98f), new Vector2(0.05f, -1.0f)));
			room.AddLightRay(new LightRay(new Vector2(0.98f, -0.25f), new Vector2(-0.5f, -0.05f)));

			Mirror m1 = new Mirror(new Vector2(-0.2f, -0.3f), new Vector2(0.1f, -0.68f), -0.79f, 0.5f);
			room.AddMirror(m1);
			Mirror m2 = new Mirror(new Vector2(-0.2f, 0.86f), new Vector2(0.1f, 0.6f), 0.73f, 0.9f);
			m2.MinimumRotation = 0.1f;
			room.AddMirror(m2);
			Mirror m3 = new Mirror(new Vector2(0.7f, -0.3f), new Vector2(0.8f, -0.85f));
			room.AddMirror(m3);

			room.Exit = new RectangularGameObject(new Vector2(0.6f, 1.04f), new Vector2(0.8f, 0.975f));

			return room;
		}





		private static Room CreateFirstRoom()
		{
			Room r = new Room();

			SetStdValues(r);

			r.Player = new Player(new Vector2(-0.9f, -0.9f));

			Obstacle o = new Obstacle();
			o.AddFirstPoint(new Vector2(-0.7f, 0.1f));
			o.AddFollowingPoint(new Vector2(-0.6f, 0.08f));
			o.AddFollowingPoint(new Vector2(-0.6f, 0.08f));
			o.AddFollowingPoint(new Vector2(-0.6f, 0.08f));
			o.AddLastPoint(new Vector2(-0.66f, 0.22f));

			r.AddObstacle(o);

			r.AddLightRay(new LightRay(new Vector2(0.05f, 0.98f), new Vector2(0.05f, -1.0f)));
			r.AddLightRay(new LightRay(new Vector2(0.98f, -0.25f), new Vector2(-0.5f, -0.05f)));

			Mirror m1 = new Mirror(new Vector2(-0.2f, -0.3f), new Vector2(0.1f, -0.68f), -45f, 0.5f);
			r.AddMirror(m1);

			Mirror m2 = new Mirror(new Vector2(-0.2f, 0.86f), new Vector2(0.1f, 0.6f), 41.5f, 0.9f);
			m2.MinimumRotation = 0.1f;
			r.AddMirror(m2);

			Mirror m3 = new Mirror(new Vector2(0.7f, -0.3f), new Vector2(0.8f, -0.85f));
			r.AddMirror(m3);

			RectangularGameObject exit = new RectangularGameObject(new Vector2(0.6f, 1.04f), new Vector2(0.8f, 0.975f));
			r.Exit = exit;


			Mirror t1 = new Mirror(new Vector2(-0.96f, 0.6f), new Vector2(-0.96f, 0.5f), 0.0f, 0.5f);
			Mirror t2 = new Mirror(new Vector2(-0.86f, 0.6f), new Vector2(-0.86f, 0.5f), 0.0f, 0.5f);

			r.AddMirror(t1);
			r.AddMirror(t2);

			r.AddLightRay(new LightRay(new Vector2(-0.9f, 0.55f), new Vector2(1.0f, 0.0f)));

			r.AddRoomTransitionTrigger(exit, 1);

			return r;
		}

	}
}
