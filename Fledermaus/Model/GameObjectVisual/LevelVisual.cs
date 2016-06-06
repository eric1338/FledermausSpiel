using System;
using System.Collections.Generic;
using System.Text;

namespace Model.GameObjectVisual
{
    public class LevelVisual
    {
        private string name;
        private List<RoomVisual> rooms;



        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public List<RoomVisual> Rooms
        {
            get
            {
                return rooms;
            }

            set
            {
                rooms = value;
            }
        }


        public LevelVisual()
        {
            rooms = new List<RoomVisual>();
        }
    }
}
