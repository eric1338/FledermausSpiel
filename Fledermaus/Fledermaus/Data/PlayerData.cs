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

		private Dictionary<string, LevelHighscores> _allLevelHighscores = new Dictionary<string, LevelHighscores>();


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
