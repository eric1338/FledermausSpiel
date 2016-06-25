using Fledermaus.GameObjects;
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

		public Level Level { get; set; }

		private Vector2 _center = new Vector2(0.0f, 0.0f);
		private float _scale = 1.0f;
		private float _alpha = 1.0f;

		private float _inactiveRoomAlpha = 0.15f;
		private float _defaultScale = 0.9f;

		private Texture _playerTexture;
		private Texture _playerHitTexture;
		private Texture _floorTexture;
		private Texture _exitTexture;
		private Texture _obstacleTexture1;
		private Texture _obstacleTexture2;
		private Texture _obstacleTexture3;
		private Texture _obstacleTexture4;

		private Room _currentRoom = null;
		private Room _previousCurrentRoom = null;

		private int _blinkCounter = 0;

        public bool LevelEditorMode { get; set; }

        public GameGraphics( bool levelEditorMode = false) : this(null)
		{
            LevelEditorMode = levelEditorMode;
        }

		public GameGraphics(Level level, bool levelEditorMode = false)
        {

            LevelEditorMode = levelEditorMode;

            Textures.Instance.LoadTextures();

			_playerTexture = Textures.Instance.PlayerTexture;
			_playerHitTexture = Textures.Instance.PlayerHitTexture;
			_floorTexture = Textures.Instance.FloorTexture;
			_exitTexture = Textures.Instance.ExitTexture;
			_obstacleTexture1 = Textures.Instance.ObstacleTexture1;
			_obstacleTexture2 = Textures.Instance.ObstacleTexture2;
			_obstacleTexture3 = Textures.Instance.ObstacleTexture3;
			_obstacleTexture4 = Textures.Instance.ObstacleTexture4;

			Level = level;
			if (level != null) _currentRoom = Level.CurrentRoom;
		}

		private void SetColor(BasicGraphics.Colors color)
		{
			BasicGraphics.SetColor(color, _alpha);
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
				DrawRoom(room, room == Level.CurrentRoom, Level.IsPlayerHit);
			}

			_currentRoom = Level.CurrentRoom;
		}

		public void DrawRoom(Room room, bool isCurrentRoom, bool isPlayerHit = false)
		{
			DrawRoomBounds(room.RoomBounds);
			DrawFloor(room.RoomBounds);

			DrawMirrors(room.Mirrors, room.Player.Position, isCurrentRoom);

			if (isCurrentRoom) DrawPlayer(room.Player, isPlayerHit);

			DrawExit(room.Exit);
			DrawLightRays(room.LightRays);
			DrawObstacles(room.Obstacles, room.Index);
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
			SetColor(BasicGraphics.Colors.LightRay);
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

		private void DrawPlayer(Player player, bool isHit = false)
		{
			Texture playerTexture;

			if (isHit)
			{
				bool useHitTexture = Math.Floor(_blinkCounter / 10f) % 2 == 0;
				playerTexture = useHitTexture ? _playerHitTexture : _playerTexture;

				_blinkCounter++;
			}
			else
			{
				playerTexture = _playerTexture;
				_blinkCounter = 0;
			}

			Vector2 bL = player.GetRelativePoint(-1, -1);
			Vector2 bR = player.GetRelativePoint(1, -1);
			Vector2 tR = player.GetRelativePoint(1, 1);
			Vector2 tL = player.GetRelativePoint(-1, 1);

			DrawTexture(playerTexture, bL, bR, tR, tL);

			//SetColor(BasicGraphics.Colors.LightRay);
			//DrawBounds(player, 0.002f);
		}

		private void DrawMirrors(List<Mirror> mirrors)
		{
			DrawMirrors(mirrors, Vector2.Zero, false);
		}

		private void DrawMirrors(List<Mirror> mirrors, Vector2 playerPosition, bool isCurrentRoom)
		{
			foreach (Mirror mirror in mirrors) DrawMirror(mirror, playerPosition, isCurrentRoom);
		}

		private void DrawMirror(Mirror mirror)
		{
			DrawMirror(mirror, Vector2.Zero, false);
		}

		private void DrawMirror(Mirror mirror, Vector2 playerPosition, bool isCurrentRoom)
		{
			SetColor(mirror.IsAccessible ? BasicGraphics.Colors.MirrorRailActive : BasicGraphics.Colors.MirrorRailInactive);
			DrawLine(new Line(mirror.RailPosition1, mirror.RailPosition2), 0.01f);

			Vector2 railVector = mirror.RailPosition2 - mirror.RailPosition1;
			railVector.Normalize();

			Vector2 point1 = mirror.RailPosition1 + railVector * 0.02f;
			Vector2 point2 = mirror.RailPosition2 + railVector * -0.02f;

			SetColor(BasicGraphics.Colors.InnerMirrorRail);
			DrawLine(new Line(point1, point2), 0.005f);

			SetColor(mirror.IsAccessible ? BasicGraphics.Colors.MirrorActive : BasicGraphics.Colors.MirrorInactive);
			DrawLine(mirror.GetMirrorLine(), 0.008f);

			// HUD

			if (!isCurrentRoom) return;

			Vector2 mirrorPosition = mirror.GetMirrorPosition();
            float alpha = 0.0f;
            if (!LevelEditorMode) {
                 alpha = (0.36f - (playerPosition - mirrorPosition).Length) * 2.8f;
                alpha = Math.Min(0.75f, alpha);
            }
            else
                alpha = 1.0f;

            if (alpha <= 0) return;

			float rotation = mirror.MinimumRotation;
			float rotationIncrease = (mirror.MaximumRotation - mirror.MinimumRotation) / 20.0f;

			GL.Color4(1.0f, 1.0f, 1.0f, alpha);

			Vector2 lastHudVector1 = new Vector2(-10, 1);
			Vector2 lastHudVector2 = new Vector2(-10, 1);

			while (rotation <= mirror.MaximumRotation)
			{
				Vector2 mirrorHalf = (mirror.GetMirrorHalf(rotation) * 0.14f);

				Vector2 hudVector1 = mirrorPosition - mirrorHalf;
				Vector2 hudVector2 = mirrorPosition + mirrorHalf;

				if (lastHudVector1.X > -2) DrawLine(new Line(hudVector1, lastHudVector1), 0.003f);
				if (lastHudVector2.X > -2) DrawLine(new Line(hudVector2, lastHudVector2), 0.003f);

				lastHudVector1 = hudVector1;
				lastHudVector2 = hudVector2;

				rotation += rotationIncrease;
			}
		}

		private void DrawObstacles(List<Obstacle> obstacles, int roomIndex = 0)
		{
			for (int i = 0; i < obstacles.Count; i++)
			{
				DrawObstacle(obstacles[i], (i + roomIndex) % 4);
			}
		}

		private Texture GetObstacleTexture(int index)
		{
			if (index == 0) return _obstacleTexture1;
			if (index == 1) return _obstacleTexture2;
			if (index == 2) return _obstacleTexture3;

			return _obstacleTexture4;
		}

		private void DrawObstacle(Obstacle obstacle, int textureIndex = 0)
		{
			Texture texture = GetObstacleTexture(textureIndex);

			DrawRectangularTexture(texture, obstacle.TopLeft, obstacle.BottomRight);
		}

		private void DrawExit(RectangularGameObject exit)
		{
			Vector2 topLeft = exit.TopLeft;
			Vector2 bottomRight = exit.BottomRight;
			Vector2 bottomLeft = new Vector2(topLeft.X, bottomRight.Y);
			Vector2 topRight = new Vector2(bottomRight.X, topLeft.Y);

			Vector2 exitVector = bottomRight - topLeft;

			bool isUpright = Math.Abs(exitVector.X) < Math.Abs(exitVector.Y);

			Vector2 textureTopLeft = isUpright ? bottomLeft : topLeft;
			Vector2 textureBottomRight = isUpright ? topRight : bottomRight;

			if (isUpright) DrawTexture(_exitTexture, bottomRight, topRight, topLeft, bottomLeft);
			else DrawRectangularTexture(_exitTexture, textureTopLeft, textureBottomRight);
		}

		// --- //

		private void DrawRectangularGameObject(RectangularGameObject rectangularGameObject)
		{
			Vector2 topLeft = GetTransformedVector(rectangularGameObject.TopLeft);
			Vector2 bottomRight = GetTransformedVector(rectangularGameObject.BottomRight);

			BasicGraphics.DrawSquare(topLeft, bottomRight);
		}

		private void DrawBounds(IBounded bounds, float thickness)
		{
			foreach (Line v in bounds.GetLines()) DrawLine(v, thickness);
		}

		private void DrawLine(Line line, float thickness)
		{
			BasicGraphics.DrawLine(GetTransformedVector(line.Point1), GetTransformedVector(line.Point2), thickness * _scale);
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

			BasicGraphics.DrawTexture(texture, bL, bR, tR, tL, _alpha);
		}

		private Vector2 GetTransformedVector(Vector2 vector)
		{
			return new Vector2(vector.X * _scale + _center.X, vector.Y * _scale + _center.Y);
		}

	}
}
