using Fledermaus.Screens;
using Model;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using OpenTK;

namespace Fledermaus.Screens
{
    abstract class MenuScreen : Screen
    {


        protected int indexActiveButton = 0;

        protected bool isActive = true;


        protected ObservableCollection<Button> menuButtons = new ObservableCollection<Button>();
        public bool IsActive
        {
            set
            {

                isActive = value;
            }
            get { return isActive; }
        }
        protected int ActiveButton
        {
            set
            {
                menuButtons[indexActiveButton].isSelected = false;
                // Index from Last to First
                if (value >= menuButtons.Count)
                    value = 0;

                // Index from First to Last
                else if (value < 0)
                    value = menuButtons.Count - 1;

                //Select Button
                indexActiveButton = value;
                menuButtons[value].isSelected = true;


                // Translate all Buttons if the selected Button is outside the Screen
                var absPosY = menuButtons[value].Position.Y;
                var tmpTranslation = .0f;

                var padding = menuButtons[value].Height + Padding;
                if (absPosY < -1 + padding)
                    for (int i = 0; menuButtons[value].Position.Y + tmpTranslation < -1 + padding; i++)
                        tmpTranslation += menuButtons[i].Height;

                else if (absPosY > 1 - padding)
                    for (int i = menuButtons.Count - 1; menuButtons[value].Position.Y + tmpTranslation > 1 - padding; i--)
                        tmpTranslation -= menuButtons[i].Height;

                if (tmpTranslation != .0f)
                    foreach (var button in menuButtons)
                        button.Position += new Vector2(.0f, tmpTranslation);
            }
            get { return indexActiveButton; }
        }
        public override HorizontalAlignment HorizontalAlignment
        {
            set
            {
                var val = horizontalAlignment;

                base.HorizontalAlignment = value;
                foreach (var button in menuButtons)
                    button.Position = new Vector2(Center.X, button.Position.Y);
            }
            get { return horizontalAlignment; }
        }

        public MenuScreen() : base()
        {


            //inputManager.Clear();
            _inputManager.AddSingleUserActionMapping(Key.Up, UserAction.MoveUp);
            _inputManager.AddSingleUserActionMapping(Key.Down, UserAction.MoveDown);
            _inputManager.AddSingleUserActionMapping(Key.Enter, UserAction.Confirm);
            _inputManager.AddSingleUserActionMapping(Key.Escape, UserAction.OpenGameMenu);

            menuButtons.CollectionChanged += MenuButtons_CollectionChanged;
        }



        private void MenuButtons_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var list = sender as ObservableCollection<Button>;
            var count = list.Count;



            var widthTotal = .0f;
            var heightTotal = .0f;
            foreach (var button in menuButtons)
            {

                heightTotal += button.Height;

                if (button.Width > widthTotal)
                    widthTotal = button.Width;
            }
            contenWidth = widthTotal;
            contentHeight = heightTotal;

            setInitPositionOfButtons();

        }

        private void setInitPositionOfButtons()
        {
            var count = menuButtons.Count;
            var height = .0f;
            // set new Positions 


            for (int i = 0; i < count; i++)
            {
                menuButtons[i].Position = new Vector2(Center.X, Center.Y - height + menuButtons[i].Height / 2);//getPositionofButton(i);
                height += menuButtons[i].Height;
            }

            var tmpTranslation = contentHeight / 2;
            while (menuButtons.First().Position.Y + menuButtons.First().Height / 2 + tmpTranslation > 1 - Padding)
                tmpTranslation -= menuButtons.First().Height;

            foreach (var button in menuButtons)
                button.Position += new Vector2(.0f, tmpTranslation);
        }
        /// <summary>
        /// Get the y Position of the Button by the index and count of Buttons in the List
        /// </summary>
        /// <param name="index">Index of the Button in the List</param>
        /// <param name="count">Elements in the List</param>
        /// <returns></returns>
/*        private Vector2 getPositionofButton(int index) {

            var heightTotal = .0f;
            foreach (var button in menuButtons)
                heightTotal += button.Height;

            float firstPos = heightTotal/2;
            for (int i=0; firstPos > (1.0f - BorderWidth-Padding);i++)
            {
                var tmp = firstPos - menuButtons[i].Height;
                firstPos = (float)Math.Round(tmp, 5);
            }
            var yPos = firstPos;
            for (int i = 0; i <= index; i++)
                yPos -= menuButtons[i].Height;

            return new Vector2(this.Center.X,Konfiguration.Round(yPos));

        }
        */

        public override void DoLogic()
        {
            ProcessSingleUserActions();
        }

        public override void Draw()
        {
            base.Draw();
            /*
                        float menuCenter = .0f; 
                        switch (horizontalAlignment)
                        {
                            case HorizontalAlignment.Left: menuCenter = (int)horizontalAlignment - 1 + BorderWidth+Padding + ContentWidth / 2 ; break;
                            case HorizontalAlignment.Center: menuCenter = .0f; break;
                            case HorizontalAlignment.Right: menuCenter = (int)horizontalAlignment - 1 - ContentWidth / 2; break;
                            case HorizontalAlignment.Stretch:  break;
                        }*/
            foreach (var mb in menuButtons)
                mb.Draw();

        }


        protected virtual void ProcessSingleUserActions()
        {
            foreach (UserAction userAction in _inputManager.GetSingleUserActionsAsList())
            {
                if (userAction == UserAction.MoveUp)
                    ActiveButton--;
                else if (userAction == UserAction.MoveDown)
                    ActiveButton++;
                else if (userAction == UserAction.Confirm)
                    menuButtons[ActiveButton].doAction();
                else if (userAction == UserAction.OpenGameMenu)
                    MyApplication.GameWindow.CurrentScreen = new MainMenuScreen();

            }
        }
    }
}
