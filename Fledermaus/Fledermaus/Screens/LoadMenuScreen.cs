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

        public LoadMenuScreen() : base()
        {
            menuButtons.Add(new ButtonText( "Level 1", delegate() { MyApplication.GameWindow.CurrentScreen = new GameScreen(); },true));
            menuButtons.Add(new ButtonText( "Level 2", delegate () { }));
     //       menuButtons.Add(new MenuButton( "Level 3", delegate (MyGameWindow _myGameWindow) { }));
   //         menuButtons.Add(new MenuButton( "Level 4", delegate (MyGameWindow _myGameWindow) { }));
 //           menuButtons.Add(new MenuButton( "Level 5", delegate (MyGameWindow _myGameWindow) { }));
            menuButtons.Add(new ButtonText( "Back", delegate () { MyApplication.GameWindow.CurrentScreen = new MainMenuScreen(); }));

        }
    }
}
