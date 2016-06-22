using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.Data
{
	class PlayerData
	{

		public static PlayerData Instance = new PlayerData();

		private bool IsLevel2Unlocked = false;
		private bool IsLevel3Unlocked = false;

		private Dictionary<string, LevelHighscores> _allLevelHighscores = new Dictionary<string, LevelHighscores>();

		private PlayerData()
		{
		}

		public void UnlockLevel(string levelName)
		{
			if (levelName == "Level 2") IsLevel2Unlocked = true;
			if (levelName == "Level 3") IsLevel3Unlocked = true;
		}

		public bool IsLevelLocked(string levelName)
		{
			if (levelName == "Level 2") return !IsLevel2Unlocked;
			if (levelName == "Level 3") return !IsLevel3Unlocked;

			return false;
		}

		public void CreateLevelHighscores(string levelName, int numberOfRooms)
		{
			_allLevelHighscores.Add(levelName, new LevelHighscores(numberOfRooms));
		}

		public LevelHighscores GetLevelHighscores(string levelName)
		{
			return _allLevelHighscores[levelName];
		}

	}
}
