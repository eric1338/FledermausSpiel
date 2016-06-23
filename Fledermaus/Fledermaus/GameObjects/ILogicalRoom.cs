using Fledermaus.GameObjects;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus
{
	public interface ILogicalRoom
	{

		int NextRoomIndex { get; }

		ILogicalPlayer GetLogicalPlayer();
		IEnumerable<ILogicalLightRay> GetLogicalLightRays();
		IEnumerable<ILogicalMirror> GetLogicalMirrors();

		IBounded GetReflectingBounds();
		IBounded GetNonReflectingBounds();
		IBounded GetLightBounds();
		
		IBounded GetExitBounds();

		void Reset();
	}
}
