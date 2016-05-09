using Fledermaus.GameObjects;
using Fledermaus.Utils;
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

		public Level Level { get; set; }
		public InputManager InputManager { get; set; }

		private bool _godMode = false;

		private Level GetLevel() { return Level; }
		private Player GetPlayer() { return Level.Player; }
		private LightRay GetLightRay() { return Level.LightRay; }
		private List<Mirror> GetMirrors() { return Level.Mirrors; }
		private Exit GetExit() { return Level.Exit; }
		private SolarPanel GetSolarPanel() { return Level.SolarPanel; }

		public void ProcessInput()
		{
			ProcessProlongedUserActions();
			ProcessSingleUserActions();
		}

		private void ProcessProlongedUserActions()
		{
			if (GetPlayer().IsFocusedToMirror())
			{
				Mirror currentMirror = GetPlayer().CurrentMirror;

				if (InputManager.IsUserActionActive(UserAction.MoveLeft))
				{
					currentMirror.MoveMirrorUp();
					GetPlayer().Position = currentMirror.GetRelativePlayerPosition();
				}
				if (InputManager.IsUserActionActive(UserAction.MoveRight))
				{
					currentMirror.MoveMirrorDown();
					GetPlayer().Position = currentMirror.GetRelativePlayerPosition();
				}

				if (InputManager.IsUserActionActive(UserAction.RotateMirrorCW)) currentMirror.RotateMirrorCW();
				if (InputManager.IsUserActionActive(UserAction.RotateMirrorCCW)) currentMirror.RotateMirrorCCW();
			}
			else
			{
				float dx = 0.0f;
				float dy = 0.0f;

				if (InputManager.IsUserActionActive(UserAction.MoveUp)) dy = 0.01f;
				if (InputManager.IsUserActionActive(UserAction.MoveDown)) dy = -0.01f;
				if (InputManager.IsUserActionActive(UserAction.MoveLeft)) dx = -0.01f;
				if (InputManager.IsUserActionActive(UserAction.MoveRight)) dx = 0.01f;

				TryToMovePlayer(dx, 0.0f);
				TryToMovePlayer(0.0f, dy);
			}
		}

		private void ProcessSingleUserActions()
		{
			foreach (UserAction userAction in InputManager.GetSingleUserActionsAsList())
			{
				if (userAction == UserAction.ToggleMirrorLock)
				{
					ToggleMirrorLock();
				}
				else if (userAction == UserAction.ResetLevel)
				{
					GetLevel().Reset();
				}
				else if (userAction == UserAction.ToggleGodMode)
				{
					_godMode = !_godMode;
				}
			}
		}

		private void TryToMovePlayer(float dx, float dy)
		{
			Vector2 deltaVector = new Vector2(dx, dy);
			Player tempPlayer = GetPlayer().CreateClone();
			bool movePlayer = true;

			tempPlayer.Move(deltaVector);

			if (tempPlayer.HasIntersection(GetLevel().GetNonReflectingGameObjects())) movePlayer = false;

			if (movePlayer) GetPlayer().Move(deltaVector);
		}

		private void ToggleMirrorLock()
		{
			if (GetPlayer().IsFocusedToMirror())
			{
				GetPlayer().UnfocusCurrentMirror();
			}
			else
			{
				foreach (Mirror mirror in GetMirrors())
				{
					if (mirror.IsAccessible)
					{
						GetPlayer().FocusMirror(mirror);
						GetPlayer().Position = mirror.GetRelativePlayerPosition();
					}
				}
			}
		}


		public void DoLogic()
		{
			GetLightRay().ResetRays();
			CalculateLightRay();

			CheckPlayerLightRayCollision();
            CheckSolarPanel();
			CheckExit();
			DetermineMirrorAccessibility();
		}

		// Temp

		private class TGO : GameObject
		{

			Line Line;

			public TGO(Line line) : base(new Vector2(0.0f, 0.0f))
			{
				Line = line;
			}

			public override List<Line> GetLines()
			{
				return new List<Line> { Line };
			}
		}

		private void CalculateLightRay()
		{
			Line currentRay = GetLightRay().GetLastRay();

			GameObject tgo = new TGO(currentRay);

			Intersection closestNonReflectingIntersection = tgo.GetClosestIntersection(GetLevel().GetNonReflectingGameObjects());
			Intersection closestReflectingIntersection = tgo.GetClosestIntersection(GetLevel().GetReflectingGameObejcts());

			bool reflect = false;

			if (closestReflectingIntersection != null)
			{
				reflect = closestReflectingIntersection.RelativeDistance < closestNonReflectingIntersection.RelativeDistance;
			}

			if (reflect)
			{
				Mirror mirror = closestReflectingIntersection.GameObject as Mirror;

				if (mirror == null) return;

				Vector3 lightDirection3D = new Vector3(currentRay.GetDirectionVector().X, currentRay.GetDirectionVector().Y, 0.0f);

				Vector2 mirrorDirection2D = mirror.GetLines()[0].GetDirectionVector();
				Vector3 mirrorDirection3D = new Vector3(mirrorDirection2D.X, mirrorDirection2D.Y, 0.0f);

				//lightDirection3D.Normalize();
				//mirrorDirection3D.Normalize();

				float angle = Vector3.CalculateAngle(lightDirection3D, mirrorDirection3D);
				angle = (angle - 1.6f) * 2;

				int f = (mirrorDirection2D.X < 0.0f) ? -1 : 1;

				Vector2 direction = Util.GetRotatedVector(currentRay.GetDirectionVector() * f, angle);

				Vector2 point = new Vector2(closestReflectingIntersection.Point.X, closestReflectingIntersection.Point.Y) + direction * 0.0001f;

				GetLightRay().AddNewRay(point, direction);

				CalculateLightRay();
			}
			else
			{
				GetLightRay().FinishRays(closestNonReflectingIntersection.Point);
			}
		}

		private void CheckPlayerLightRayCollision()
		{
			if (GetPlayer().HasIntersection(GetLightRay()) && !_godMode)
			{
				Console.WriteLine("verloren :(");
				GetLevel().Reset();
			}
		}

        private void CheckSolarPanel()
        {
			GetExit().IsOpen = GetSolarPanel().HasIntersection(GetLightRay());
        }

        private void CheckExit()
        {
			if (GetExit().IsOpen && GetPlayer().HasIntersection(GetExit()))
			{
				Console.WriteLine("gewonnen :)");
				GetLevel().Reset();
			}
		}

		private void DetermineMirrorAccessibility()
		{
			foreach (Mirror mirror in GetMirrors())
			{
				mirror.DetermineAccessiblity(GetPlayer().Position);
			}
		}

	}
}
