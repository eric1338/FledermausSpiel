using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus
{
	class Inputs
	{

		private Dictionary<Key, UserAction> _keyMap = new Dictionary<Key, UserAction>();

		public Dictionary<UserAction, bool> UserActionStatus { get; set; }

		public Inputs()
		{
			_keyMap.Add(Key.W, UserAction.MoveUp);
			_keyMap.Add(Key.A, UserAction.MoveLeft);
			_keyMap.Add(Key.S, UserAction.MoveDown);
			_keyMap.Add(Key.D, UserAction.MoveRight);
			_keyMap.Add(Key.F, UserAction.ToggleMirrorLock);
			_keyMap.Add(Key.E, UserAction.RotateMirrorCW);
			_keyMap.Add(Key.Q, UserAction.RotateMirrorCCW);

			UserActionStatus = new Dictionary<UserAction, bool>();

			// TODO: anders
			UserActionStatus.Add(UserAction.MoveUp, false);
			UserActionStatus.Add(UserAction.MoveLeft, false);
			UserActionStatus.Add(UserAction.MoveDown, false);
			UserActionStatus.Add(UserAction.MoveRight, false);
			UserActionStatus.Add(UserAction.ToggleMirrorLock, false);
			UserActionStatus.Add(UserAction.RotateMirrorCW, false);
			UserActionStatus.Add(UserAction.RotateMirrorCCW, false);
		}

		public void SetKeyPressing(Key key)
		{
			if (!_keyMap.ContainsKey(key)) return;

			UserAction action = _keyMap[key];
			UserActionStatus[action] = true;
		}

		public void SetKeyReleased(Key key)
		{
			if (!_keyMap.ContainsKey(key)) return;

			UserAction action = _keyMap[key];
			UserActionStatus[action] = false;
		}

	}
}
