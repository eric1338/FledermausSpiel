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

		private Vector2 _initialPosition;
		public Vector2 Position { get; set; }

		public Mirror CurrentMirror { get; set; }

		//public bool LockedToMirror { get; set; }

		public Player(float x, float y)
		{
			_initialPosition = new Vector2(x, y);
			Position = _initialPosition;
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

		public void Reset()
		{
			Position = _initialPosition;
		}

	}
}
