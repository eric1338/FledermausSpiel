using Fledermaus.GameObjects;
using Fledermaus.Screens;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fledermaus.Screens
{
    class LevelEditorScreen : MenuScreen
    {
        private Level level;
        public Level Level
        {
            set { level = value; }
            get { return level; }
        }
        public LevelEditorScreen(MyGameWindow win) : base(win)
        {
            createTestLevel();
            var filename = "Level1.xml";
            saveLevel(Directory.GetCurrentDirectory()+"\\Levels\\",filename);

        }
        private void createTestLevel()
        {
            Level = Levels.CreateTestLevel();
        }
        private bool saveLevel(String filePath, String fileName) {

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            XmlSerializer x = new XmlSerializer(typeof(Room));
            TextWriter writer = new StreamWriter(filePath+fileName);
            x.Serialize(writer, level);

            return true;
        }
    }
}
