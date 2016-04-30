using Fledermaus.GameObjects;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.Utils
{
	class Util
	{

		public static Vector2 GetRotatedVector(Vector2 vector, float angle)
		{
			return GetRotatedVector(vector, angle, false);
		}

		public static Vector2 GetRotatedVector(Vector2 vector, float angle, bool clockwise)
		{
			if (clockwise) angle *= -1;

			Matrix2 rotationMatrix = Matrix2.CreateRotation(angle);

			float x = rotationMatrix.M11 * vector.X + rotationMatrix.M12 * vector.Y;
			float y = rotationMatrix.M21 * vector.X + rotationMatrix.M22 * vector.Y;

			return new Vector2(x, y);
		}

		public static Vector2 GetOrthogonalVectorCW(Vector2 vector)
		{
			return GetOrthogonalVector(vector, true);
		}

		public static Vector2 GetOrthogonalVectorCCW(Vector2 vector)
		{
			return GetOrthogonalVector(vector, false);
		}

		public static Vector2 GetOrthogonalVector(Vector2 vector, bool clockwise)
		{
			float xFactor = clockwise ? 1 : -1;
			float yFactor = xFactor * -1;

			return new Vector2(xFactor * vector.Y, yFactor * vector.X);
		}
		

		public static Intersection GetIntersection(Line v1, Line v2)
		{
			Vector2 p1o = v1.GetOriginVector();
			Vector2 p1d = v1.GetDirectionVector();
			Vector2 p2o = v2.GetOriginVector();
			Vector2 p2d = v2.GetDirectionVector();

			float dot = Vector2.Dot(p1d, p2d);

			if (dot == -1 || dot == 1) return null;

			// TODO: Parallelität testen

			float x1 = p1o.X;
			float y1 = p1o.Y;
			float x2 = p1d.X;
			float y2 = p1d.Y;
			float x3 = p2o.X;
			float y3 = p2o.Y;
			float x4 = p2d.X;
			float y4 = p2d.Y;

			float t = (-x1 * y4 + x3 * y4 + x4 * y1 - x4 * y3) / (x2 * y4 - x4 * y2);

			float u = x4 == 0.0f ? ((y1 + t * y2 - y3) / y4) : ((x1 + t * x2 - x3) / x4);

			if (t > 0 && t <= 1 && ((u > 0 && u <= 1) || x4 == 0))
			{
				return new Intersection(p1o + p1d * t, t);
			}

			return null;
		}


		public static float ConvertDegreeToRadian(float degree)
		{
			return degree * 0.0175f;
		}

	}
}
