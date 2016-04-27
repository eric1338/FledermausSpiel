using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	class Player : IBounded
	{

		// TODO: PlayerSize / Hitboxen

		private Vector2 _initialPosition;
		public Vector2 Position { get; set; }

		public Mirror CurrentMirror { get; set; }

		public Player(float x, float y) : this(new Vector2(x, y))
		{
		}

		public Player(Vector2 initialPosition)
		{
			_initialPosition = initialPosition;
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
			Move(new Vector2(dx, dy));
		}

		public void Move(Vector2 deltaVector)
		{
			Position += deltaVector;
		}

		public void Reset()
		{
			Position = _initialPosition;
		}

		public List<Line> GetLines()
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
