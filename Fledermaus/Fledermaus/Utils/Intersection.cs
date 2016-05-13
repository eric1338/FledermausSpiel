using Fledermaus.GameObjects;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.Utils
{

	class Intersection : IComparable<Intersection>
	{
		public Vector2 Point { get; set; }
		public float RelativeDistance { get; set; }

		public Intersection(Vector2 point, float relativeDistance)
		{
			Point = point;
			RelativeDistance = relativeDistance;
		}

		public int CompareTo(Intersection obj)
		{
			return RelativeDistance.CompareTo(obj.RelativeDistance);
		}
	}

}
