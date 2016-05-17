using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	interface ILogicalLightRay : IBounded
	{

		Line GetLastRay();

		void AddNewRay(Vector2 point, Vector2 direction);
		void FinishRays(Vector2 lastPoint);
		void ResetRays();

	}
}
