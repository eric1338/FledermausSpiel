﻿using Fledermaus.GameObjects;
using Fledermaus.Utils;
using Framework;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus
{
	class GameGraphics
	{

		private enum Colors
		{
			Player,
			NPC,
			LightRay,
			RoomGround,
			Obstacle,
			MirrorActive,
			MirrorInactive,
			MirrorRailActive,
			MirrorRailInactive,
			SolarPanel,
			ExitOpen,
			ExitClosed
		}

		public Level Level { get; set; }

		private Vector2 _center = new Vector2(0.0f, 0.0f);
		private float _scale = 1.0f;
		private float _alpha = 1.0f;

		private float globalScale = 0.9f;

		public void SetDrawSettings(Vector2 center, float scale, float alpha)
		{
			_center = center;
			_scale = scale;
			_alpha = alpha;
		}

		private Dictionary<Room, RoomDrawSettings> RoomDrawSettingsList = new Dictionary<Room, RoomDrawSettings>();

		private class RoomDrawSettings
		{
			public Vector2 Position { get; set; }
			public float Alpha { get; set; }
			public float Scale { get; set; }

			public SmoothMovement SmoothMovement { get; set; }

			public RoomDrawSettings(Vector2 position, float alpha, float scale)
			{
				Position = position;
				Alpha = alpha;
				Scale = scale;
			}
		}

		private void Tick()
		{
			var roomDrawSettingsList = RoomDrawSettingsList.ToList();

			foreach (var roomDrawSettingsTuple in roomDrawSettingsList)
			{
				var roomDrawSettings = roomDrawSettingsTuple.Value;

				if (roomDrawSettings.SmoothMovement != null) roomDrawSettings.SmoothMovement.Tick();
			}
		}

		private void SetDrawSettings(Room room)
		{
			RoomDrawSettings roomDrawSettings = RoomDrawSettingsList[room];

			if (roomDrawSettings.SmoothMovement != null)
			{
				roomDrawSettings.Position = roomDrawSettings.SmoothMovement.GetPosition();
				if (roomDrawSettings.SmoothMovement.GetAlpha() >= 0) roomDrawSettings.Alpha = roomDrawSettings.SmoothMovement.GetAlpha();
				if (roomDrawSettings.SmoothMovement.GetScale() >= 0) roomDrawSettings.Scale = roomDrawSettings.SmoothMovement.GetScale();
			}

			SetDrawSettings(roomDrawSettings.Position, roomDrawSettings.Scale, roomDrawSettings.Alpha);
		}


		private Room _lastCurrentRoom = null;

		private bool ShouldMove()
		{
			return Level.CurrentRoom != _lastCurrentRoom;
		}

		private void InitializeRoomDrawSettings()
		{
			RoomDrawSettings currentRoomDrawSettings = new RoomDrawSettings(new Vector2(0, 0), 1.0f, globalScale);

			RoomDrawSettingsList.Add(Level.CurrentRoom, currentRoomDrawSettings);

			foreach (Room room in Level.GetOtherRooms())
			{
				float x = (room.Column - Level.CurrentRoom.Column) * 2 * globalScale;
				float y = (room.Row - Level.CurrentRoom.Row) * -2 * globalScale;

				RoomDrawSettings roomDrawSettings = new RoomDrawSettings(new Vector2(x, y), 0.1f, globalScale);

				RoomDrawSettingsList.Add(room, roomDrawSettings);
			}
		}

		private void UpdateRoomDrawSettings()
		{
			RoomDrawSettings currentRoomDrawSettings = RoomDrawSettingsList[Level.CurrentRoom];
			currentRoomDrawSettings.SmoothMovement = new SmoothMovement(currentRoomDrawSettings.Position, new Vector2(0.0f, 0.0f), currentRoomDrawSettings.Alpha, 1.0f);

			foreach (Room room in Level.GetOtherRooms())
			{
				float x = (room.Column - Level.CurrentRoom.Column) * 2 * globalScale;
				float y = (room.Row - Level.CurrentRoom.Row) * -2 * globalScale;

				RoomDrawSettings roomDrawSettings = RoomDrawSettingsList[room];
				roomDrawSettings.SmoothMovement = new SmoothMovement(roomDrawSettings.Position, new Vector2(x, y), roomDrawSettings.Alpha, 0.1f);
			}
		}

		public void DrawLevel()
		{
			Tick();

			if (ShouldMove())
			{
				if (RoomDrawSettingsList.Count < 1)
				{
					InitializeRoomDrawSettings();
				}
				else
				{
					UpdateRoomDrawSettings();
				}
			}

			foreach (Room room in Level.Rooms)
			{
				SetDrawSettings(room);
				DrawRoom(room);
			}

			_lastCurrentRoom = Level.CurrentRoom;
		}

		public void DrawRoom(Room room)
		{
			DrawRoomBounds(room.RoomBounds);
			DrawPlayer(room.Player);
			DrawNPCs(room.NPCs);

			DrawLightRays(room.LightRays);
			DrawMirrors(room.Mirrors);
			DrawObstacles(room.Obstacles);
			DrawSolarPanel(room.SolarPanel);
			DrawExit(room.Exit, room.IsExitOpen);
		}

		private Vector3 GetColor(Colors color)
		{
			switch (color)
			{
				case Colors.Player: return new Vector3(1.0f, 0.8f, 0.6f);
				case Colors.NPC: return new Vector3(1.0f, 0.4f, 0.0f);
				case Colors.LightRay: return new Vector3(1.0f, 0.6f, 0.0f);
				case Colors.RoomGround: return new Vector3(0.16f, 0.16f, 0.16f);
				case Colors.Obstacle: return new Vector3(1.0f, 0.2f, 0.2f);
				case Colors.MirrorActive: return new Vector3(0.4f, 0.5f, 0.94f);
				case Colors.MirrorInactive: return new Vector3(0.4f, 0.45f, 0.5f);
				case Colors.MirrorRailActive: return new Vector3(0.6f, 0.6f, 0.6f);
				case Colors.MirrorRailInactive: return new Vector3(0.5f, 0.5f, 0.5f);
				case Colors.SolarPanel: return new Vector3(0.3f, 0.3f, 1.0f);
				case Colors.ExitOpen: return new Vector3(0.4f, 1.0f, 0.3f);
				case Colors.ExitClosed: return new Vector3(1.0f, 0.4f, 0.6f);
			}

			return new Vector3(0.0f, 0.0f, 0.0f);
		}

		private void SetColor(Colors color)
		{
			Vector3 colorVector = GetColor(color);

			colorVector *= _alpha;

			GL.Color4(colorVector.X, colorVector.Y, colorVector.Z, 1.0f);
		}

		private void DrawRoomBounds(RectangularGameObject room)
		{
			SetColor(Colors.RoomGround);
			DrawRectangularGameObject(room);

			GL.Color3(0.1f, 0.1f, 0.1f);
			DrawBounds(room, 0.003f);
		}

		private void DrawLightRays(List<LightRay> lightRays)
		{
			foreach (LightRay lightRay in lightRays) DrawLightRay(lightRay);
		}

		private void DrawLightRay(LightRay lightRay)
		{
			SetColor(Colors.LightRay);
			DrawBounds(lightRay, 0.005f);
		}

		private void DrawPlayer(Player player)
		{
			SetColor(Colors.Player);
			DrawBounds(player, 0.002f);
		}

		private void DrawNPCs(List<NPC> npcs)
		{
			foreach (NPC npc in npcs) DrawNPC(npc);
		}

		private void DrawNPC(NPC npc)
		{
			SetColor(Colors.NPC);
			DrawBounds(npc, 0.002f);
		}

		private void DrawMirrors(List<Mirror> mirrors)
		{
			foreach (Mirror m in mirrors) DrawMirror(m);
		}

		private void DrawMirror(Mirror mirror)
		{
			SetColor(mirror.IsAccessible ? Colors.MirrorRailActive : Colors.MirrorRailInactive);
			DrawLine(new Line(mirror.RailPosition1, mirror.RailPosition2), 0.005f);

			SetColor(mirror.IsAccessible ? Colors.MirrorActive : Colors.MirrorInactive);
			DrawLine(mirror.GetMirrorLine(), 0.006f);
		}

		private void DrawObstacles(List<Obstacle> obstacles)
		{
			foreach (Obstacle o in obstacles) DrawObstacle(o);
		}

		private void DrawObstacle(Obstacle obstacle)
		{
			SetColor(Colors.Obstacle);
			DrawBounds(obstacle, 0.003f);
		}

		private void DrawSolarPanel(RectangularGameObject solarPanel)
		{
			SetColor(Colors.SolarPanel);
			DrawRectangularGameObject(solarPanel);
		}

		private void DrawExit(RectangularGameObject exit, bool isExitOpen)
		{
			SetColor(isExitOpen ? Colors.ExitOpen : Colors.ExitClosed);
			DrawRectangularGameObject(exit);
		}

		// --- //

		private void DrawRectangularGameObject(RectangularGameObject rectangularGameObject)
		{
			Vector2 topLeft = GetTransformedVector(rectangularGameObject.Point1);
			Vector2 bottomRight = GetTransformedVector(rectangularGameObject.Point3);

			DrawSquare(topLeft, bottomRight);
		}

		private void DrawBounds(IBounded bounds, float thickness)
		{
			foreach (Line v in bounds.GetLines()) DrawLine(v, thickness);
		}

		private void DrawLine(Line line, float thickness)
		{
			DrawLine(GetTransformedVector(line.Point1), GetTransformedVector(line.Point2), thickness);
		}

		private void DrawLine(Vector2 p1, Vector2 p2, float thickness)
		{
			Vector2 normal = p2 - p1;
			normal.Normalize();

			normal *= thickness;

			Vector2 lR = Util.GetOrthogonalVectorCW(normal);
			Vector2 rR = Util.GetOrthogonalVectorCCW(normal);

			Vector2 p11 = p1 + lR;
			Vector2 p12 = p1 + rR;
			Vector2 p21 = p2 + lR;
			Vector2 p22 = p2 + rR;

			GL.Begin(PrimitiveType.Triangles);
			GL.Vertex2(p11);
			GL.Vertex2(p12);
			GL.Vertex2(p22);
			GL.End();

			GL.Begin(PrimitiveType.Triangles);
			GL.Vertex2(p11);
			GL.Vertex2(p21);
			GL.Vertex2(p22);
			GL.End();
		}

		private Vector2 GetTransformedVector(Vector2 vector)
		{
			return new Vector2(vector.X * _scale + _center.X, vector.Y * _scale + _center.Y);
		}

		private void DrawSquare(Vector2 topLeft, Vector2 bottomRight)
		{
			GL.Begin(PrimitiveType.Quads);
			GL.Vertex2(topLeft.X, topLeft.Y);
			GL.Vertex2(bottomRight.X, topLeft.Y);
			GL.Vertex2(bottomRight.X, bottomRight.Y);
			GL.Vertex2(topLeft.X, bottomRight.Y);
			GL.End();
		}

		private void DrawCoordinateSystem()
		{
			GL.Color3(1.0f, 0.0f, 0.3f);

			GL.Begin(PrimitiveType.Lines);
			GL.Vertex2(0f, 1f);
			GL.Vertex2(0f, -1f);
			GL.End();
			GL.Begin(PrimitiveType.Lines);
			GL.Vertex2(-1f, 0f);
			GL.Vertex2(1f, 0f);
			GL.End();
		}

	}
}
