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

		private bool _godMode = false;

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
				else if (nextUserAction == UserAction.ToggleGodMode)
				{
					_godMode = !_godMode;
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
			test = 0;
			level.LightRay.ResetRays();
			CalculateLightRay(level);

			CheckPlayerLightRayCollision(level);
            CheckSolarPanel(level);
			CheckExit(level);
			DetermineMirrorAccessibility(level);
		}

		private void DetermineMirrorAccessibility(Level level)
		{
			foreach (Mirror m in level.Mirrors)
			{
				m.DetermineAccessiblity(level.Player.Position);
			}
		}

		private int test = 0;

		private void CalculateLightRay(Level level)
		{

            LightRay ray = level.LightRay;
			Console.WriteLine(ray.GetLines().Count);

			Line currentRay = ray.GetLastRay();

			List<Vector2> nonReflectingIntersections = Util.GetIntersections(currentRay, level.GetNonReflectingGameObjects());
			List<Vector2> reflectingIntersections = Util.GetIntersections(currentRay, level.GetReflectingLines());

			Vector2 closestNonReflectingIntersection = GetClosestPoint(currentRay.GetOriginVector(), nonReflectingIntersections);
			Vector2 closestReflectingIntersection = GetClosestPoint(currentRay.GetOriginVector(), reflectingIntersections);

			float d1 = (closestNonReflectingIntersection - currentRay.GetOriginVector()).Length;
			float d2 = (closestReflectingIntersection - currentRay.GetOriginVector()).Length;

			if (reflectingIntersections.Count < 1) d2 = 20.0f;

			//Console.WriteLine(d1 + " - " + d2);

			if (test++ > 10) return;

			if (d1 < d2)
			{
				ray.FinishRays(closestNonReflectingIntersection);
			}
			else
			{
				//Console.WriteLine("ADD NEW");
				Vector2 direction = ray.GetLines().Count < 1 ? new Vector2(-20.5f, 20.5f) : new Vector2(20.5f, 20.5f);

				Vector2 point = new Vector2(closestReflectingIntersection.X - 0.005f, closestReflectingIntersection.Y + 0.005f);

				ray.AddNewRay(point, direction);

				CalculateLightRay(level);
			}
		}

		// TODO: Liste points leer
		private Vector2 GetClosestPoint(Vector2 origin, List<Vector2> points)
		{
			float distance;
			float minDistance = 5.0f;
			Vector2 closestPoint = new Vector2(10.0f, 10.0f);

			foreach (Vector2 point in points)
			{
				distance = (point - origin).Length;

				if (distance < minDistance)
				{
					minDistance = distance;
					closestPoint = point;
				}
			}

			return closestPoint;
		}

		private void CheckPlayerLightRayCollision(Level level)
		{
			if (Util.HasIntersection(level.Player, level.LightRay) && !_godMode)
			{
				Console.WriteLine("verloren :(");
				level.Reset();
			}
		}

        private void CheckSolarPanel(Level level)
        {
			// evtl level.LightRay durch level.LightRay.GetLastRay() ersetzen 
			level.Exit.IsOpen = Util.HasIntersection(level.SolarPanel, level.LightRay);
        }

        private void CheckExit(Level level)
        {
			if (level.Exit.IsOpen && Util.HasIntersection(level.Exit, level.Player))
			{
				Console.WriteLine("gewonnen :)");
				level.Reset();
			}
        }

	}
}
