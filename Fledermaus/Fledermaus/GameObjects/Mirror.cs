using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fledermaus.Utils;

namespace Fledermaus.GameObjects
{
    public class Mirror : ILogicalMirror
	{
		
		public float StartingAngle = 20.0f;

		public float MirrorLength = 0.2f;

		public float RelativePosition { get; set; }
		public float Rotation { get; set; }

		public float MaximumRotation { get; set; }

		public Vector2 RailPosition1 { get; set; }
		public Vector2 RailPosition2 { get; set; }

		public bool IsAccessible { get; set; }

		public Mirror(Vector2 railPosition1, Vector2 railPosition2)
		{
			if (railPosition1.X < railPosition2.X)
			{
				RailPosition1 = railPosition1;
				RailPosition2 = railPosition2;
			}
			else
			{
				RailPosition1 = railPosition2;
				RailPosition2 = railPosition1;
			}

			RelativePosition = 0.5f;
			Rotation = Util.ConvertDegreeToRadian(StartingAngle);
			MaximumRotation = Util.ConvertDegreeToRadian(40.0f);
		}

		private Vector2 GetRailVector()
		{
			return RailPosition2 - RailPosition1;
		}

		private Vector2 GetNormalizedRailVector()
		{
			Vector2 railVector = GetRailVector();
			railVector.Normalize();
			return railVector;
		}

		private Vector2 GetCenterPosition()
		{
			return GetRelativeRailPosition(0.5f);
		}

		public Vector2 GetMirrorPosition()
		{
			return GetRelativeRailPosition(RelativePosition);
		}

		public Vector2 GetMirrorNormal1()
		{
			Vector2 normal = Util.GetOrthogonalVectorCW(GetMirrorLine().GetDirectionVector());

			normal.Normalize();

			return normal;
		}

		public Vector2 GetMirrorNormal2()
		{
			Vector2 normal = Util.GetOrthogonalVectorCCW(GetMirrorLine().GetDirectionVector());

			normal.Normalize();

			return normal;
		}

		private Vector2 GetMirrorHalf()
		{
			return Util.GetRotatedVector(GetNormalizedRailVector(), Rotation) * MirrorLength;
		}

		public Line GetMirrorLine()
		{
			return new Line(GetMirrorPosition1(), GetMirrorPosition2());
		}

		private Vector2 GetMirrorPosition1()
		{
			return GetMirrorPosition() + GetMirrorHalf();
		}

		private Vector2 GetMirrorPosition2()
		{
			return GetMirrorPosition() - GetMirrorHalf();
		}

		private Vector2 GetRelativeRailPosition(float factor)
		{
			return RailPosition1 + GetRailVector() * factor;
		}

		// TODO: Up/Down oder Left/Right? (evtl abhängig vom RailVector (x > y bzw. y > x)
		public void MoveMirrorUp(float deltaDistance)
		{
			RelativePosition = Math.Max(0.06f, RelativePosition - deltaDistance);
		}

		public void MoveMirrorDown(float deltaDistance)
		{
			RelativePosition = Math.Min(0.94f, RelativePosition + deltaDistance);
		}

		public void RotateCW(float deltaAngle)
		{
			Rotation = Math.Min(MaximumRotation, Rotation + deltaAngle);
		}

		public void RotateCCW(float deltaAngle)
		{
			Rotation = Math.Max(-MaximumRotation, Rotation - deltaAngle);
		}

		public void Reset()
		{
			RelativePosition = 0.5f;
			Rotation = Util.ConvertDegreeToRadian(StartingAngle);
		}

		public IEnumerable<Line> GetLines()
		{
			return new List<Line> { GetMirrorLine() };
		}
	}
}
