using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fledermaus.Screens
{
	public abstract class Screen
	{
		protected InputManager _inputManager = new InputManager();

        private float height;
        private float width;
        private Vector2 position;
        protected bool showBorder;



        protected Vector2 center;
        private float scale;

        protected float marging;
        protected float padding;
       // protected bool showBorder;
        protected float borderWidth;
        protected HorizontalAlignment horizontalAlignment;// = HorizontalAlignment.Center;
        protected float contenWidth;
        protected float contentHeight;
        protected float maxHeight;
        protected float maxWidth;

        protected bool isSelected;

        public virtual HorizontalAlignment HorizontalAlignment
        {
            set
            {
                horizontalAlignment = value;
                if (value == HorizontalAlignment.Left)
                    Center = new Vector2(-1 + padding + borderWidth + ContentWidth / 2, Center.Y);

            }
            get { return horizontalAlignment; }
        }

        internal void ProcessMouseWheel(MouseWheelEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public virtual float ContentWidth
        {
            get
            {
                return contenWidth;
            }
            set
            {
                contenWidth = value;
            }
        }
        public float ContentHeight
        {
            get
            {
                return contentHeight;
            }
            set
            {
                contentHeight = value;
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
                return borderWidth;
            }

            set
            {
                borderWidth = value;
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

        public float MaxWidth
        {
            get
            {
                return borderWidth + Padding + ContentWidth + padding + borderWidth;
            }
        }

        public float MaxHeight
        {
            get
            {
                return maxHeight;
            }

            set
            {
                maxHeight = value;
            }
        }

        protected float Marging
        {
            get
            {
                return marging;
            }

            set
            {
                marging = value;
            }
        }

        public Vector2 Center
        {
            get
            {
                return center;
            }

            set
            {
                center = value;
            }
        }

        public float Scale
        {
            get
            {
                return scale;
            }

            set
            {
                scale = value;
            }
        }

        protected float Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        protected float Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        protected Vector2 Position
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

        public Screen()
        {
            Width = .2f;
            height = .2f;
            Position = new Vector2(.0f, .0f);
            //horizontalAlignment = HorizontalAlignment.Center;
            Center = new Vector2(.0f, .0f);
            Scale = 1.0f;
            Padding = .03f;
        }
        public Screen(float width, float height, Vector2 position)
        {
            Width = width;
            Height = height;
            Position = position;
        }

        public virtual bool isPointInScreen(System.Drawing.Point point)
        {
            var ret = false;

            Vector2 relPos = new Vector2((point.X / (float)MyApplication.GameWindow.Width) * 2.0f - 1.0f,
                  ((point.Y / (float)MyApplication.GameWindow.Height) * 2.0f - 1.0f) * -1);
            Position = center;
            var left = Scale*( Position.X - Width / 2.0f);
            var right = Scale*(Position.X + Width / 2.0f);
            var top = Scale*(Position.Y + Height / 2);
            var bottm = Scale * (Position.Y - Height / 2);
            if (relPos.X > left && relPos.X < right)
            {

                if (relPos.Y < top && relPos.Y > bottm)
                {
                    ret = true;
                }
            }

            return ret;
        }

        public abstract void DoLogic();
        public virtual void Draw()
        {
            if (ShowBorder)
            {
                DrawBorder();
            }
        }

        protected virtual void DrawBorder()
        {

            if (IsSelected)
                GL.Color3(Color.Yellow);
            else
                GL.Color3(Color.LightGray);

            GL.Begin(PrimitiveType.Quads);

            GL.Vertex2( (Center.X - MaxWidth / 2) , (Center.Y + MaxHeight / 2));
            GL.Vertex2( (Center.X - MaxWidth / 2) , (Center.Y - MaxHeight / 2));
            GL.Vertex2( (Center.X + MaxWidth / 2), (Center.Y - MaxHeight / 2));
            GL.Vertex2( (Center.X + MaxWidth / 2), (Center.Y + MaxHeight / 2));

            GL.End();
            GL.Color3(Color.Black);
            GL.Begin(PrimitiveType.Quads);

            GL.Vertex2( (Center.X - MaxWidth / 2 + borderWidth),  (Center.Y + MaxHeight / 2 - borderWidth));
            GL.Vertex2( (Center.X - MaxWidth / 2 + borderWidth),  (Center.Y - MaxHeight / 2 + borderWidth));
            GL.Vertex2( (Center.X + MaxWidth / 2 - borderWidth), (Center.Y - MaxHeight / 2 + borderWidth));
            GL.Vertex2( (Center.X + MaxWidth / 2 - borderWidth),  (Center.Y + MaxHeight / 2 - borderWidth));

            GL.End();
        }


        public void ProcessKeyUp(Key key)
        {
            _inputManager.ProcessKeyUp(key);
        }

        public virtual void ProcessKeyDown(Key key)
        {
            _inputManager.ProcessKeyDown(key);
        }
        public virtual void ProcessMouseMove(MouseMoveEventArgs e)
        {

            //_inputManager.ProcessKeyDown(button);
        }
        public virtual void ProcessMouseButtonDown(MouseButtonEventArgs e)
        {

            //_inputManager.ProcessKeyDown(button);
        }


		protected void SwitchToScreen(Screen screen)
		{
			MyApplication.GameWindow.CurrentScreen = screen;
		}


    }
}
