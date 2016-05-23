using Fledermaus.GameObjects;
using Fledermaus.Screens;
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

		public bool MovementInputBlocked { get; set; }
		public bool GamePaused { get; set; }

		private bool _godMode = false;

		public ILogicalLevel Level;

		private ILogicalRoom CurrentRoom
		{
			get { return Level.GetCurrentRoom(); }
		}

		private ILogicalPlayer Player
		{
			get { return CurrentRoom.GetLogicalPlayer(); }
		}

		private IEnumerable<ILogicalLightRay> LightRays
		{
			get { return CurrentRoom.GetLogicalLightRays(); }
		}

		private IEnumerable<ILogicalMirror> Mirrors
		{
			get { return CurrentRoom.GetLogicalMirrors(); }
		}


		private const float _playerMovementSpeed = 0.01f;
		private const float _mirrorMovementSpeed = 0.02f;
		private const float _rotationSpeed = 0.006f;
		private const float _minMirrorAccessibilityDistance = 0.14f;

		public InputManager InputManager { get; set; }

		public GameScreen GameScreen { get; set; }

		public void ProcessInput()
		{
			ProcessProlongedUserActions();
			ProcessSingleUserActions();
		}

		private void ProcessProlongedUserActions()
		{
			if (MovementInputBlocked) return;

			if (Player.IsFocusedToMirror())
			{
				ILogicalMirror currentMirror = Player.CurrentMirror;

				bool moveLeft = InputManager.IsUserActionActive(UserAction.MoveLeft);
				bool moveRight = InputManager.IsUserActionActive(UserAction.MoveRight);

				if (moveLeft) currentMirror.MoveMirrorUp(_mirrorMovementSpeed);
				if (moveRight) currentMirror.MoveMirrorDown(_mirrorMovementSpeed);

				if (moveLeft || moveRight)
				{
					Player.Position = currentMirror.GetMirrorPosition() + Player.VectorToMirror;
				}

				if (InputManager.IsUserActionActive(UserAction.RotateMirrorCW)) currentMirror.RotateCW(_rotationSpeed);
				if (InputManager.IsUserActionActive(UserAction.RotateMirrorCCW)) currentMirror.RotateCCW(_rotationSpeed);
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

		private void TryToMovePlayer(float dx, float dy)
		{
			Vector2 deltaVector = new Vector2(dx, dy);
			ILogicalPlayer tempPlayer = Player.CreateClone();
			bool movePlayer = true;

			tempPlayer.Position += deltaVector;

			if (Util.HasIntersection(tempPlayer, CurrentRoom.GetNonReflectingLines())) movePlayer = false;

			if (movePlayer) Player.Position += deltaVector;
		}

		// TODO: UserAction und Function evtl mappen

		private void ProcessSingleUserActions()
		{
			foreach (UserAction userAction in InputManager.GetSingleUserActionsAsList())
			{
				if (userAction == UserAction.ToggleMirrorLock && !MovementInputBlocked)
				{
					ToggleMirrorLock();
				}
				else if (userAction == UserAction.ResetLevel)
				{
					CurrentRoom.Reset();
				}
				else if (userAction == UserAction.ToggleGodMode)
				{
					_godMode = !_godMode;
				}
				else if (userAction == UserAction.TogglePauseGame)
				{
					TogglePauseGame();
				}
				else if (userAction == UserAction.OpenGameMenu)
				{
					GameScreen.OpenGameMenuScreen();
				}
			}
		}

		private void ToggleMirrorLock()
		{
			if (Player.IsFocusedToMirror())
			{
				Player.UnfocusCurrentMirror();
			}
			else
			{
				foreach (ILogicalMirror mirror in Mirrors)
				{
					if (mirror.IsAccessible)
					{
						Player.CurrentMirror = mirror;
						Player.VectorToMirror = Player.Position - mirror.GetMirrorPosition();
					}
				}
			}
		}

		private void TogglePauseGame()
		{
			if (GamePaused) UnpauseGame();
			else PauseGame();
		}

		// TODO: GameGraphics iwie über Änderung informieren, damit alpha auf 0.15 gesetzt werden kann
		// bzw. das Menüoverlay gezeigt werden kann

		public void PauseGame()
		{
			GamePaused = true;
			MovementInputBlocked = true;
		}

		public void UnpauseGame()
		{
			GamePaused = false;
			MovementInputBlocked = false;
		}

		public void DoLogic()
		{
			if (GamePaused) return;

			ResetLighRays();
			CalculateLightRays();

			MoveNPCs();

			CheckRoomTransitionTriggers();

			CheckPlayerLightCollision();
            CheckSolarPanel();
			CheckExit();
			DetermineMirrorAccessibility();
		}

		private void ResetLighRays()
		{
			foreach (ILogicalLightRay lightRay in LightRays) lightRay.ResetRays();
		}

		private void CalculateLightRays()
		{
			foreach (ILogicalLightRay lightRay in LightRays) CalculateLightRay(lightRay);
		}

		private void CalculateLightRay(ILogicalLightRay lightRay)
		{
			Line currentRay = lightRay.GetLastRay();

			Intersection closestNonReflectingIntersection = Util.GetClosestIntersection(currentRay, CurrentRoom.GetNonReflectingLines());

			var mirrorIntersections = new List<Tuple<Intersection, ILogicalMirror>>();

			foreach (ILogicalMirror mirror in Mirrors)
			{
				Intersection mirrorIntersection = Util.GetIntersection(currentRay, mirror.GetMirrorLine());

				if (mirrorIntersection != null)
				{
					mirrorIntersections.Add(new Tuple<Intersection, ILogicalMirror>(mirrorIntersection, mirror));
				}
			}

			bool reflect = false;

			Tuple<Intersection, ILogicalMirror> closestReflectingIntersection = null;

			if (mirrorIntersections.Count > 0)
			{
				closestReflectingIntersection = mirrorIntersections[0];

				foreach (var mirrorIntersection in mirrorIntersections)
				{

					if (mirrorIntersection.Item1.RelativeDistance < closestReflectingIntersection.Item1.RelativeDistance)
					{
						closestReflectingIntersection = mirrorIntersection;
					}

				}

				reflect = closestReflectingIntersection.Item1.RelativeDistance < closestNonReflectingIntersection.RelativeDistance;
			}

			if (reflect)
			{
				Vector2 intersectionPoint = closestReflectingIntersection.Item1.Point;
				ILogicalMirror mirror = closestReflectingIntersection.Item2;

				Vector2 lightDirection = currentRay.GetDirectionVector();
				lightDirection.Normalize();

				Vector2 normal = mirror.GetMirrorNormal1();

				//Vector2 normal2 = mirror.GetMirrorNormal2();
				//float angle1 = Util.CalculateAngle(normal1, lightDirection);
				//float angle2 = Util.CalculateAngle(normal2, lightDirection);
				//Vector2 normal = angle1 < angle2 ? normal1 : normal2;

				Vector2 newDirection = lightDirection - 2 * (Vector2.Dot(normal, lightDirection)) * normal;
				Vector2 point = new Vector2(intersectionPoint.X, intersectionPoint.Y) + newDirection * 0.0001f;

				lightRay.AddNewRay(point, newDirection);

				CalculateLightRay(lightRay);
			}
			else
			{
				lightRay.FinishRays(closestNonReflectingIntersection.Point);
			}
		}

		private void MoveNPCs()
		{
			foreach (ILogicalNPC npc in CurrentRoom.GetLogicalNPCs())
			{
				npc.PointOfInterest = Player.Position;
				npc.TickAndMove();
			}
		}

		private void CheckRoomTransitionTriggers()
		{
			foreach (Tuple<IBounded, int> roomTransitionTrigger in CurrentRoom.GetRoomTransitionTriggers())
			{
				if (Util.HasIntersection(Player, roomTransitionTrigger.Item1))
				{
					Level.SwitchCurrentRoom(roomTransitionTrigger.Item2);
				}
			}
		}

		private void CheckPlayerLightCollision()
		{
			if (_godMode) return;

			if (Util.HasIntersection(Player, CurrentRoom.GetLightLines()))
			{
				Console.WriteLine("verloren :(");
				CurrentRoom.Reset();
			}
		}

        private void CheckSolarPanel()
        {
			List<Line> lightRayLines = new List<Line>();

			foreach (var lightRay in LightRays) lightRayLines.AddRange(lightRay.GetLines());

			IBounded bounds = Util.CreateBoundsFromList(lightRayLines);

			CurrentRoom.IsExitOpen = Util.HasIntersection(CurrentRoom.GetSolarPanelLines(), bounds);
        }

        private void CheckExit()
        {
			if (CurrentRoom.IsExitOpen && Util.HasIntersection(Player, CurrentRoom.GetExitLines()))
			{
				Console.WriteLine("gewonnen :)");
				CurrentRoom.Reset();
			}
		}

		private void DetermineMirrorAccessibility()
		{
			foreach (ILogicalMirror mirror in Mirrors)
			{
				float distance = (mirror.GetMirrorPosition() - Player.Position).Length;
				mirror.IsAccessible = distance < _minMirrorAccessibilityDistance;
			}
		}
	}
}
