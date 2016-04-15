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

	}
}
