using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.GameObject
{
    public class Exit : GameObject
    {
        private Vector2 nextRoomPosition;

        public Exit(){ }

        public Vector2 NextRoomPosition
        {
            get
            {
                return nextRoomPosition;
            }

            set
            {
                nextRoomPosition = value;
            }
        }
    }
}
