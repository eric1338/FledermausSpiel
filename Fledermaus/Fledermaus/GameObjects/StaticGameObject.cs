using OpenTK;
using System.Collections.Generic;
using System;

namespace Fledermaus.GameObjects
{
	public class StaticGameObject : IBounded
	{

		private List<Line> _lines = new List<Line>();

		protected void AddLine(Line line)
		{
			_lines.Add(line);
		}

		protected void AddLine(Vector2 point1, Vector2 point2)
		{
			AddLine(new Line(point1, point2));
		}

		public IEnumerable<Line> GetLines()
		{
			return _lines;
		}
	}
}
