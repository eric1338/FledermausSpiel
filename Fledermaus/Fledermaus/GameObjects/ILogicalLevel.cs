﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	interface ILogicalLevel
	{

		ILogicalRoom GetCurrentRoom();
		void SwitchCurrentRoom(int newRoomIndex);

		void StartTimer();
		void PauseTimer();
		void UnpauseTimer();
		void StopTimer();

		void FinishLevel();

	}
}
