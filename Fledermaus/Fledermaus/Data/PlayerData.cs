using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.Data
{
	public class PlayerData
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
			if (!_allLevelHighscores.ContainsKey(levelName))
			{
				// TODO: anpassen

				LevelHighscores highscores = new LevelHighscores(5);
				_allLevelHighscores.Add(levelName, highscores);

				return highscores;
			}

			return _allLevelHighscores[levelName];
		}

		private static string GetPlayerDataFileDirectory()
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\FledermausSpiel";
		}

		private static string GetPlayerDataFilePath()
		{
			return GetPlayerDataFileDirectory() + "\\PlayerData.xml";
		}

		public static void WriteXML()
		{
			PlayerData overview = Instance;

			System.Xml.Serialization.XmlSerializer writer =
				new System.Xml.Serialization.XmlSerializer(typeof(PlayerData));

			// catch Exception

			string directory = GetPlayerDataFileDirectory();

			if (!Directory.Exists(directory))
			{
				Console.WriteLine("didnt exist");
				Directory.CreateDirectory(directory);
			}

			string filePath = GetPlayerDataFilePath();

			Console.WriteLine("path: " + filePath);

			System.IO.FileStream file;

			try
			{
				file = System.IO.File.Create(filePath);
				writer.Serialize(file, overview);
				file.Close();
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}

		}

	}
}
