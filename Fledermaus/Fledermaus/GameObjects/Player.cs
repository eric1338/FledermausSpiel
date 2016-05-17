using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	class Player : ILogicalPlayer
	{

		public Vector2 Position { get; set; }

		// TODO: PlayerSize / Hitboxen

		private Vector2 _initialPosition;

		public Vector2 VectorToMirror { get; set; }

		public ILogicalMirror CurrentMirror { get; set; }

		public Player(Vector2 initialPosition)
		{
			_initialPosition = initialPosition;
			Position = _initialPosition;
		}

		public void FocusMirror(ILogicalMirror mirror)
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

		public IEnumerable<Line> GetLines()
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

		public void Reset()
		{
			UnfocusCurrentMirror();
			Position = _initialPosition;
		}

		public ILogicalPlayer CreateClone()
		{
			Player player = new Player(_initialPosition);
			player.Position = Position;

			return player;
		}
	}
}
