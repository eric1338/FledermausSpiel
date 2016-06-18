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

		private List<string> _lockedLevels = new List<string>();
		private Dictionary<string, LevelHighscores> _allLevelHighscores = new Dictionary<string, LevelHighscores>();

		private PlayerData()
		{
			_lockedLevels.Add("Level 2");
			_lockedLevels.Add("Level 3");
		}

		public void UnlockLevel(string levelName)
		{
			_lockedLevels.Remove(levelName);
		}

		public bool IsLevelLocked(string levelName)
		{
			return _lockedLevels.Contains(levelName);
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
