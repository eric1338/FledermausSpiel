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


		private class SmoothSpeedValue
		{

			private float _initialSpeed;
			private float _maximumSpeed;
			private float _speedIncrease;

			private float _speed;

			public SmoothSpeedValue(float initialSpeed, float maximumSpeed)
			{
				_initialSpeed = initialSpeed;
				_maximumSpeed = maximumSpeed;

				_speedIncrease = (_maximumSpeed - _initialSpeed) / 25.0f;
			}

			public float GetSpeed()
			{
				_speed = Math.Min(_speed + _speedIncrease, _maximumSpeed);

				return _speed;
			}

			public void ResetSpeed()
			{
				_speed = _initialSpeed;
			}

		}

		private SmoothSpeedValue _playerMovement = new SmoothSpeedValue(0.004f, 0.01f);
		private SmoothSpeedValue _mirrorMovement = new SmoothSpeedValue(0.004f, 0.01f);
		private SmoothSpeedValue _mirrorRotation = new SmoothSpeedValue(0.004f, 0.008f);

		private const float _minMirrorAccessibilityDistance = 0.15f;

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
				_playerMovement.ResetSpeed();

				ILogicalMirror currentMirror = Player.CurrentMirror;

				// Movement

				float mirrorMovementSpeed = _mirrorMovement.GetSpeed();

				bool moveLeft = InputManager.IsUserActionActive(UserAction.MoveLeft);
				bool moveRight = InputManager.IsUserActionActive(UserAction.MoveRight);
				bool moveUp = InputManager.IsUserActionActive(UserAction.MoveUp);
				bool moveDown = InputManager.IsUserActionActive(UserAction.MoveDown);

				if (moveLeft) currentMirror.MoveMirrorLeft(mirrorMovementSpeed);
				if (moveRight) currentMirror.MoveMirrorRight(mirrorMovementSpeed);
				if (moveUp) currentMirror.MoveMirrorUp(mirrorMovementSpeed);
				if (moveDown) currentMirror.MoveMirrorDown(mirrorMovementSpeed);

				if (moveLeft || moveRight || moveUp || moveDown) Player.Position = currentMirror.GetMirrorPosition() + Player.VectorToMirror;
				else _mirrorMovement.ResetSpeed();

				// Rotation

				float mirrorRotationSpeed = _mirrorRotation.GetSpeed();

				bool rotateCW = InputManager.IsUserActionActive(UserAction.RotateMirrorCW);
				bool rotateCCW = InputManager.IsUserActionActive(UserAction.RotateMirrorCCW);

				if (rotateCW) currentMirror.RotateCW(mirrorRotationSpeed);
				if (rotateCCW) currentMirror.RotateCCW(mirrorRotationSpeed);

				//Player.Rotation = currentMirror.Rotation;
				SetPlayerRotation(currentMirror);

				if (!rotateCW && !rotateCCW) _mirrorRotation.ResetSpeed();
			}
			else
			{
				_mirrorMovement.ResetSpeed();
				_mirrorRotation.ResetSpeed();

				float dx = 0.0f;
				float dy = 0.0f;

				float playerMovementSpeed = _playerMovement.GetSpeed();

				if (InputManager.IsUserActionActive(UserAction.MoveUp)) dy = playerMovementSpeed;
				if (InputManager.IsUserActionActive(UserAction.MoveDown)) dy = -playerMovementSpeed;
				if (InputManager.IsUserActionActive(UserAction.MoveLeft)) dx = -playerMovementSpeed;
				if (InputManager.IsUserActionActive(UserAction.MoveRight)) dx = playerMovementSpeed;

				if (dx == 0.0f && dy == 0.0f) _playerMovement.ResetSpeed();

				TryToMovePlayer(dx, 0.0f);
				TryToMovePlayer(0.0f, dy);
			}
		}

		// WIP
		private void SetPlayerRotation(ILogicalMirror mirror)
		{
			//Console.WriteLine("RP: " + Util.CalculateAngle(mirror.RailPosition1, mirror.RailPosition2) * 57.1f);
			//Console.WriteLine("An: " + mirror.Rotation * 57.1f);

			//float railAngle = Util.CalculateAngle(mirror.RailPosition1, mirror.RailPosition2);

			Vector2 pp = new Vector2(mirror.RailPosition1.X + 1f, mirror.RailPosition1.Y) - mirror.RailPosition1;
			float railAngle = Util.CalculateAngle(mirror.RailPosition2 - mirror.RailPosition1, pp);

			//Console.WriteLine("RA: " + railAngle * 57.1f);

			float rotation = railAngle + mirror.Rotation;

			if (Player.Position.Y > mirror.GetMirrorPosition().Y) rotation += 3.14f;

			//Console.WriteLine("To: " + rotation * 57.1f);
			//Console.WriteLine(" ");

			Player.Rotation = 0.0f;
		}

		private void TryToMovePlayer(float dx, float dy)
		{
			Vector2 deltaVector = new Vector2(dx, dy);
			ILogicalPlayer tempPlayer = Player.CreateClone();
			bool movePlayer = true;

			tempPlayer.Position += deltaVector;

			if (Util.HasIntersection(tempPlayer, CurrentRoom.GetNonReflectingBounds())) movePlayer = false;
			if (Util.HasIntersection(tempPlayer, CurrentRoom.GetReflectingBounds())) movePlayer = false;
			if (Util.HasIntersection(tempPlayer, CurrentRoom.GetLightBounds()) && !_godMode) movePlayer = false;

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
						Player.FocusMirror(mirror);
						SetPlayerRotation(mirror);
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

		public void PauseGame()
		{
			Level.PauseTimer();
			GamePaused = true;
			MovementInputBlocked = true;
		}

		public void UnpauseGame()
		{
			Level.UnpauseTimer();
			GamePaused = false;
			MovementInputBlocked = false;
		}

		public void DoLogic()
		{
			if (GamePaused) return;

			ResetLighRays();
			CalculateLightRays();

			CheckRoomTransitionTriggers();

			CheckPlayerLightCollision();
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

			Intersection closestNonReflectingIntersection = Util.GetClosestIntersection(currentRay, CurrentRoom.GetNonReflectingBounds());

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

		private void CheckRoomTransitionTriggers()
		{
			foreach (Tuple<IBounded, int> roomTransitionTrigger in CurrentRoom.GetRoomTransitionTriggers())
			{
				if (Util.HasIntersection(Player, roomTransitionTrigger.Item1))
				{
					// temp
					if (roomTransitionTrigger.Item2 < 0)
					{
						Level.FinishLevel();
						GameScreen.FinishLevel();
						PauseGame();

						return;
					}

					Level.SwitchCurrentRoom(roomTransitionTrigger.Item2);
				}
			}
		}

		private void CheckPlayerLightCollision()
		{
			if (_godMode) return;

			if (Util.HasIntersection(Player, CurrentRoom.GetLightBounds()))
			{
				Console.WriteLine("verloren :(");
				CurrentRoom.Reset();
			}
		}

        private void CheckExit()
        {
			if (CurrentRoom.IsExitOpen && Util.HasIntersection(Player, CurrentRoom.GetExitBounds()))
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
