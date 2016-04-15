using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	class Room
	{

		public float LeftX { get; set; }
		public float RightX { get; set; }
		public float TopY { get; set; }
		public float BottomY { get; set; }

		public Room(float leftX, float rightX, float topY, float bottomY)
		{
			LeftX = leftX;
			RightX = rightX;
			TopY = topY;
			BottomY = bottomY;
		}



	}
}
