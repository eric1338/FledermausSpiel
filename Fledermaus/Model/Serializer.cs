using Model.GameObject;
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
        public static Level getLevelByFile( String filePath, String fileName)
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

        public static Level getLevelVisualAsLevel(GameObjectVisual.LevelVisual levelVisual)
        {
            var level = new Level();
            for(int i=0;i<levelVisual.Rooms.Count;i++)
            {
                var roomV = levelVisual.Rooms[i];
                Room room = roomV.Data as Room;
                room.Index = i;
                room.Player = roomV.PlayerVisual.Data as Player;
                room.Exit = roomV.ExitVisual.Data as Exit;


                foreach (var lightrayV in roomV.LightRayVisuals)
                    room.LightRays.Add(lightrayV.Data as LightRay);
                foreach (var obstacleV in roomV.ObstacleVisuals)
                    room.Obstacles.Add(obstacleV.Data as Obstacle);
                foreach (var mirrorV in roomV.MirrorVisuals)
                    room.Mirrors.Add(mirrorV.Data as Mirror);

                level.Rooms.Add(room);
            }

            return level;
        }
        public static Fledermaus.GameObjects.Level getLevelAs_Level(Level level)
        {
            var _level = new Fledermaus.GameObjects.Level(level.Name);
           
            for(int i =0;i<level.Rooms.Count;i++){
                var room = level.Rooms[i];
                var _room = new Fledermaus.Room() {
                    Index = i,
                    NextRoomIndex = i + 1,
                    Column = room.Column,
                    Row = room.Row,
                    RoomBounds = new Fledermaus.GameObjects.RectangularGameObject(room.RelativeBounds[0], room.RelativeBounds[2]),
                    Player = new Fledermaus.GameObjects.Player(room.Player.Position),
                    Exit = new Fledermaus.GameObjects.RectangularGameObject(room.Exit.Position+room.Exit.RelativeBounds[0], room.Exit.Position + room.Exit.RelativeBounds[2]),
                     
                    
                };
                if (i == level.Rooms.Count - 1)
                    _room.NextRoomIndex = -1;
                foreach(var lightRay in room.LightRays)
                {
                    _room.LightRays.Add(new Fledermaus.GameObjects.LightRay(lightRay.Position, lightRay.RayDirection));
                }


                foreach (var obstacle in room.Obstacles)
                {
                    _room.Obstacles.Add(new Fledermaus.GameObjects.Obstacle(obstacle.Position + obstacle.RelativeBounds[0], obstacle.Position+obstacle.RelativeBounds[2]));
                }

                _level.AddRoom(_room);
            }
            _level.CurrentRoom = _level.Rooms[0];
            return _level;
        }
    }
}
