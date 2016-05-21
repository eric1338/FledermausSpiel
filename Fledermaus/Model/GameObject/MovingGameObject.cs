
using OpenTK;


namespace Model.GameObject
{
    public class MovingGameObject : GameObject
    {
        protected Vector2 initPosition;
 
        public Vector2 InitialPosition
        {
            get
            {
                return initPosition;
            }

            set
            {
                initPosition = value;
            }
        }

        public MovingGameObject()
        {

        }
    }
}
