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
			Rotation = 0.0f;
		}

		public bool IsFocusedToMirror()
		{
			return CurrentMirror != null;
		}

		public IEnumerable<Line> GetLines()
		{
			/*
			Vector2 v1 = GetTempVector(1, 1);
			Vector2 v2 = GetTempVector(1, -1);
			Vector2 v3 = GetTempVector(-1, -1);
			Vector2 v4 = GetTempVector(-1, 1);

			List<Line> lines = new List<Line>();
			lines.Add(new Line(v1, v2));
			lines.Add(new Line(v2, v3));
			lines.Add(new Line(v3, v4));
			lines.Add(new Line(v1, v4));
			*/

			/*
			Vector2 point1 = GetRelativePoint(0.0f, 0.02f);
			Vector2 point2 = GetRelativePoint(0.06f, 0.04f);
			Vector2 point3 = GetRelativePoint(0.0f, -0.04f);
			Vector2 point4 = GetRelativePoint(-0.06f, 0.04f);

			List<Line> lines = new List<Line>();
			lines.Add(new Line(point1, point2));
			lines.Add(new Line(point2, point3));
			lines.Add(new Line(point3, point4));
			lines.Add(new Line(point1, point4));
			*/

			Vector2 point1 = GetRelativePoint(0.0f, 0.03f);
			Vector2 point2 = GetRelativePoint(0.025f, 0.01f);
			Vector2 point3 = GetRelativePoint(0.07f, 0.02f);

			Vector2 point4 = GetRelativePoint(0.02f, -0.045f);
			Vector2 point5 = GetRelativePoint(0.0f, -0.03f);
			Vector2 point6 = GetRelativePoint(-0.02f, -0.045f);

			Vector2 point7 = GetRelativePoint(-0.07f, 0.02f);
			Vector2 point8 = GetRelativePoint(-0.025f, 0.01f);

			List<Line> lines = new List<Line>();
			lines.Add(new Line(point1, point2));
			lines.Add(new Line(point2, point3));
			lines.Add(new Line(point3, point4));
			lines.Add(new Line(point4, point5));
			lines.Add(new Line(point5, point6));
			lines.Add(new Line(point6, point7));
			lines.Add(new Line(point7, point8));
			lines.Add(new Line(point8, point1));

			return lines;
		}

		private Vector2 GetRelativePoint(float dx, float dy)
		{
			Vector2 rel = new Vector2(dx, dy);
			Vector2 rr = Utils.Util.GetRotatedVector(rel, Rotation);

			return new Vector2(Position.X + rr.X, Position.Y + rr.Y);

			//Vector2 v = new Vector2(Position.X + dx, Position.Y + dy);

			//return Utils.Util.GetRotatedVector(v, rotation);

			//return new Vector2(Position.X + dx, Position.Y + dy);
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
