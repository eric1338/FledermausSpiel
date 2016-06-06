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
           

            this.doAction = doAction;
            isSelected = selected;

        }
        ~Button()
        {
           // MyApplication.GameWindow.MouseMove -= Mouse_Move;
        }




        public abstract void Draw();
    }

}
