using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus
{
	public enum UserAction
	{
		MoveUp,
		MoveDown,
		MoveLeft,
		MoveRight,
		ToggleMirrorLock,
		RotateMirrorCW,
		RotateMirrorCCW,
		ResetLevel,
		TogglePauseGame,
		ToggleGodMode,
		Confirm,
        Cancel
    }
}
