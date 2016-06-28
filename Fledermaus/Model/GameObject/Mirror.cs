using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.GameObject
{
    public class Mirror : MovingGameObject
    {
        public static readonly float MirrorLength = 0.15f;

        protected float initAngle;

        private Vector2 railStart;
        private Vector2 railEnd;

        private float startingRelativePosition;
        private float minRotation;
        private float maxRotation;



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

        public Vector2 RailEnd
        {
            get
            {
                return railEnd;
            }

            set
            {
                railEnd = value;
            }
        }

        public Vector2 RailStart
        {
            get
            {
                return railStart;
            }

            set
            {
                railStart = value;
            }
        }

        public float MaxRotation
        {
            get
            {
                return maxRotation;
            }

            set
            {
                maxRotation = value;
            }
        }

        public float MinRotation
        {
            get
            {
                return minRotation;
            }

            set
            {
                minRotation = value;
            }
        }

        public float StartingRelativePosition
        {
            get
            {
                return startingRelativePosition;
            }

            set
            {
                startingRelativePosition = value;
            }
        
    
        }

        public Mirror()
        {

        }
    }
}
