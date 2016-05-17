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
    class MainMenuScreen : MenuScreen
    {


        public MainMenuScreen(MyGameWindow win) : base(win)
        {
            menuButtons.Add(new MenuButton("Start Game", delegate(MyGameWindow _myGameWindow) { _myGameWindow.CurrentScreen = new StartMenuScreen(_myGameWindow); }, true));
            menuButtons.Add(new MenuButton("Load Game", delegate (MyGameWindow _myGameWindow) { _myGameWindow.CurrentScreen = new LoadMenuScreen(_myGameWindow); }));
            menuButtons.Add(new MenuButton("Highscore", delegate (MyGameWindow _myGameWindow) {  }));
            menuButtons.Add(new MenuButton("Level Editor", delegate (MyGameWindow _myGameWindow) { _myGameWindow.CurrentScreen = new LevelEditorScreen(_myGameWindow); }));
            menuButtons.Add(new MenuButton("Configuration", delegate (MyGameWindow _myGameWindow) { }));
            menuButtons.Add(new MenuButton("Credits", delegate (MyGameWindow _myGameWindow) { }));
            menuButtons.Add(new MenuButton("Exit", delegate (MyGameWindow _myGameWindow) { _myGameWindow.Exit(); }));
        }
    }
}
