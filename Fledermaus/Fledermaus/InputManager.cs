using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus
{
	public static class InputManager
	{

		private static Dictionary<Key, UserAction> _prolongedUserActionKeyMap = new Dictionary<Key, UserAction>();
        private static Dictionary<Key, UserAction> _singleUserActionKeyMap = new Dictionary<Key, UserAction>();

        private static Dictionary<UserAction, bool> ProlongedUserActions = new Dictionary<UserAction, bool>();
        private static Queue<UserAction> SingleUserActions = new Queue<UserAction>();

        static InputManager()
		{

		}
        public static void Clear()
        {
            _singleUserActionKeyMap.Clear();
            _prolongedUserActionKeyMap.Clear();
        }
        public static void ProcessKeyUp(Key key)
		{
			if (_prolongedUserActionKeyMap.ContainsKey(key))
			{
				ProlongedUserActions[_prolongedUserActionKeyMap[key]] = false;
			}
		}

        public static void ProcessKeyDown(Key key)
		{
			if (_prolongedUserActionKeyMap.ContainsKey(key))
			{
				ProlongedUserActions[_prolongedUserActionKeyMap[key]] = true;
			}

			if (_singleUserActionKeyMap.ContainsKey(key))
			{
				SingleUserActions.Enqueue(_singleUserActionKeyMap[key]);
			}
		}


        public static bool IsUserActionActive(UserAction userAction)
		{
			return ProlongedUserActions.ContainsKey(userAction) && ProlongedUserActions[userAction];
		}

        public static List<UserAction> GetSingleUserActionsAsList()
		{
			List<UserAction> singleUserActions = SingleUserActions.ToList<UserAction>();
			SingleUserActions.Clear();

			return singleUserActions;
		}

        public static void AddProlongedUserActionMapping(Key key, UserAction userAction)
		{
			_prolongedUserActionKeyMap.Add(key, userAction);
		}

        public static void AddSingleUserActionMapping(Key key, UserAction userAction)
		{
			_singleUserActionKeyMap.Add(key, userAction);
		}


	}
}
