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

		public RectangularGameObject(float leftX, float topY, float rightX, float bottomY) :
			this (new Vector2(leftX, topY), new Vector2(rightX, topY), new Vector2(rightX, bottomY), new Vector2(leftX, bottomY))
		{

		}

		public RectangularGameObject(Vector2 point1, Vector2 point2, Vector2 point3, Vector2 point4)
		{
			Point1 = point1;
			Point2 = point2;
			Point3 = point3;
			Point4 = point4;

			AddLine(point1, point2);
			AddLine(point2, point3);
			AddLine(point3, point4);
			AddLine(point1, point4);

			aabr = new AABR(Point4.X, Point4.Y, GetWidth(), GetHeight());
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
