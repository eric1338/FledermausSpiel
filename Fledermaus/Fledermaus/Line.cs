using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus
{
	public class Line
	{

		public Vector2 Point1 { get; set; }
		public Vector2 Point2 { get; set; }

		public Line(Vector2 point1, Vector2 point2)
		{
			Point1 = point1;
			Point2 = point2;
		}

		public static Line CreateParameterized(Vector2 origin, Vector2 direction, float factor)
		{
			return new Line(origin, origin + direction * factor);
		}

		public Vector2 GetOriginVector()
		{
			return Point1;
		}

		public Vector2 GetDirectionVector()
		{
			return Point2 - Point1;
		}

	}
}
