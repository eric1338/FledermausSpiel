using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	class Room : GameObject
	{


        public float LeftX { get; set; }
		public float RightX { get; set; }
		public float TopY { get; set; }
		public float BottomY { get; set; }

        public Vector2 LeftBottom { get; set; }
        public Vector2 RightBottom { get; set; }
        public Vector2 LeftTop { get; set; }
        public Vector2 RightTop { get; set; }



        public Room(float leftX, float rightX, float topY, float bottomY)
		{
			LeftX = leftX;
			RightX = rightX;
			TopY = topY;
			BottomY = bottomY;


		}
        public Room(Vector2 leftBottom, Vector2 rightBottom, Vector2 leftTop, Vector2 rightTop) {
            LeftBottom = leftBottom;
            RightBottom = rightBottom;
            LeftTop = leftTop;
            RightTop = rightTop;

            AddVertex(leftBottom, rightBottom);
            AddVertex(rightBottom, rightTop);
            AddVertex(rightTop, leftTop);
            AddVertex(leftTop, leftBottom);
        }

	}
}
