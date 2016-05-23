using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Model
{
    public static class Serializer
    {
        public static bool saveLevel(Level level, String filePath, String fileName)
        {

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            XmlSerializer x = new XmlSerializer(typeof(Level));
            TextWriter writer = new StreamWriter(filePath + fileName);
            x.Serialize(writer, level);

            return true;
        }
        public static Level loadLevel( String filePath, String fileName)
        {
            Level level = new Level();
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            XmlSerializer x = new XmlSerializer(typeof(Level));
            TextReader reader = new StreamReader(filePath + fileName);
            level = (Level)x.Deserialize(reader);

            return level;
        }
    }
}
