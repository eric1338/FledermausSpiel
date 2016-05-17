using Fledermaus.Screens;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.Screens
{
    class LoadMenuScreen : MenuScreen
    {

        public LoadMenuScreen(MyGameWindow win) : base(win)
        {
            menuButtons.Add(new MenuButton( "Level 1", delegate(MyGameWindow _myGameWindow) { _myGameWindow.CurrentScreen = new GameScreen(_myGameWindow); },true));
            menuButtons.Add(new MenuButton( "Level 2", delegate (MyGameWindow _myGameWindow) { }));
     //       menuButtons.Add(new MenuButton( "Level 3", delegate (MyGameWindow _myGameWindow) { }));
   //         menuButtons.Add(new MenuButton( "Level 4", delegate (MyGameWindow _myGameWindow) { }));
 //           menuButtons.Add(new MenuButton( "Level 5", delegate (MyGameWindow _myGameWindow) { }));
            menuButtons.Add(new MenuButton( "Back", delegate (MyGameWindow _myGameWindow) { _myGameWindow.CurrentScreen = new MainMenuScreen(_myGameWindow); }));

        }
    }
}
