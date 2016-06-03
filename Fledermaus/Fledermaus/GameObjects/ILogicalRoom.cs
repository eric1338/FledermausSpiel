using Fledermaus.GameObjects;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus
{
	interface ILogicalRoom
	{

		ILogicalPlayer GetLogicalPlayer();
		IEnumerable<ILogicalLightRay> GetLogicalLightRays();
		IEnumerable<ILogicalMirror> GetLogicalMirrors();
		IEnumerable<ILogicalNPC> GetLogicalNPCs();

		IEnumerable<Tuple<IBounded, int>> GetRoomTransitionTriggers();

		IBounded GetReflectingBounds();
		IBounded GetNonReflectingBounds();

		IBounded GetLightBounds();

		IBounded GetSolarPanelBounds();
		IBounded GetExitBounds();

		bool IsExitOpen { get; set; }

		void Reset();
	}
}
