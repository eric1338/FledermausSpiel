using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	class LightRay
	{

		public Vector2 Origin { get; set; }
		public Vector2 LightVector { get; set; }

		public LightRay(Vector2 origin, Vector2 lightVector)
		{
			Origin = origin;
			LightVector = lightVector;
		}

	}
}
