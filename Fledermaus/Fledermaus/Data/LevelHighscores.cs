using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fledermaus.Data
{
	public class LevelHighscores
	{

		public string LevelName { get; set; }
		public int NumberOfRooms { get; set; }
		
		public float[] SingleTimes;
		public float TotalTime = -1f;

		public LevelHighscores()
		{
			Init("", 0);
		}

		public LevelHighscores(string levelName, int numberOfRooms)
		{
			Init(levelName, numberOfRooms);
		}

		public void Init(string levelName, int numberOfRooms)
		{
			LevelName = levelName;
			NumberOfRooms = numberOfRooms;

			SingleTimes = new float[numberOfRooms];

			for (int i = 0; i < NumberOfRooms; i++) SingleTimes[i] = -1f;
		}

		public float GetTime(int roomNumber)
		{
			return SingleTimes[roomNumber];
		}

		public bool CheckSingleTime(int roomNumber, float newTime)
		{
			if (SingleTimes[roomNumber] < 0 || SingleTimes[roomNumber] > newTime)
			{
				SingleTimes[roomNumber] = newTime;
				return true;
			}

			return false;
		}

		public bool CheckTotalTime(float newTime)
		{
			if (TotalTime < 0 || TotalTime > newTime)
			{
				TotalTime = newTime;
				return true;
			}

			return false;
		}

	}
}
