using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	interface ILogicalLevel
	{

		bool IsPlayerHit { get; set; }

		ILogicalRoom GetCurrentRoom();
		void SwitchCurrentRoom(int newRoomIndex);

		void PauseTimer();
		void UnpauseTimer();

		void FinishLevel();

	}
}
