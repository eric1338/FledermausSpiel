using System;
using System.Collections.Generic;
using System.Text;

namespace Model.GameObject
{
    public class Mirror : MovingGameObject
    {
        protected float initAngle;

        public float InitAngle
        {
            get
            {
                return initAngle;
            }

            set
            {
                initAngle = value;
            }
        }

        public Mirror()
        {

        }
    }
}
