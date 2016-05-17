using Fledermaus.GameObjects;
using Fledermaus.Utils;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus
{
	public class Room : ILogicalRoom
	{

		public Vector2 LevelCenter { get; set; }

		public RectangularGameObject RoomBounds { get; set; }

		public Player Player { get; set; }
		public LightRay LightRay { get; set; }
		public RectangularGameObject SolarPanel { get; set; }
		public RectangularGameObject Exit { get; set; }

		public List<Mirror> Mirrors { get; set; }
		public List<Obstacle> Obstacles { get; set; }
		public List<NPC> NPCs { get; set; }

		public bool IsExitOpen { get; set; }

		public Room()
		{
			Mirrors = new List<Mirror>();
			Obstacles = new List<Obstacle>();
			NPCs = new List<NPC>();
		}

		public void AddMirror(Mirror mirror)
		{
			Mirrors.Add(mirror);
		}

		public void AddObstacle(Obstacle obstacle)
		{
			Obstacles.Add(obstacle);
		}

		public void AddNPC(NPC npc)
		{
			NPCs.Add(npc);
		}

		public void Reset()
		{
			Player.Reset();
			foreach (Mirror mirror in Mirrors) mirror.Reset();
			foreach (NPC npc in NPCs) npc.Reset();
		}

		public ILogicalPlayer GetLogicalPlayer()
		{
			return Player;
		}

		public ILogicalLightRay GetLogicalLightRay()
		{
			return LightRay;
		}

		public IEnumerable<ILogicalMirror> GetLogicalMirrors()
		{
			return Mirrors;
		}

		public IEnumerable<ILogicalNPC> GetLogicalNPCs()
		{
			return NPCs;
		}

		public IBounded GetReflectingLines()
		{
			List<Line> reflectingLines = new List<Line>();

			foreach (ILogicalMirror mirror in GetLogicalMirrors())
			{
				reflectingLines.Add(mirror.GetMirrorLine());
			}

			return Util.CreateBoundsFromList(reflectingLines);
		}

		public IBounded GetNonReflectingLines()
		{
			List<Line> nonReflectingLines = new List<Line>();

			nonReflectingLines.AddRange(RoomBounds.GetLines());

			foreach (Obstacle obstacle in Obstacles) nonReflectingLines.AddRange(obstacle.GetLines());

			return Util.CreateBoundsFromList(nonReflectingLines);
		}

		public IBounded GetLightLines()
		{
			List<Line> lightLines = new List<Line>();

			lightLines.AddRange(LightRay.GetLines());

			foreach (NPC npc in NPCs) lightLines.AddRange(npc.GetLines());

			return Util.CreateBoundsFromList(lightLines);
		}

		public IBounded GetSolarPanelLines()
		{
			return SolarPanel;
		}

		public IBounded GetExitLines()
		{
			return Exit;
		}

	}
}
