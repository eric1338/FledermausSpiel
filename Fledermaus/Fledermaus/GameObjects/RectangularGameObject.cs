using Framework;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	class RectangularGameObject : StaticGameObject
	{
		
		public Vector2 Point1 { get; set; }
		public Vector2 Point2 { get; set; }
		public Vector2 Point3 { get; set; }
		public Vector2 Point4 { get; set; }

		public AABR aabr { get; set; }

		public RectangularGameObject(Vector2 topLeft, Vector2 bottomRight)
		{
			Position = topLeft + 0.5f * (bottomRight - topLeft);

			Point1 = topLeft;
			Point2 = new Vector2(bottomRight.X, topLeft.Y);
			Point3 = bottomRight;
			Point4 = new Vector2(topLeft.X, bottomRight.Y);

			CreateLinesAndAABR();
		}

		public RectangularGameObject(Vector2 position, float width, float height) : base(position)
		{
			float halfWidth = width / 2.0f;
			float halfHeight = height / 2.0f;

			Point1 = new Vector2(position.X - halfWidth, position.Y + halfHeight);
			Point2 = new Vector2(position.X + halfWidth, position.Y + halfHeight);
			Point3 = new Vector2(position.X + halfWidth, position.Y - halfHeight);
			Point4 = new Vector2(position.X - halfWidth, position.Y - halfHeight);

			CreateLinesAndAABR();
		}

		private void CreateLinesAndAABR()
		{
			AddLine(Point1, Point2);
			AddLine(Point2, Point3);
			AddLine(Point3, Point4);
			AddLine(Point1, Point4);

			aabr = new AABR(Point1.X, Point1.Y, Point3.X - Point1.X, Point3.Y - Point1.Y);
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
