using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	class Level : ILogicalLevel
	{

		class RoomTransition
		{
			Room destiny;
			IBounded trigger;
		}


		public Room CurrentRoom;

		public List<Room> Rooms { get; set; }	


		public Level()
		{
			Rooms = new List<Room>();
		}

		private Room GetRoom(int index)
		{
			foreach (Room room in Rooms)
			{
				if (room.Index == index) return room;
			}

			return null;
		}

		public void SwitchCurrentRoom(int newCurrentRoomIndex)
		{
			Room newCurrentRoom = GetRoom(newCurrentRoomIndex);

			if (newCurrentRoom != null)
			{
				CurrentRoom = newCurrentRoom;
				CurrentRoom.Player.Reset();
			}
		}

		public void AddRoom(Room room)
		{
			Rooms.Add(room);
		}

		public List<Room> GetOtherRooms()
		{
			List<Room> otherRooms = new List<Room>(Rooms);

			otherRooms.Remove(CurrentRoom);

			return otherRooms;
		}

		public ILogicalRoom GetCurrentRoom()
		{
			return CurrentRoom;
		}
	}
}
