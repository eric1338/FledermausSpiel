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

				TryToMovePlayer(level, dx, dy);
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

			Room room = level.Room;

			bool movePlayer = true;

			// anders (am linken Rand blockiert W+A)
			if (room.LeftX > newPosition.X || room.RightX < newPosition.X ||
				room.BottomY > newPosition.Y || room.TopY < newPosition.Y) movePlayer = false;

			// CheckBounds

			if (movePlayer) level.Player.Position = newPosition;

			// evtl Move
			// CheckRay
		}

		public void DoLogic(Level level)
		{
			DetermineMirrorAccessibility(level);
			CalculateLightRay();
			CheckExit();
		}

		private void DetermineMirrorAccessibility(Level level)
		{
			foreach (Mirror m in level.Mirrors)
			{
				m.DetermineAccessiblity(level.Player.Position);
			}
		}

		private void CalculateLightRay()
		{

		}

		private void CheckExit()
		{
			// Exit.IsOpen = SolarPanel.HitByRay()
		}

	}
}
