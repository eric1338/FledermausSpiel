using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	class StaticGameObject : GameObject
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

		public override List<Line> GetLines()
		{
			return _lines;
		}
	}
}
