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


        public MainMenuScreen() : base()
        {
            menuButtons.Add(new ButtonText("Start Game", delegate() { MyApplication.GameWindow.CurrentScreen = new StartMenuScreen(); }, true));
            menuButtons.Add(new ButtonText("Load Game", delegate () { MyApplication.GameWindow.CurrentScreen = new LoadMenuScreen(); }));
            menuButtons.Add(new ButtonText("Highscore", delegate () {  }));
            menuButtons.Add(new ButtonText("Level Editor", delegate () { MyApplication.GameWindow.CurrentScreen = new LevelEditorScreen(); }));
            menuButtons.Add(new ButtonText("Configuration", delegate () { }));
            menuButtons.Add(new ButtonText("Credits", delegate () { }));
            menuButtons.Add(new ButtonText("Exit", delegate () { MyApplication.Exit(); }));
        }
    }
}
