﻿using Fledermaus.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus
{
	public class Level
	{

		public RectangularGameObject Room { get; set; }

		public Player Player { get; set; }
		public LightRay LightRay { get; set; }
		public SolarPanel SolarPanel { get; set; }
		public Exit Exit { get; set; }

		public List<Mirror> Mirrors { get; set; }

		public List<Obstacle> Obstacles { get; set; }

		public Level()
		{
			Mirrors = new List<Mirror>();
			Obstacles = new List<Obstacle>();
		}

		public void AddMirror(Mirror mirror)
		{
			Mirrors.Add(mirror);
		}

		public void AddObstacle(Obstacle obstacle)
		{
			Obstacles.Add(obstacle);
		}

		public void Reset()
		{
			Player.Reset();
			foreach (Mirror mirror in Mirrors) mirror.Reset();
		}

		public List<Line> GetReflectingLines()
		{
			List<Line> reflectingLines = new List<Line>();

			foreach (Mirror mirror in Mirrors)
			{
				reflectingLines.Add(mirror.GetMirrorLine());
			}

			return reflectingLines;
		}

		public List<GameObject> GetReflectingGameObejcts()
		{
			List<GameObject> reflectingGameObejcts = new List<GameObject>();

			foreach (Mirror mirror in Mirrors)
			{
				reflectingGameObejcts.Add(mirror);
			}

			return reflectingGameObejcts;
		}

		public List<GameObject> GetNonReflectingGameObjects()
		{
			List<GameObject> nonReflectingGameObjects = new List<GameObject>();

			nonReflectingGameObjects.Add(Room);
			nonReflectingGameObjects.AddRange(Obstacles);

			return nonReflectingGameObjects;
		}

	}
}
