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
		ILogicalLightRay GetLogicalLightRay();
		IEnumerable<ILogicalMirror> GetLogicalMirrors();
		IEnumerable<ILogicalNPC> GetLogicalNPCs();

		IEnumerable<Tuple<IBounded, int>> GetRoomTransitionTriggers();

		IBounded GetReflectingLines();
		IBounded GetNonReflectingLines();

		IBounded GetLightLines();

		IBounded GetSolarPanelLines();
		IBounded GetExitLines();

		bool IsExitOpen { get; set; }

		void Reset();
	}
}
