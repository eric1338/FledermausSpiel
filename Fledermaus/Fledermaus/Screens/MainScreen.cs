using Fledermaus.Menu;
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
    class MainScreen : Screen
    {

        private int indexActiveButton;
        List<MenuButton> menuButtons = new List<MenuButton>();

        int IndexActiveButton {
            set {
                menuButtons[value].isSelected = false;
                indexActiveButton = value;
                menuButtons[value].isSelected = true;
            } get { return indexActiveButton; }
        }

        public MainScreen(MyGameWindow win) : base(win)
        {
            _inputManager.AddSingleUserActionMapping(Key.Up, UserAction.MoveUp);
            _inputManager.AddSingleUserActionMapping(Key.Down, UserAction.MoveDown);
            _inputManager.AddSingleUserActionMapping(Key.Enter, UserAction.Confirm);

            menuButtons.Add(new MenuButton(0.8f));
            menuButtons.Add(new MenuButton(0.6f));
            menuButtons.Add(new MenuButton(0.4f));

            IndexActiveButton = 0;
        }
        public override void DoLogic()
        {
            ProcessSingleUserActions();
        }

        public override void Draw()
        {
            foreach (var mb in menuButtons)
                mb.draw();
                
            System.Diagnostics.Debug.WriteLine("" + _myGameWindow.Height);
            System.Diagnostics.Debug.WriteLine("MainScreen Draw");

        }

        private void ProcessSingleUserActions()
        {
            foreach (UserAction userAction in _inputManager.GetSingleUserActionsAsList())
            {
                if (userAction == UserAction.MoveUp)
                {
                    if (IndexActiveButton < menuButtons.Count-1)
                        IndexActiveButton++;
                }
                else if (userAction == UserAction.MoveDown)
                {
                    if (IndexActiveButton > 0)
                        IndexActiveButton--;
                }
                else if (userAction == UserAction.Confirm)
                {
                    if (IndexActiveButton == menuButtons.Count - 1) {
                        _myGameWindow.CurrentScreen = new GameScreen(_myGameWindow);
                    }
                }
            }
        }
    }
}
