using Fledermaus.Utils;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
    public class NPC : ILogicalNPC
	{

		private Vector2 _initialPosition;

		public Vector2 Position;

		private float _speed = 0.002f;

		private Vector2 _firstVector;
		private Vector2 _secondVector;

		private float _ticksLeft;
		private float _ticksPerVector = 60;

		public Vector2 PointOfInterest { get; set; }

		public float Affection = 0;

		public NPC(Vector2 initialPosition)
		{
			_initialPosition = initialPosition;
			Position = _initialPosition;

			_firstVector = GetRandomVector();
			_secondVector = GetRandomVector();

			_ticksLeft = _ticksPerVector;

			PointOfInterest = new Vector2(0.0f, 0.0f);
		}

		public void Tick()
		{
			_ticksLeft--;

			if (_ticksLeft <= 0)
			{
				_firstVector = _secondVector;
				_secondVector = GetRandomVector();

				_ticksLeft = _ticksPerVector;
			}
		}

		public void Move()
		{
			Position += GetMovementVector();
		}

		public void TickAndMove()
		{
			Tick();
			Move();
		}

		public Vector2 GetRandomVector()
		{
			Vector2 vector = new Vector2(GetWeightedRandomCoordinate(true), GetWeightedRandomCoordinate(false));

			vector.Normalize();

			return vector;
		}

		private float GetWeightedRandomCoordinate(bool x)
		{
			bool acceptCoordinate = false;
			float randomCoordinate = 0.0f;

			float positionCoordinate = x ? Position.X : Position.Y;

			while (!acceptCoordinate)
			{
				randomCoordinate = GetRandomCoordinate();

				float distance = Math.Abs(positionCoordinate - randomCoordinate);

				bool differentSigns = Math.Sign(randomCoordinate) != Math.Sign(positionCoordinate);

				acceptCoordinate = differentSigns || (Util.GetRandomFloat() * 2.5f) < distance;
			}

			return randomCoordinate;
		}

		private float GetRandomCoordinate()
		{
			return Util.GetRandomFloat() * 2.0f - 1.0f;
		}

		private Vector2 GetRandomMovementVector()
		{
			return (_firstVector * _ticksLeft) + (_secondVector * (_ticksPerVector - _ticksLeft));
		}

		private Vector2 GetVectorToPointOfInterest()
		{
			Vector2 vector = PointOfInterest - Position;

			vector.Normalize();

			return vector;
		}

		public Vector2 GetMovementVector()
		{
			Vector2 movementVector = GetRandomMovementVector() + Affection * GetVectorToPointOfInterest();

			movementVector.Normalize();

			return movementVector * _speed;
		}
		
		public IEnumerable<Line> GetLines()
		{
			Vector2 v1 = GetTempVector(1, 1);
			Vector2 v2 = GetTempVector(1, -1);
			Vector2 v3 = GetTempVector(-1, -1);
			Vector2 v4 = GetTempVector(-1, 1);

			List<Line> lines = new List<Line>();
			lines.Add(new Line(v1, v2));
			lines.Add(new Line(v2, v3));
			lines.Add(new Line(v3, v4));
			lines.Add(new Line(v1, v4));

			return lines;
		}

		private Vector2 GetTempVector(float xF, float yF)
		{
			return Position + new Vector2(0.03f * xF, 0.03f * yF);
		}

		public void Reset()
		{
			Position = _initialPosition;
		}


	}
}
