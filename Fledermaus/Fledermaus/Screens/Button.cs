using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.Screens
{
    public abstract class Button
    {

        //      private Vector2 translation;

        public delegate void DoAction();
        public DoAction doAction;




        public float Width { get; set; }
        public float Height { get; set; }

        public Vector2 Position { get; set; }
        //public Vector2 Translation { get; set; }



        public bool isSelected { get; set; }

        /*       public Vector2 AbsolutePosition
               {
                   get { return Position + Translation; }
               }*/

        /*        public Vector2 Translation
                {
                    get
                    {
                        return translation;
                    }

                    set
                    {
                        translation = value;
                    }
                }*/

        public Button(DoAction doAction, bool selected = false)
        {
            MyApplication.GameWindow.MouseMove += Mouse_Move;

            this.doAction = doAction;
            isSelected = selected;

        }
        ~Button()
        {
            MyApplication.GameWindow.MouseMove -= Mouse_Move;
        }

        private void Mouse_Move(object sender, MouseMoveEventArgs e)
        {

            Vector2 relPos = new Vector2((e.Mouse.X / (float)MyApplication.GameWindow.Width) * 2.0f - 1.0f,
                                          ((e.Mouse.Y / (float)MyApplication.GameWindow.Height) * 2.0f - 1.0f)) * -1;

            //      System.Diagnostics.Debug.WriteLine("Mouse X: " + e.Mouse.X + "\t" + relPos.X);
            //      System.Diagnostics.Debug.WriteLine("Mouse Y: " + e.Mouse.Y + "\t" + relPos.Y);
            /*
                        if (relPos.X > this.center.X - contenWidth / 2 && relPos.X < this.center.X + contenWidth / 2)
                        {
                            System.Diagnostics.Debug.WriteLine("Inside Menu X");
                            if (relPos.Y > this.center.Y - contentHeight / 2 && relPos.X < this.center.X + contenWidth / 2)
                            {
                            }

                        }*/
        }



        public abstract void Draw();
    }

}
