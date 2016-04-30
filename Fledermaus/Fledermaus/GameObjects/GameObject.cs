﻿using Fledermaus.Utils;
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

		public abstract List<Line> GetLines();


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
				if (HasIntersection(line, gameObject)) return true;
			}

			return false;
		}

		private static bool HasIntersection(Line line, GameObject gameObject)
		{
			List<Vector2> intersections = new List<Vector2>();

			foreach (Line otherLine in gameObject.GetLines())
			{
				Intersection intersection = Util.GetIntersection(line, otherLine);

				if (intersection != null) return true;
			}

			return false;
		}

		private List<Intersection> GetIntersections(List<GameObject> gameObjects)
		{
			List<Intersection> intersections = new List<Intersection>();

			foreach (Line line in GetLines())
			{
				intersections.AddRange(GetIntersections(line, gameObjects));
			}

			return intersections;
		}

		private static List<Intersection> GetIntersections(Line line, List<GameObject> gameObjects)
		{
			List<Intersection> intersections = new List<Intersection>();

			foreach (GameObject gameObject in gameObjects)
			{
				intersections.AddRange(GetIntersections(line, gameObject));
			}

			return intersections;
		}

		private static List<Intersection> GetIntersections(Line line, GameObject gameObject)
		{
			List<Intersection> intersections = new List<Intersection>();

			foreach (Line otherLine in gameObject.GetLines())
			{
				Intersection intersection = Util.GetIntersection(line, otherLine);

				if (intersection != null)
				{
					intersection.GameObject = gameObject;
					intersections.Add(intersection);
				}
			}

			return intersections;
		}

	}
}
