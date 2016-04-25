using Fledermaus.GameObjects;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus
{
	class Util
	{

		public static Vector2 GetRotatedVector(Vector2 vector, float degree)
		{
			return GetRotatedVector(vector, degree, false);
		}

		public static Vector2 GetRotatedVector(Vector2 vector, float degree, bool clockwise)
		{
			float dg = clockwise ? (360 - degree) : degree;
			float angle = dg * (float) (Math.PI / 180);

			Matrix2 rM = Matrix2.CreateRotation(angle);

			float f1 = rM.M11 * vector.X + rM.M12 * vector.Y;
			float f2 = rM.M21 * vector.X + rM.M22 * vector.Y;

			return new Vector2(f1, f2);
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




		public static bool HasIntersection(IBounded gameObject1, IBounded gameObject2)
		{
			foreach (Line line in gameObject1.GetLines())
			{
				if (HasIntersection(line, gameObject2)) return true;
			}

			return false;
		}

		public static bool HasIntersection(Line line, IBounded gameObject)
		{
			return HasIntersection(line, gameObject.GetLines());
		}

		public static bool HasIntersection(Line line, List<Line> lines)
		{
			List<Vector2> intersections = new List<Vector2>();

			foreach (Line otherLine in lines)
			{
				Vector2? intersection = GetIntersection(line, otherLine);

				if (intersection != null) return true;
			}

			return false;
		}

		public static List<Vector2> GetIntersections(Line line, List<IBounded> gameObjects)
		{
			List<Vector2> intersections = new List<Vector2>();

			foreach (IBounded gameObject in gameObjects)
			{
				intersections.AddRange(GetIntersections(line, gameObject));
			}

			return intersections;
		}

		public static List<Vector2> GetIntersections(Line line, IBounded gameObject)
		{
			return GetIntersections(line, gameObject.GetLines());
		}

		public static List<Vector2> GetIntersections(Line line, List<Line> lines)
		{
			List<Vector2> intersections = new List<Vector2>();

			foreach (Line otherLine in lines)
			{
				Vector2? intersection = GetIntersection(line, otherLine);

				if (intersection != null) intersections.Add((Vector2)intersection);
			}

			return intersections;
		}

		public static Vector2? GetIntersection(Line v1, Line v2)
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
			float u = (x1 + t * x2 - x3) / x4;

			if (t >= 0 && t <= 1 && u >= 0 && u <= 1)
			{
				return p1o + p1d * t;
			}

			return null;
		}

	}
}
