using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.Utils
{
	class SmoothValueTransition
	{

		private float _originValue;
		private float _destinyValue;

		public float CurrentValue { get; private set; }
		public bool IsFinished { get; private set; }

		private float _deltaValue;

		private float _increaseFactor = 1.04f;

		private bool _speedUp = true;

		public SmoothValueTransition(float origin, float destiny)
		{
			_originValue = origin;
			_destinyValue = destiny;

			CurrentValue = _originValue;

			_deltaValue = (_destinyValue - _originValue) * 0.001f;

			IsFinished = false;
		}

		public void Tick()
		{
			if (IsFinished) return;

			CheckThreshold();

			if (_speedUp) _deltaValue *= _increaseFactor;
			else _deltaValue /= _increaseFactor;

			float newValue = CurrentValue + _deltaValue;

			if (Math.Abs(_destinyValue - _originValue) < Math.Abs(newValue - _originValue))
			{
				CurrentValue = _destinyValue;
				IsFinished = true;
			}
			else
			{
				CurrentValue = newValue;
			}
		}

		private void CheckThreshold()
		{
			if (_speedUp && Math.Abs(_destinyValue - _originValue) <= Math.Abs(CurrentValue - _originValue) * 2)
			{
				_speedUp = false;
			}
		}

		public float TickAndGetCurrentValue()
		{
			Tick();
			return CurrentValue;
		}
	}
}
