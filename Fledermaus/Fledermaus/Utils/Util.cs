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

		public class SimpleBounds : IBounded
		{
			private List<Line> _lines;

			public SimpleBounds(List<Line> lines)
			{
				_lines = lines;
			}

			public IEnumerable<Line> GetLines()
			{
				return _lines;
			}
		}

		public static IBounded CreateBoundsFromList(List<Line> lines)
		{
			return new SimpleBounds(lines);
		}

		public static string GetTimeString(float time)
		{
			return Math.Round(time, 2) + " s";
		}

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

		public static float CalculateAngle(Vector2 v1, Vector2 v2)
		{
			Vector3 v13d = new Vector3(v1.X, v1.Y, 0.0f);
			Vector3 v23d = new Vector3(v2.X, v2.Y, 0.0f);

			return Vector3.CalculateAngle(v13d, v23d);
		}

		private static Random _random = null;

		public static float GetRandomFloat()
		{
			if (_random == null) _random = new Random(DateTime.Now.Millisecond);

			return (float) _random.NextDouble();
		}

		// INTERSECTION

		public static bool HasIntersection(IBounded bounds1, IBounded bounds2)
		{
			foreach (Line line in bounds1.GetLines())
			{
				if (HasIntersection(line, bounds2)) return true;
			}

			return false;
		}

		public static bool HasIntersection(Line line, IBounded bounds)
		{
			List<Vector2> intersections = new List<Vector2>();

			foreach (Line otherLine in bounds.GetLines())
			{
				Intersection intersection = GetIntersection(line, otherLine);

				if (intersection != null) return true;
			}

			return false;
		}

		public static Intersection GetClosestIntersection(Line line, IBounded bounds)
		{
			List<Intersection> intersections = GetIntersections(line, bounds);

			if (intersections.Count < 1) return null;

			intersections.Sort();

			return intersections[0];
		}

		public static List<Intersection> GetIntersections(IBounded bounds1, IBounded bounds2)
		{
			List<Intersection> intersections = new List<Intersection>();

			foreach (Line line in bounds1.GetLines())
			{
				intersections.AddRange(GetIntersections(line, bounds2));
			}

			return intersections;
		}

		public static List<Intersection> GetIntersections(Line line, IBounded bounds)
		{
			List<Intersection> intersections = new List<Intersection>();

			foreach (Line otherLine in bounds.GetLines())
			{
				Intersection intersection = GetIntersection(line, otherLine);

				if (intersection != null) intersections.Add(intersection);
			}

			return intersections;
		}

		public static Intersection GetIntersection(Line v1, Line v2)
		{
			Vector2 p1o = v1.GetOriginVector();
			Vector2 p1d = v1.GetDirectionVector();
			Vector2 p2o = v2.GetOriginVector();
			Vector2 p2d = v2.GetDirectionVector();

			float dot = Vector2.Dot(p1d, p2d);

			//if (dot == -1 || dot == 1) return null;

			// TODO: Parallelität testen

			float x1 = p1o.X;
			float y1 = p1o.Y;
			float x2 = p1d.X;
			float y2 = p1d.Y;
			float x3 = p2o.X;
			float y3 = p2o.Y;
			float x4 = p2d.X;
			float y4 = p2d.Y;

			// TODO: alles iwie überarbeiten :/

			if (x2 * y4 - x4 * y2 == 0)
			{
				x2 -= 0.0001f;
				y4 += 0.0001f;
				x4 -= 0.00008f;
				y2 += 0.00012f;
			}

			float t = (-x1 * y4 + x3 * y4 + x4 * y1 - x4 * y3) / (x2 * y4 - x4 * y2);

			float u = x4 == 0.0f ? ((y1 + t * y2 - y3) / y4) : ((x1 + t * x2 - x3) / x4);

			if (t > 0 && t <= 1 && (u > 0 && u <= 1))
			{
				return new Intersection(p1o + p1d * t, t);
			}

			return null;
		}


	}
}
