using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	class Exit : RectangularGameObject
	{

		public bool IsOpen { get; set; }

		public Exit(float leftX, float topY, float rightX, float bottomY) :
			base(leftX, topY, rightX, bottomY)
		{
			IsOpen = false;
		}

	}
}
