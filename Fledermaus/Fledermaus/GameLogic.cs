using Fledermaus.GameObjects;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus
{
	class GameLogic
	{

		private static GameLogic instance = new GameLogic();

		public static GameLogic GetInstance()
		{
			return instance;
		}

		public void MakeActions(Inputs inputs, Level level)
		{
			// TODO: anders

			Player player = level.Player;
			Mirror currentMirror = player.CurrentMirror;

			bool mirrorMovement = currentMirror != null;

			if (mirrorMovement)
			{
				if (inputs.UserActionStatus[UserAction.MoveLeft])
				{
					currentMirror.MoveMirrorUp();
					player.Position = currentMirror.GetRelativePlayerPosition();
				}
				if (inputs.UserActionStatus[UserAction.MoveRight])
				{
					currentMirror.MoveMirrorDown();
					player.Position = currentMirror.GetRelativePlayerPosition();
				}

				if (inputs.UserActionStatus[UserAction.RotateMirrorCW]) currentMirror.RotateMirrorCW();
				if (inputs.UserActionStatus[UserAction.RotateMirrorCCW]) currentMirror.RotateMirrorCCW();
			}
			else
			{
				float dx = 0.0f;
				float dy = 0.0f;

				if (inputs.UserActionStatus[UserAction.MoveUp]) dy = 0.01f;
				if (inputs.UserActionStatus[UserAction.MoveDown]) dy = -0.01f;
				if (inputs.UserActionStatus[UserAction.MoveLeft]) dx = -0.01f;
				if (inputs.UserActionStatus[UserAction.MoveRight]) dx = 0.01f;

				TryToMovePlayer(level, dx, 0.0f);
				TryToMovePlayer(level, 0.0f, dy);
			}

			int n = inputs.SingleUserActions.Count;
			
			for (int i = 0; i < n; i++)
			{
				UserAction nextUserAction = inputs.SingleUserActions.Dequeue();

				if (nextUserAction == UserAction.ToggleMirrorLock)
				{
					ToggleMirrorLock(level);

				}
				else if (nextUserAction == UserAction.ResetLevel)
				{
					level.Reset();
				}
			}
		}

		private void ToggleMirrorLock(Level level)
		{
			Player player = level.Player;

			if (player.CurrentMirror != null)
			{
				player.CurrentMirror = null;
			}
			else
			{
				foreach (Mirror m in level.Mirrors)
				{
					if (m.IsAccessible)
					{
						player.CurrentMirror = m;
						player.Position = m.GetRelativePlayerPosition();
					}
				}
			}
		}

		private void TryToMovePlayer(Level level, float dx, float dy)
		{
			Vector2 newPosition = level.Player.Position + new Vector2(dx, dy);

			RectangularGameObject room = level.Room;

			bool movePlayer = true;
			
			if (room.Point1.X > newPosition.X || room.Point2.X < newPosition.X ||
				room.Point4.Y > newPosition.Y || room.Point2.Y < newPosition.Y) movePlayer = false;

			// CheckBounds

			if (movePlayer) level.Player.Position = newPosition;
		}

		public void DoLogic(Level level)
		{
			DetermineMirrorAccessibility(level);
			CalculateLightRay(level);
			CheckExit();
		}

		private void DetermineMirrorAccessibility(Level level)
		{
			foreach (Mirror m in level.Mirrors)
			{
				m.DetermineAccessiblity(level.Player.Position);
			}
		}

		private void CalculateLightRay(Level level)
		{
            List<Line> lines = level.Room.GetLines();

            LightRay ray = level.LightRay;

			List<Vector2> intersections = GetIntersections(ray.GetLastRay(), level.Room);

			if (intersections.Count == 1)
			{
				ray.EndVector = intersections[0];
			}
        }

		private bool HasIntersection(Line line, List<Line> lines)
		{
			List<Vector2> intersections = new List<Vector2>();

			foreach (Line otherLine in lines)
			{
				Vector2? intersection = GetIntersection(line, otherLine);

				if (intersection != null) return true;
			}

			return false;
		}

		public List<Vector2> GetIntersections(Line line, GameObject gameObject)
		{
			return GetIntersections(line, gameObject.GetLines());
		}

		public List<Vector2> GetIntersections(Line line, List<Line> lines)
		{
			List<Vector2> intersections = new List<Vector2>();

			foreach (Line otherLine in lines)
			{
				Vector2? intersection = GetIntersection(line, otherLine);

				if (intersection != null) intersections.Add((Vector2) intersection);
			}

			return intersections;
		}

        private Vector2? GetIntersection(Line v1, Line v2)
        {
			Vector2 p1o = v1.GetOriginVector();
			Vector2 p1d = v1.GetDirectionVector();
			Vector2 p2o = v2.GetOriginVector();
			Vector2 p2d = v2.GetDirectionVector();

			// TODO: Parallelität

			float x1 = p1o.X;
            float y1 = p1o.Y;
            float x2 = p1d.X;
            float y2 = p1d.Y;
            float x3 = p2o.X;
            float y3 = p2o.Y;
            float x4 = p2d.X;
            float y4 = p2d.Y;
			
            float t = (-x1 * y4 + x3 * y4 + x4 * y1 - x4 * y3) / (x2 * y4 - x4 * y2);
            float u = (x1 + t * x2 - x3) / x4;

            if (t >= 0 && t <= 1 && u >= 0 && u <= 1)
            {
                return p1o + p1d * t;
            }

			return null;
        }

		private void CheckExit()
		{
			// Exit.IsOpen = SolarPanel.HitByRay()
		}

	}
}
