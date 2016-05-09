using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	class Room : StaticGameObject
	{

		// Derzeit nicht gebraucht, evtl von RectangularGameObject erben

        public Vector2 LeftBottom { get; set; }
        public Vector2 RightBottom { get; set; }
        public Vector2 LeftTop { get; set; }
        public Vector2 RightTop { get; set; }
		
        public Room(Vector2 position, Vector2 leftBottom, Vector2 rightBottom, Vector2 leftTop, Vector2 rightTop) : base(position) {
            LeftBottom = leftBottom;
            RightBottom = rightBottom;
            LeftTop = leftTop;
            RightTop = rightTop;

            AddLine(leftBottom, rightBottom);
            AddLine(rightBottom, rightTop);
            AddLine(rightTop, leftTop);
            AddLine(leftTop, leftBottom);
        }

	}
}
