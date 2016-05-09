using OpenTK;
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

		public Exit(Vector2 topLeft, Vector2 bottomRight) : base(topLeft, bottomRight)
		{
			IsOpen = false;
		}

		public Exit(Vector2 position, float width, float height) : base(position, width, height)
		{
			IsOpen = false;
		}

	}
}
