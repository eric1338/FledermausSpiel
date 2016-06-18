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

		float Rotation { get; set; }

		ILogicalMirror CurrentMirror { get; set; }
		bool IsFocusedToMirror();
		void FocusMirror(ILogicalMirror mirror);
		void UnfocusCurrentMirror();

		ILogicalPlayer CreateClone();

	}
}
