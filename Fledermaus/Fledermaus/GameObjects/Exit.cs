using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	class Exit
	{

		public bool IsOpen { get; set; }

		public Exit(float x, float y)
		{
			IsOpen = false;
		}

	}
}
