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
			LightRay,
			Obstacle,
			MirrorActive,
			MirrorInactive,
			MirrorRailActive,
			MirrorRailInactive,
			Exit
		}

		public Level Level { get; set; }

		private Vector2 _center = new Vector2(0.0f, 0.0f);
		private float _scale = 1.0f;
		private float _alpha = 1.0f;

		private float _inactiveRoomAlpha = 0.15f;
		private float _defaultScale = 0.9f;

		private Texture _playerTexture;
		private Texture _floorTexture;
		private Texture _exitTexture;
		private Texture _obstacleTexture;

		private Room _currentRoom = null;
		private Room _previousCurrentRoom = null;

		public GameGraphics() : this(null)
		{

		}

		public GameGraphics(Level level)
		{
			Textures.Instance.LoadTextures();

			_playerTexture = Textures.Instance.PlayerTexture;
			_floorTexture = Textures.Instance.FloorTexture;
			_exitTexture = Textures.Instance.ExitTexture;
			_obstacleTexture = Textures.Instance.ObstacleTexture;

			Level = level;
			if (level != null) _currentRoom = Level.CurrentRoom;
		}


		private Vector3 GetColor(Colors color)
		{
			switch (color)
			{
				case Colors.LightRay: return new Vector3(1.0f, 0.6f, 0.0f);
				case Colors.Obstacle: return new Vector3(1.0f, 0.2f, 0.2f);
				case Colors.MirrorActive: return new Vector3(0.4f, 0.5f, 0.94f);
				case Colors.MirrorInactive: return new Vector3(0.4f, 0.45f, 0.5f);
				case Colors.MirrorRailActive: return new Vector3(0.6f, 0.6f, 0.6f);
				case Colors.MirrorRailInactive: return new Vector3(0.5f, 0.5f, 0.5f);
				case Colors.Exit: return new Vector3(1.0f, 0.4f, 0.6f);
			}

			return new Vector3(0.0f, 0.0f, 0.0f);
		}

		private void SetColor(Colors color)
		{
			Vector3 colorVector = GetColor(color);

			colorVector *= _alpha;

			GL.Color3(colorVector.X, colorVector.Y, colorVector.Z);
		}

		public void SetDrawSettings(Vector2 center, float scale, float alpha)
		{
			_center = center;
			_scale = scale;
			_alpha = alpha;
		}

		private bool HasCurrentRoomSwitched()
		{
			return Level.CurrentRoom != _currentRoom;
		}

		private SmoothMovement _smoothMovement;

		private Vector2 GetRelativeDistanceBetweenRooms(Room r1, Room r2)
		{
			float x = (r1.Column - r2.Column) * 2 * _scale;
			float y = (r1.Row - r2.Row) * -2 * _scale;

			return new Vector2(x, y);
		}

		private void CalculateAndSetDrawSettings(Room room, bool isCurrentRoom = false)
		{
			float scale = _smoothMovement != null ? _smoothMovement.GetScale() : _defaultScale;
			Vector2 center = GetRelativeDistanceBetweenRooms(room, Level.CurrentRoom);

			if (_smoothMovement != null) center += _smoothMovement.GetPosition();


			float alpha;

			if (_smoothMovement != null)
			{
				if (isCurrentRoom) alpha = _smoothMovement.GetAlpha();
				else if (room == _previousCurrentRoom) alpha = 1 - _smoothMovement.GetAlpha() + _inactiveRoomAlpha;
				else alpha = _inactiveRoomAlpha;
			}
			else
			{
				alpha = isCurrentRoom ? 1.0f : _inactiveRoomAlpha;
			}

			SetDrawSettings(center, scale, alpha);
		}

		public void DrawLevel()
		{
			if (Level == null) return;

			if (_currentRoom == null) _currentRoom = Level.CurrentRoom;

			if (_smoothMovement != null) _smoothMovement.Tick();

			if (HasCurrentRoomSwitched())
			{
				Vector2 lastPosition = GetRelativeDistanceBetweenRooms(Level.CurrentRoom, _currentRoom);
				_smoothMovement = new SmoothMovement(lastPosition, new Vector2(0.0f, 0.0f), _inactiveRoomAlpha, 1.0f, _defaultScale, _defaultScale);

				_previousCurrentRoom = _currentRoom;
			}

			foreach (Room room in Level.Rooms)
			{
				CalculateAndSetDrawSettings(room, room == Level.CurrentRoom);
				DrawRoom(room, room == Level.CurrentRoom);
			}

			_currentRoom = Level.CurrentRoom;
		}

		public void DrawRoom(Room room, bool drawPlayer)
		{
			DrawRoomBounds(room.RoomBounds);
			DrawFloor(room.RoomBounds);

			DrawMirrors(room.Mirrors);

			if (drawPlayer) DrawPlayer(room.Player);

			DrawExit(room.Exit);
			DrawLightRays(room.LightRays);
			DrawObstacles(room.Obstacles);
		}

		private void DrawFloor(RectangularGameObject room)
		{
			float halfWidth = (room.BottomRight.X - room.TopLeft.X) / 2f;
			float halfHeight = (room.TopLeft.Y - room.BottomRight.Y) / 2f;

			Vector2 topLeft = room.TopLeft;
			Vector2 bottomRight = room.BottomRight;

			float centerX = topLeft.X + halfWidth;
			float centerY = topLeft.Y - halfHeight;

			DrawRectangularTexture(_floorTexture, topLeft, new Vector2(centerX, centerY));
			DrawRectangularTexture(_floorTexture, new Vector2(topLeft.X, centerY), new Vector2(centerX, bottomRight.Y));
			DrawRectangularTexture(_floorTexture, new Vector2(centerX, topLeft.Y), new Vector2(bottomRight.X, centerY));
			DrawRectangularTexture(_floorTexture, new Vector2(centerX, centerY), bottomRight);
		}

		private void DrawRoomBounds(RectangularGameObject room)
		{
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
			DrawBounds(lightRay, 0.004f);

			if (lightRay.GetLines().ToList().Count < 1) return;

			Line lastLine = lightRay.GetLines().ToList().Last();

			Vector2 oppositeDirection = lastLine.GetDirectionVector() * -1;
			oppositeDirection.Normalize();

			Vector2 arrowVector1 = Util.GetRotatedVector(oppositeDirection, 0.9f);
			Vector2 arrowVector2 = Util.GetRotatedVector(oppositeDirection, -0.9f);

			Line arrowLine1 = Line.CreateParameterized(lastLine.Point2, arrowVector1, 0.04f);
			Line arrowLine2 = Line.CreateParameterized(lastLine.Point2, arrowVector2, 0.04f);

			IBounded arrowLines = Util.CreateBoundsFromList(new List<Line>() { arrowLine1, arrowLine2 });

			DrawBounds(arrowLines, 0.004f);
		}

		private void DrawPlayer(Player player)
		{
			Vector2 bL = player.GetRelativePoint(-1, -1);
			Vector2 bR = player.GetRelativePoint(1, -1);
			Vector2 tR = player.GetRelativePoint(1, 1);
			Vector2 tL = player.GetRelativePoint(-1, 1);

			DrawTexture(_playerTexture, bL, bR, tR, tL);

			//SetColor(Colors.LightRay);
			//DrawBounds(player, 0.002f);
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

		private void DrawExit(RectangularGameObject exit)
		{
			SetColor(Colors.Exit);
			DrawRectangularGameObject(exit);
		}

		// --- //

		private void DrawRectangularGameObject(RectangularGameObject rectangularGameObject)
		{
			Vector2 topLeft = GetTransformedVector(rectangularGameObject.TopLeft);
			Vector2 bottomRight = GetTransformedVector(rectangularGameObject.BottomRight);

			DrawSquare(topLeft, bottomRight);
		}

		private void DrawBounds(IBounded bounds, float thickness)
		{
			foreach (Line v in bounds.GetLines()) DrawLine(v, thickness);
		}

		private void DrawLine(Line line, float thickness)
		{
			DrawLine(GetTransformedVector(line.Point1), GetTransformedVector(line.Point2), thickness * _scale);
		}

		private void DrawLine(Vector2 point1, Vector2 point2, float thickness)
		{
			Vector2 normal = point2 - point1;
			normal.Normalize();

			normal *= thickness;

			Vector2 cwRotation = Util.GetOrthogonalVectorCW(normal);
			Vector2 ccwRotation = Util.GetOrthogonalVectorCCW(normal);

			Vector2 point11 = point1 + cwRotation;
			Vector2 point12 = point1 + ccwRotation;
			Vector2 point21 = point2 + cwRotation;
			Vector2 point22 = point2 + ccwRotation;

			GL.Begin(PrimitiveType.Triangles);
			GL.Vertex2(point11);
			GL.Vertex2(point12);
			GL.Vertex2(point22);
			GL.End();

			GL.Begin(PrimitiveType.Triangles);
			GL.Vertex2(point11);
			GL.Vertex2(point21);
			GL.Vertex2(point22);
			GL.End();
		}

		private void DrawRectangularTexture(Texture texture, Vector2 topLeft, Vector2 bottomRight)
		{
			Vector2 bottomLeft = new Vector2(topLeft.X, bottomRight.Y);
			Vector2 topRight = new Vector2(bottomRight.X, topLeft.Y);

			DrawTexture(texture, bottomLeft, bottomRight, topRight, topLeft);
		}

		private void DrawTexture(Texture texture, Vector2 bottomLeft, Vector2 bottomRight, Vector2 topRight, Vector2 topLeft)
		{
			Vector2 bL = GetTransformedVector(bottomLeft);
			Vector2 bR = GetTransformedVector(bottomRight);
			Vector2 tR = GetTransformedVector(topRight);
			Vector2 tL = GetTransformedVector(topLeft);

			DrawTexture2(texture, bL, bR, tR, tL);
		}

		private void DrawTexture2(Texture texture, Vector2 bottomLeft, Vector2 bottomRight, Vector2 topRight, Vector2 topLeft)
		{
			texture.BeginUse();

			GL.Color3(_alpha, _alpha, _alpha);

			GL.Begin(PrimitiveType.Quads);
			GL.TexCoord2(0.0f, 0.0f);
			GL.Vertex2(bottomLeft);
			GL.TexCoord2(1.0f, 0.0f);
			GL.Vertex2(bottomRight);
			GL.TexCoord2(1.0f, 1.0f);
			GL.Vertex2(topRight);
			GL.TexCoord2(0.0f, 1.0f);
			GL.Vertex2(topLeft);
			GL.End();

			texture.EndUse();
		}

		private void DrawSquare(Vector2 topLeft, Vector2 bottomRight)
		{
			GL.Begin(PrimitiveType.Quads);
			GL.Vertex2(topLeft.X, bottomRight.Y);
			GL.Vertex2(topLeft.X, topLeft.Y);
			GL.Vertex2(bottomRight.X, topLeft.Y);
			GL.Vertex2(bottomRight.X, bottomRight.Y);
			GL.End();
		}

		private Vector2 GetTransformedVector(Vector2 vector)
		{
			return new Vector2(vector.X * _scale + _center.X, vector.Y * _scale + _center.Y);
		}

	}
}
