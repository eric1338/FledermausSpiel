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

        public Screen() {
        }

		public abstract void DoLogic();
		public abstract void Draw();


		public void ProcessKeyUp(Key key)
		{
            InputManager.ProcessKeyUp(key);
		}

		public void ProcessKeyDown(Key key)
		{
            InputManager.ProcessKeyDown(key);
		}


	}
}
