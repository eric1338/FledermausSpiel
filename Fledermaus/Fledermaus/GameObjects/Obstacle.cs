using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	public class Obstacle : RectangularGameObject
	{

		public Obstacle(Vector2 topLeft, Vector2 bottomRight) : base(topLeft, bottomRight)
		{

		}

		public static Obstacle CreateRectangular(Vector2 topLeft, Vector2 bottomRight)
		{
			return new Obstacle(topLeft, bottomRight);
		}

	}
}
