using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	class Player
	{

		// TODO: PlayerSize / Hitboxen

		public Vector2 Position { get; set; }
		public bool LockedToMirror { get; set; }

		public Player(float x, float y)
		{
			Position = new Vector2(x, y);
		}

		public void MoveX(float dx)
		{
			Move(dx, 0.0f);
		}

		public void MoveY(float dy)
		{
			Move(0.0f, dy);
		}

		public void Move(float dx, float dy)
		{
			Position += new Vector2(dx, dy);
		}

	}
}
