
using OpenTK;
using System.Collections.Generic;
//using System.Windows;

namespace Model.GameObject
{
    public class GameObject
    {
        protected Vector2 position;

        protected List<Vector2> relativeBounds;



        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public List<Vector2> RelativeBounds
        {
            get
            {
                return relativeBounds;
            }

            set
            {
                relativeBounds = value;
            }
        }


        public GameObject()
        {

        }
    }
}
