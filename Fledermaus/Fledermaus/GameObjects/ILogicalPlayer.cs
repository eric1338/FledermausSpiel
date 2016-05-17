using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
    public interface ILogicalPlayer : IBounded
	{

		Vector2 Position { get; set; }
		Vector2 VectorToMirror { get; set; }

		ILogicalMirror CurrentMirror { get; set; }
		bool IsFocusedToMirror();
		void UnfocusCurrentMirror();

		ILogicalPlayer CreateClone();

	}
}
