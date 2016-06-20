using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	public class Player : ILogicalPlayer
	{

		public Vector2 Position { get; set; }
		public float Rotation { get; set; }

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
			Vector2 point1 = GetRelativePoint(0.0f, 0.21f);

			Vector2 point2 = GetRelativePoint(-0.32f, 0.42f);
			Vector2 point3 = GetRelativePoint(-0.24f, 0.0f);
			Vector2 point4 = GetRelativePoint(-0.55f, 0.05f);
			Vector2 point5 = GetRelativePoint(-0.82f, 0.22f);
			Vector2 point6 = GetRelativePoint(-0.5f, -0.3f);

			Vector2 point7 = GetRelativePoint(0.0f, -0.5f);

			Vector2 point8 = GetRelativePoint(0.5f, -0.3f);
			Vector2 point9 = GetRelativePoint(0.82f, 0.22f);
			Vector2 point10 = GetRelativePoint(0.55f, 0.05f);
			Vector2 point11 = GetRelativePoint(0.24f, 0.0f);
			Vector2 point12 = GetRelativePoint(0.32f, 0.42f);

			List<Line> lines = new List<Line>();
			lines.Add(new Line(point1, point2));
			lines.Add(new Line(point2, point3));
			lines.Add(new Line(point3, point4));
			lines.Add(new Line(point4, point5));
			lines.Add(new Line(point5, point6));
			lines.Add(new Line(point6, point7));
			lines.Add(new Line(point7, point8));
			lines.Add(new Line(point8, point9));
			lines.Add(new Line(point9, point10));
			lines.Add(new Line(point10, point11));
			lines.Add(new Line(point11, point12));
			lines.Add(new Line(point12, point1));

			return lines;
		}

		public Vector2 GetRelativePoint(float dx, float dy)
		{
			Vector2 relativePoint = new Vector2(dx * 0.1f, dy * 0.1f);
			Vector2 rotatedPoint = Utils.Util.GetRotatedVector(relativePoint, Rotation);

			return new Vector2(Position.X + rotatedPoint.X, Position.Y + rotatedPoint.Y);
		}

		public void Reset()
		{
			UnfocusCurrentMirror();
			Position = _initialPosition;
			Rotation = 0.0f;
		}

		public ILogicalPlayer CreateClone()
		{
			Player player = new Player(_initialPosition);
			player.Position = Position;

			return player;
		}
	}
}
