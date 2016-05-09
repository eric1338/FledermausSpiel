using OpenTK;
using System.Collections.Generic;

namespace Fledermaus.GameObjects
{
	class StaticGameObject : GameObject
	{

		public StaticGameObject()
		{

		}

		public StaticGameObject(Vector2 position) : base(position)
		{

		}

		private List<Line> _lines = new List<Line>();

		protected void AddLine(Line line)
		{
			_lines.Add(line);
		}

		protected void AddLine(Vector2 point1, Vector2 point2)
		{
			AddLine(new Line(point1, point2));
		}

		public override List<Line> GetLines()
		{
			return _lines;
		}
	}
}
