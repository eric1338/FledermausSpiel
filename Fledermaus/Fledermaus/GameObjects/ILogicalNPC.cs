using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	interface ILogicalNPC : IBounded
	{

		Vector2 PointOfInterest { get; set; }

		void Tick();
		void Move();
		void TickAndMove();

		Vector2 GetMovementVector();

	}
}
