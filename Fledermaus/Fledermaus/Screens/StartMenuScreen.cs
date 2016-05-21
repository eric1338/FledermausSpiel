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
    class StartMenuScreen : MenuScreen
    {


        public StartMenuScreen() : base()
        {
            menuButtons.Add(new ButtonText( "Test Level", delegate () { MyApplication.GameWindow.CurrentScreen = new GameScreen(); GameLogic.Level = Levels.CreateTestLevel();  },true));
            menuButtons.Add(new ButtonText( "Level 2", delegate () { }));
            menuButtons.Add(new ButtonText( "Level 3", delegate () { }));
            menuButtons.Add(new ButtonText( "Level 4", delegate () { }));
            menuButtons.Add(new ButtonText( "Level 5", delegate () { }));
            menuButtons.Add(new ButtonText( "Back", delegate () { MyApplication.GameWindow.CurrentScreen = new MainMenuScreen(); }));
        }
    }
}
