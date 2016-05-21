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

namespace Fledermaus.Screens
{
	abstract class MenuScreen : Screen
	{

        private float padding;
        private bool showBorder;
        private float borderWidth;

        protected HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center;
        protected float minWidth;



        protected int indexActiveButton = 0;

        protected bool isActive = true;
        protected bool isSelected;


        protected ObservableCollection<Button> menuButtons = new ObservableCollection<Button>();
        public bool IsActive
        {
            set {
                
                isActive = value;
            }
            get { return isActive; }
        }
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
                    var yPos = getYPositionofButton(value);
                    var translation=getYPositionofButton(menuButtons.IndexOf(menuButtons.Last()));
                    if(translation<-1+Padding)
                    foreach (var button in menuButtons) { 
                        button.Translation = translation;
                        //translation += button.Height;
                    }

                }

                //Select Button
                indexActiveButton = value;
                menuButtons[value].isSelected = true;

                
                // Translate all Buttons if the selected Button is outside the Screen
                var absPos = menuButtons[value].Position + menuButtons[value].Translation;

                if (absPos< -1+ menuButtons[value].Height) { 

                    foreach (var button in menuButtons) 
                        button.Translation += button.Height;
                }
                if (absPos > 1- menuButtons[value].Height)
                {

                    foreach (var button in menuButtons)
                        button.Translation -= button.Height;
                }
            }
            get { return indexActiveButton; }
        }
        public HorizontalAlignment HorizontalAlignment
        {
            set { horizontalAlignment = value; }
            get{ return horizontalAlignment; }
        }

        protected float MinWidth
        {
            get
            {
                return minWidth;
            }
        }

        public float Padding
        {
            get
            {
                return padding;
            }

            set
            {
                padding = value;
            }
        }

        public bool ShowBorder
        {
            get
            {
                return showBorder;
            }

            set
            {
                showBorder = value;
            }
        }

        public float BorderWidth
        {
            get
            {
                return BorderWidth1;
            }

            set
            {
                BorderWidth1 = value;
            }
        }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                isSelected = value;
            }
        }

        public float BorderWidth1
        {
            get
            {
                return borderWidth;
            }

            set
            {
                borderWidth = value;
            }
        }

        public MenuScreen() : base() {
            Padding = .1f;
            BorderWidth = .02f;
            InputManager.Clear();
            InputManager.AddSingleUserActionMapping(Key.Up, UserAction.MoveUp);
            InputManager.AddSingleUserActionMapping(Key.Down, UserAction.MoveDown);
            InputManager.AddSingleUserActionMapping(Key.Enter, UserAction.Confirm);
            InputManager.AddSingleUserActionMapping(Key.Escape, UserAction.Cancel);

            menuButtons.CollectionChanged += MenuButtons_CollectionChanged;
        }

        private void MenuButtons_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var list = sender as ObservableCollection<Button>;
            var count = list.Count;
            // set new Positions 
            for (int i = 0; i < count; i++)
                list[i].Position = getYPositionofButton(i);
            if(e.NewItems[0]!=null)
                if (minWidth < ((Button)e.NewItems[0]).Width)
                    minWidth = ((Button)e.NewItems[0]).Width;
        }
        /// <summary>
        /// Get the y Position of the Button by the index and count of Buttons in the List
        /// </summary>
        /// <param name="index">Index of the Button in the List</param>
        /// <param name="count">Elements in the List</param>
        /// <returns></returns>
        private float getYPositionofButton(int index) {

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

            return Konfiguration.Round(yPos);

            /*
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

                        return (float)Math.Round(yPos,5); */
        }

        public override void DoLogic()
        {
            ProcessSingleUserActions();
        }

        public override void Draw()
        {
            if (ShowBorder)
            {
                DrawBorder();
            }
            float menuCenter = .0f; 
            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Left: menuCenter = (int)horizontalAlignment - 1 + BorderWidth+Padding + MinWidth / 2 ; break;
                case HorizontalAlignment.Center: menuCenter = .0f; break;
                case HorizontalAlignment.Right: menuCenter = (int)horizontalAlignment - 1 - MinWidth / 2; break;
                case HorizontalAlignment.Stretch:  break;
            }
            foreach (var mb in menuButtons)
                mb.Draw(menuCenter);

        }

        private void DrawBorder()
        {

            if (IsSelected)
                GL.Color3(Color.Yellow);
            else
                GL.Color3(Color.LightGray);

            GL.Begin(PrimitiveType.Quads);
            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    GL.Vertex2((int)horizontalAlignment - 1.0f                               ,  BorderWidth + Padding + menuButtons.First().Position + (menuButtons.First().Height / 2));
                    GL.Vertex2((int)horizontalAlignment - 1.0f                               , -BorderWidth - Padding + menuButtons.Last().Position - (menuButtons.Last().Height / 2));
                    GL.Vertex2((int)horizontalAlignment - 1.0f + Padding + minWidth + Padding + 2*BorderWidth , -BorderWidth  -Padding+ menuButtons.Last().Position- (menuButtons.Last().Height / 2));
                    GL.Vertex2((int)horizontalAlignment - 1.0f + Padding + minWidth + Padding + 2*BorderWidth , BorderWidth +Padding + menuButtons.First().Position + (menuButtons.First().Height / 2));
                    break;
                case HorizontalAlignment.Center:
                    GL.Vertex2((int)horizontalAlignment - 1.0f - minWidth / 2 - Padding - BorderWidth, BorderWidth + menuButtons.First().Position + (menuButtons.First().Height / 2));
                    GL.Vertex2((int)horizontalAlignment - 1.0f - minWidth / 2 - Padding - BorderWidth, -BorderWidth + menuButtons.Last().Position - (menuButtons.Last().Height / 2));
                    GL.Vertex2((int)horizontalAlignment - 1.0f + minWidth / 2 + Padding + BorderWidth, -BorderWidth + menuButtons.Last().Position - (menuButtons.Last().Height / 2));
                    GL.Vertex2((int)horizontalAlignment - 1.0f + minWidth / 2 + Padding + BorderWidth, BorderWidth + menuButtons.First().Position + (menuButtons.First().Height / 2));
                    break;
            }
            

            GL.End();
            GL.Color3(Color.Black);
            GL.Begin(PrimitiveType.Quads);
            
            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    GL.Vertex2((int)horizontalAlignment - 1.0f + BorderWidth  , Padding +menuButtons.First().Position + (menuButtons.First().Height / 2));
                    GL.Vertex2((int)horizontalAlignment - 1.0f + BorderWidth  , -Padding +menuButtons.Last().Position - (menuButtons.Last().Height / 2));
                    GL.Vertex2((int)horizontalAlignment - 1.0f + BorderWidth + Padding + minWidth + Padding, -Padding +menuButtons.Last().Position - (menuButtons.Last().Height / 2));
                    GL.Vertex2((int)horizontalAlignment - 1.0f + BorderWidth + Padding + minWidth + Padding, Padding +menuButtons.First().Position + (menuButtons.First().Height / 2));
                    break;
                case HorizontalAlignment.Center:
                    GL.Vertex2((int)horizontalAlignment - 1.0f - minWidth / 2 - Padding,  menuButtons.First().Position + (menuButtons.First().Height / 2));
                    GL.Vertex2((int)horizontalAlignment - 1.0f - minWidth / 2 - Padding,  menuButtons.Last().Position - (menuButtons.Last().Height / 2));
                    GL.Vertex2((int)horizontalAlignment - 1.0f + minWidth / 2 + Padding,  menuButtons.Last().Position - (menuButtons.Last().Height / 2));
                    GL.Vertex2((int)horizontalAlignment - 1.0f + minWidth / 2 + Padding,   menuButtons.First().Position + (menuButtons.First().Height / 2));
                    break;
            }
            GL.End();
        }

        protected virtual void ProcessSingleUserActions()
        {
            foreach (UserAction userAction in InputManager.GetSingleUserActionsAsList())
            {
                if (userAction == UserAction.MoveUp)
                    ActiveButton--;
                else if (userAction == UserAction.MoveDown)
                    ActiveButton++;
                else if (userAction == UserAction.Confirm)
                    menuButtons[ActiveButton].doAction();
                else if (userAction == UserAction.Cancel)
                    MyApplication.GameWindow.CurrentScreen = new MainMenuScreen();
                    
            }
        }
    }
}
