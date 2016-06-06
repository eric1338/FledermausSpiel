using Model.GameObject;
using System.Collections.Generic;


namespace Model
{
    public class Level
    {
        private string name;
        private List<Room> rooms;



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

        public List<Room> Rooms
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


        public Level()
        {
            rooms = new List<Room>();
        }
    }
}
