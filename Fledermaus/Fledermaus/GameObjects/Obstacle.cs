using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	public class Obstacle : StaticGameObject
	{

		private Vector2 _firstPoint;
		private Vector2 _lastPoint;

		public Obstacle()
		{

		}

		public static Obstacle CreateRectangular(Vector2 topLeft, Vector2 bottomRight)
		{
			Obstacle obstacle = new Obstacle();

			obstacle.AddFirstPoint(topLeft);
			obstacle.AddFollowingPoint(new Vector2(bottomRight.X, topLeft.Y));
			obstacle.AddFollowingPoint(bottomRight);
			obstacle.AddLastPoint(new Vector2(topLeft.X, bottomRight.Y));

			return obstacle;
		}

		public void AddFirstPoint(Vector2 point)
		{
			_firstPoint = point;
			_lastPoint = point;
		}

		public void AddFollowingPoint(Vector2 point)
		{
			AddLine(_lastPoint, point);
			_lastPoint = point;
		}

		public void AddLastPoint(Vector2 point)
		{
			AddLine(_lastPoint, point);
			AddLine(point, _firstPoint);
		}

	}
}
