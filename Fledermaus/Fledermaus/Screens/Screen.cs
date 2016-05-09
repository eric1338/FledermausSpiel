using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.Screens
{
	abstract class Screen
	{

		protected InputManager _inputManager = new InputManager();
        protected MyGameWindow _myGameWindow;

        public Screen(MyGameWindow win) {
            _myGameWindow = win;
        }

		public abstract void DoLogic();
		public abstract void Draw();


		public void ProcessKeyUp(Key key)
		{
			_inputManager.ProcessKeyUp(key);
		}

		public void ProcessKeyDown(Key key)
		{
			_inputManager.ProcessKeyDown(key);
		}


	}
}
