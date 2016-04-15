using Fledermaus.GameObjects;
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

			bool b = level.Player.LockedToMirror;

			if (!b && inputs.UserActionStatus[UserAction.MoveUp]) level.Player.MoveY(0.01f);
			if (!b && inputs.UserActionStatus[UserAction.MoveDown]) level.Player.MoveY(-0.01f);
			if (!b && inputs.UserActionStatus[UserAction.MoveLeft]) level.Player.MoveX(-0.01f);
			if (!b && inputs.UserActionStatus[UserAction.MoveRight]) level.Player.MoveX(0.01f);

			if (b)
			{
				Mirror m = level.Mirrors[0];

				if (inputs.UserActionStatus[UserAction.MoveLeft]) m.MoveMirrorUp();
				if (inputs.UserActionStatus[UserAction.MoveRight]) m.MoveMirrorDown();
			}

			if (!b && inputs.UserActionStatus[UserAction.ToggleMirrorLock])
			{
				level.Player.LockedToMirror = true;

				Mirror m = level.Mirrors[0];

				level.Player.Position = m.GetBatPosition();
			}
		}

		public void DoLogic(Level level)
		{
			CalculateLightRay();
			CheckExit();
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
