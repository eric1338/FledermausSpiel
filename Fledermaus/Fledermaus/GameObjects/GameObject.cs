using Fledermaus.Utils;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	abstract class GameObject
	{

		public Vector2 Position;

		public abstract List<Line> GetLines();

		public GameObject()
		{

		}

		public GameObject(Vector2 position)
		{
			Position = position;
		}


		public Intersection GetClosestIntersection(List<GameObject> gameObjects)
		{
			List<Intersection> intersections = GetIntersections(gameObjects);

			if (intersections.Count < 1) return null;

			intersections.Sort();

			return intersections[0];
		}

		public bool HasIntersection(List<GameObject> gameObjects)
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (HasIntersection(gameObject)) return true;
			}

			return false;
		}

		public bool HasIntersection(GameObject gameObject)
		{
			foreach (Line line in GetLines())
			{
				if (Util.HasIntersection(line, gameObject)) return true;
			}

			return false;
		}

		private List<Intersection> GetIntersections(List<GameObject> gameObjects)
		{
			List<Intersection> intersections = new List<Intersection>();

			foreach (Line line in GetLines())
			{
				intersections.AddRange(Util.GetIntersections(line, gameObjects));
			}

			return intersections;
		}



	}
}
