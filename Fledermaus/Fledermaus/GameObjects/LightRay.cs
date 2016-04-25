using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	class LightRay : IBounded
	{

		public Vector2 Origin { get; set; }
		public Vector2 FirstDirection { get; set; }




		private Line _lastRay;
		private List<Line> _rays = new List<Line>();

		public LightRay(Vector2 origin, Vector2 firstDirection)
		{
			Origin = origin;
			FirstDirection = firstDirection;

			ResetRays();
			CreateLastRay(Origin, FirstDirection);
		}

		public void ResetRays()
		{
			_lastRay = CreateLastRay(Origin, FirstDirection);
			_rays.Clear();
		}
		
		public Line GetLastRay()
		{
			return _lastRay;
		}

		private Line CreateLastRay(Vector2 origin, Vector2 direction)
		{
			Vector2 newDirection = direction;
			newDirection.Normalize();

			return Line.CreateParameterized(origin, newDirection, 50); // TODO: evtl anderer Faktor
		}

		public void FinishRays(Vector2 lastPoint)
		{
			_rays.Add(new Line(_lastRay.Point1, lastPoint));
		}

		public void AddNewRay(Vector2 origin, Vector2 direction)
		{
			_rays.Add(new Line(_lastRay.Point1, origin));
			_lastRay = CreateLastRay(origin, direction);
		}

		public List<Line> GetLines()
		{
			return _rays;
		}
	}
}
