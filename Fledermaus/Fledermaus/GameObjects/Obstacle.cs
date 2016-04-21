using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	class Obstacle : StaticGameObject
	{

		public List<Vector2> Points { get; set; }

		public Obstacle()
		{

		}

		public void AddPoint(Vector2 point)
		{
			Points.Add(point);
		}


	}
}
