using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	class Level : ILogicalLevel
	{

		public List<Room> Rooms { get; set; }
		public Room CurrentRoom;

		private Stopwatch _stopwatch = new Stopwatch();
		public List<float> Times = new List<float>();

		public Level()
		{
			Rooms = new List<Room>();
			_stopwatch.Start();
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

			AddNewRoomTime();
			_stopwatch.Start();
		}

		private void AddNewRoomTime()
		{
			_stopwatch.Stop();
			Times.Add((float)_stopwatch.Elapsed.TotalSeconds);
			_stopwatch.Reset();
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

		public void StartTimer()
		{
			_stopwatch.Start();
		}

		public void PauseTimer()
		{
			_stopwatch.Stop();
		}

		public void UnpauseTimer()
		{
			_stopwatch.Start();
		}

		public void StopTimer()
		{
			_stopwatch.Stop();
		}

		public void FinishLevel()
		{
			AddNewRoomTime();
		}

		public float GetTotalTime()
		{
			float totalTime = 0.0f;

			foreach (float time in Times) totalTime += time;

			return totalTime;
		}
	}
}
