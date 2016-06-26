using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.Data
{
	class LevelHighscores
	{

		public int NumberOfRooms { get; set; }

		private float[] _singleTimes;
		private float _totalTime = -1f;

		public LevelHighscores(int numberOfRooms)
		{
			NumberOfRooms = numberOfRooms;

			_singleTimes = new float[numberOfRooms];

			for (int i = 0; i < NumberOfRooms; i++) _singleTimes[i] = -1f;
		}

		public bool CheckSingleTime(int roomNumber, float newTime)
		{
            if(_singleTimes.Count() > 0)
			if (_singleTimes[roomNumber] < 0 || _singleTimes[roomNumber] > newTime)
			{
				_singleTimes[roomNumber] = newTime;
				return true;
			}

			return false;
		}

		public bool CheckTotalTime(float newTime)
		{
			if (_totalTime < 0 || _totalTime > newTime)
			{
				_totalTime = newTime;
				return true;
			}

			return false;
		}

		public float GetTime(int roomNumber)
		{
			return _singleTimes[roomNumber];
		}

		public float GetTotalTime()
		{
			return _totalTime;
		}

	}
}
