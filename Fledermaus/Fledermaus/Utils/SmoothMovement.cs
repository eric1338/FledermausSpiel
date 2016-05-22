using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.Utils
{
	class SmoothMovement
	{

		private SmoothValueTransition _x;
		private SmoothValueTransition _y;
		private SmoothValueTransition _alpha;
		private SmoothValueTransition _scale;

		public SmoothMovement(Vector2 origin, Vector2 destiny) : this(origin, destiny, -1.0f, -1.0f)
		{

		}

		public SmoothMovement(Vector2 origin, Vector2 destiny, float originAlpha, float destinyAlpha) :
			this(origin, destiny, originAlpha, destinyAlpha, -1.0f, -1.0f)
		{

		}

		public SmoothMovement(Vector2 origin, Vector2 destiny, float originAlpha, float destinyAlpha, float originScale, float destinyScale)
		{
			_x = new SmoothValueTransition(origin.X, destiny.X);
			_y = new SmoothValueTransition(origin.Y, destiny.Y);
			_alpha = new SmoothValueTransition(originAlpha, destinyAlpha);
			_scale = new SmoothValueTransition(originScale, destinyScale);
		}

		public void Tick()
		{
			_x.Tick();
			_y.Tick();
			_alpha.Tick();
			_scale.Tick();
		}

		public Vector2 GetPosition()
		{
			return new Vector2(_x.CurrentValue, _y.CurrentValue);
		}

		public float GetAlpha()
		{
			return _alpha.CurrentValue;
		}

		public float GetScale()
		{
			return _scale.CurrentValue;
		}
	}
}
