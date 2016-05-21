using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.Screens
{
    public abstract class Button
    {
        
        

        public delegate void DoAction();
        public DoAction doAction;



        public float Width { get; set; }
        public float Height { get; set; }

        public float Position { get; set; }
        public float Translation { get; set; }

        public bool isSelected { get; set; }

        public Button(DoAction doAction, bool selected = false)
        {
            this.doAction = doAction;
            isSelected = selected;

        }

        public abstract void Draw(float center);
    }

}
