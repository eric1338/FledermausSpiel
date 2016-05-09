using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	class Player : GameObject
	{

		// TODO: PlayerSize / Hitboxen

		private Vector2 _initialPosition;

		public Mirror CurrentMirror { get; set; }

		public Player(Vector2 initialPosition) : base(initialPosition)
		{
			_initialPosition = initialPosition;
			Position = _initialPosition;
		}

		public void Move(float dx, float dy)
		{
			Move(new Vector2(dx, dy));
		}

		public void Move(Vector2 deltaVector)
		{
			Position += deltaVector;
		}

		public void Reset()
		{
			UnfocusCurrentMirror();
			Position = _initialPosition;
		}

		public void FocusMirror(Mirror mirror)
		{
			CurrentMirror = mirror;
		}

		public void UnfocusCurrentMirror()
		{
			CurrentMirror = null;
		}

		public bool IsFocusedToMirror()
		{
			return CurrentMirror != null;
		}

		public override List<Line> GetLines()
		{
			Vector2 v1 = GetTempVector(1, 1);
			Vector2 v2 = GetTempVector(1, -1);
			Vector2 v3 = GetTempVector(-1, -1);
			Vector2 v4 = GetTempVector(-1, 1);

			List<Line> lines = new List<Line>();
			lines.Add(new Line(v1, v2));
			lines.Add(new Line(v2, v3));
			lines.Add(new Line(v3, v4));
			lines.Add(new Line(v1, v4));

			return lines;
		}

		private Vector2 GetTempVector(float xF, float yF)
		{
			return Position + new Vector2(0.05f * xF, 0.05f * yF);
		}

		public Player CreateClone()
		{
			Player player = new Player(_initialPosition);
			player.Position = Position;

			return player;
		}
	}
}
