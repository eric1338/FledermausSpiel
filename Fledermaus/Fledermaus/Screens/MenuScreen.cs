using Fledermaus.Menu;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.Screens
{
	abstract class MenuScreen : Screen
	{
        protected readonly float buttonHeight = .15f;
        protected readonly float borderHeight = .2f;
        protected int indexActiveButton = 0;
        
        
        protected ObservableCollection<MenuButton> menuButtons = new ObservableCollection<MenuButton>();

        protected int ActiveButton
        {
            set
            {
                menuButtons[indexActiveButton].isSelected = false;
                // Jump from Last to First
                if (value >= menuButtons.Count)
                {
                    value = 0;
                    foreach (var button in menuButtons)
                        button.Translation = 0;
                }
                // Jump from First to Last
                else if (value < 0)
                {
                    value = menuButtons.Count - 1;
                    var yPos = getYPositionofButton(value, menuButtons.Count);
                    foreach (var button in menuButtons)
                        button.Translation = menuButtons.Count*buttonHeight;

                }

                //Select Button
                indexActiveButton = value;
                menuButtons[value].isSelected = true;

                
                // Translate all Buttons if the selected Button is outside the Screen
                var absPos = menuButtons[value].Position + menuButtons[value].Translation;

                if (absPos< -1+borderHeight) { 

                    foreach (var button in menuButtons) 
                        button.Translation += buttonHeight;
                }
                if (absPos > 1-borderHeight)
                {

                    foreach (var button in menuButtons)
                        button.Translation -= buttonHeight;
                }
            }
            get { return indexActiveButton; }
        }

        public MenuScreen(MyGameWindow win) : base(win) {
            _inputManager.AddSingleUserActionMapping(Key.Up, UserAction.MoveUp);
            _inputManager.AddSingleUserActionMapping(Key.Down, UserAction.MoveDown);
            _inputManager.AddSingleUserActionMapping(Key.Enter, UserAction.Confirm);

            menuButtons.CollectionChanged += MenuButtons_CollectionChanged;
        }

        private void MenuButtons_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var list = sender as ObservableCollection<MenuButton>;
            var count = list.Count;
            // set new Positions 
            for (int i = 0; i < count; i++)
                list[i].Position = getYPositionofButton(i, count);
        }
        /// <summary>
        /// Get the y Position of the Button by the index and count of Buttons in the List
        /// </summary>
        /// <param name="index">Index of the Button in the List</param>
        /// <param name="count">Elements in the List</param>
        /// <returns></returns>
        private float getYPositionofButton(int index, int count) {

            float firstPos = count / 2 * buttonHeight;
            var mod = (float)Math.Round(firstPos % buttonHeight, 5);
            if (mod > 0)
                firstPos += buttonHeight;

            while (firstPos > (1.0f - borderHeight))
            {
                var tmp = firstPos - buttonHeight;
                firstPos = (float)Math.Round(tmp, 5);
            }

            var yPos = (firstPos) - (index * buttonHeight);

            return (float)Math.Round(yPos,5);
        }

        public override void DoLogic()
        {
            ProcessSingleUserActions();
        }

        public override void Draw()
        {
            foreach (var mb in menuButtons)
                mb.draw();
        }

        private void ProcessSingleUserActions()
        {
            foreach (UserAction userAction in _inputManager.GetSingleUserActionsAsList())
            {
                if (userAction == UserAction.MoveUp)
                    ActiveButton--;
                else if (userAction == UserAction.MoveDown)
                    ActiveButton++;
                else if (userAction == UserAction.Confirm)
                    menuButtons[ActiveButton].doAction(_myGameWindow);
            }
        }
    }
}
