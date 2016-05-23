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
	abstract class Screen
	{
		protected InputManager _inputManager = new InputManager();

        protected Vector2 center;
        private float scale;
        protected float marging;
        protected float padding;
        protected bool showBorder;
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

        public Screen()
        {

            horizontalAlignment = HorizontalAlignment.Center;
            Center = new Vector2(.0f, .0f);
            Scale = 1.0f;
            Padding = .03f;
            BorderWidth = 0.01f;
            /*          Padding = .1f;
                      Marging = .1f;
                      BorderWidth = .01f;*/
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

            GL.Vertex2(Center.X - MaxWidth / 2, (Center.Y + MaxHeight / 2));
            GL.Vertex2(Center.X - MaxWidth / 2, (Center.Y - MaxHeight / 2));
            GL.Vertex2(Center.X + MaxWidth / 2, (Center.Y - MaxHeight / 2));
            GL.Vertex2(Center.X + MaxWidth / 2, (Center.Y + MaxHeight / 2));

            GL.End();
            GL.Color3(Color.Black);
            GL.Begin(PrimitiveType.Quads);

            GL.Vertex2((Center.X - MaxWidth / 2 + borderWidth), (Center.Y + MaxHeight / 2 - borderWidth));
            GL.Vertex2((Center.X - MaxWidth / 2 + borderWidth), (Center.Y - MaxHeight / 2 + borderWidth));
            GL.Vertex2((Center.X + MaxWidth / 2 - borderWidth), (Center.Y - MaxHeight / 2 + borderWidth));
            GL.Vertex2((Center.X + MaxWidth / 2 - borderWidth), (Center.Y + MaxHeight / 2 - borderWidth));

            GL.End();
        }

        public void ProcessKeyUp(Key key)
        {
            _inputManager.ProcessKeyUp(key);
        }

        public void ProcessKeyDown(Key key)
        {
            _inputManager.ProcessKeyDown(key);
        }


    }
}
