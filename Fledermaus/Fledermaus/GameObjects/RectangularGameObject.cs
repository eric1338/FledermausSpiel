using Framework;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	public class RectangularGameObject : StaticGameObject
	{
		
		public Vector2 Point1 { get; set; }
		public Vector2 Point2 { get; set; }
		public Vector2 Point3 { get; set; }
		public Vector2 Point4 { get; set; }

		public Vector2 TopLeft;
		public Vector2 BottomRight;

		public RectangularGameObject(Vector2 topLeft, Vector2 bottomRight)
		{
			Point1 = topLeft;
			Point2 = new Vector2(bottomRight.X, topLeft.Y);
			Point3 = bottomRight;
			Point4 = new Vector2(topLeft.X, bottomRight.Y);

			TopLeft = topLeft;
			BottomRight = bottomRight;

			CreateLines();
		}

		private void CreateLines()
		{
			AddLine(Point1, Point2);
			AddLine(Point2, Point3);
			AddLine(Point3, Point4);
			AddLine(Point1, Point4);
		}

		public float GetWidth()
		{
			return (Point2 - Point1).Length;
		}

		public float GetHeight()
		{
			return (Point2 - Point3).Length;
		}
	}
}
