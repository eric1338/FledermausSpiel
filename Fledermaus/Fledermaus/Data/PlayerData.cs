using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fledermaus.Data
{
	public class PlayerData
	{

		public static PlayerData Instance = new PlayerData();

		public bool IsLevel2Unlocked { get; set; }
		public bool IsLevel3Unlocked { get; set; }
		
		public List<LevelHighscores> AllLevelHighscores = new List<LevelHighscores>();

		private PlayerData()
		{

		}

		public static void SetToDefault()
		{
			PlayerData defaultPlayerData = new PlayerData();

			defaultPlayerData.CreateLevelHighscores("Level 1", 5);
			defaultPlayerData.CreateLevelHighscores("Level 2", 5);
			defaultPlayerData.CreateLevelHighscores("Level 3", 5);

			Instance = defaultPlayerData;
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
			AllLevelHighscores.Add(new LevelHighscores(levelName, numberOfRooms));
		}

		public List<string> GetLevelNames()
		{
			List<string> levelNames = new List<string>();

			foreach (LevelHighscores highscores in AllLevelHighscores)
			{
				levelNames.Add(highscores.LevelName);
			}

			return levelNames;
		}

		public LevelHighscores GetLevelHighscores(string levelName, int numberOfRooms = 5)
		{
			foreach (LevelHighscores highscores in AllLevelHighscores)
			{
				if (highscores.LevelName == levelName) return highscores;
			}
			
			LevelHighscores newHighscores = new LevelHighscores(levelName, numberOfRooms);
			AllLevelHighscores.Add(newHighscores);

			return newHighscores;
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
			PlayerData playerData = Instance;

			XmlSerializer writer = new XmlSerializer(typeof(PlayerData));

			string directory = GetPlayerDataFileDirectory();

			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}

			string filePath = GetPlayerDataFilePath();

			FileStream file;

			try
			{
				file = File.Create(filePath);
				writer.Serialize(file, playerData);
				file.Close();
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}

		}

		public static void ReadXML()
		{
			XmlSerializer reader = new XmlSerializer(typeof(PlayerData));

			string filePath = GetPlayerDataFilePath();

			if (!File.Exists(filePath))
			{
				SetToDefault();
				return;
			}

			FileStream file = new FileStream(filePath, FileMode.Open);

			try
			{
				PlayerData data = (PlayerData) reader.Deserialize(file);
				file.Close();

				if (data != null) Instance = data;
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}

		}


	}
}
