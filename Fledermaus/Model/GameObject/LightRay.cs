using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Model.GameObject
{
    public class LightRay : GameObject
    {
        protected Vector2 rayDirection;

        public Vector2 RayDirection
        {
            get
            {
                return rayDirection;
            }

            set
            {
                rayDirection = value;
            }
        }
    }
}
