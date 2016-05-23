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
	class Room : ILogicalRoom
	{

		public int Index { get; set; }
		public int Row { get; set; }
		public int Column { get; set; }

		public RectangularGameObject RoomBounds { get; set; }

		public Player Player { get; set; }
		public RectangularGameObject SolarPanel { get; set; }
		public RectangularGameObject Exit { get; set; }

		public List<LightRay> LightRays { get; set; }
		public List<Mirror> Mirrors { get; set; }
		public List<Obstacle> Obstacles { get; set; }
		public List<NPC> NPCs { get; set; }

		public List<Tuple<IBounded, int>> RoomTransitionTriggers { get; set; }

		public bool IsExitOpen { get; set; }

		public Room()
		{
			LightRays = new List<LightRay>();
			Mirrors = new List<Mirror>();
			Obstacles = new List<Obstacle>();
			NPCs = new List<NPC>();

			RoomTransitionTriggers = new List<Tuple<IBounded, int>>();
		}

		public void AddLightRay(LightRay lightRay)
		{
			LightRays.Add(lightRay);
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

		public void AddRoomTransitionTrigger(IBounded triggerArea, int roomIndex)
		{
			RoomTransitionTriggers.Add(new Tuple<IBounded, int>(triggerArea, roomIndex));
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

		public IEnumerable<ILogicalMirror> GetLogicalMirrors()
		{
			return Mirrors;
		}

		public IEnumerable<ILogicalNPC> GetLogicalNPCs()
		{
			return NPCs;
		}

		public IEnumerable<ILogicalLightRay> GetLogicalLightRays()
		{
			return LightRays;
		}

		public IEnumerable<Tuple<IBounded, int>> GetRoomTransitionTriggers()
		{
			return RoomTransitionTriggers;
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

			foreach (LightRay lightRay in LightRays) lightLines.AddRange(lightRay.GetLines());

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
