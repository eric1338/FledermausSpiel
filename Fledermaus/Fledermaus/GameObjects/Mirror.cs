using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fledermaus.Utils;

namespace Fledermaus.GameObjects
{
	class Mirror : GameObject
	{

		// TODO: bool FaceUpwards?

		// TODO: evtl auslagern
		public const float MirrorMovementSpeed = 0.02f;
		public const float RotationSpeed = 0.006f;
		public const float PlayerMirrorDistance = 0.1f;
		public const float MinAccessibilityDistance = 0.25f;
		public float StartingAngle = 20.0f;

		public float MirrorLength = 0.2f;

		public float RelativePosition { get; set; }
		public float Rotation { get; set; }

		public float MaximumRotation { get; set; }

		public Vector2 RailPosition1 { get; set; }
		public Vector2 RailPosition2 { get; set; }

		public bool IsPlayerBelow { get; set; }

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

			IsPlayerBelow = (RailPosition1.Y + RailPosition2.Y) < 0;
		}

		public Vector2 GetRelativePlayerPosition()
		{
			Vector2 distanceVector = GetNormalizedRailVector() * PlayerMirrorDistance;
			
			Vector2 orthogonal = IsPlayerBelow ?
				Util.GetOrthogonalVectorCW(distanceVector) : Util.GetOrthogonalVectorCCW(distanceVector);

			return GetMirrorCenterPosition() + orthogonal;
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

		public Vector2 GetMirrorCenterPosition()
		{
			return GetRelativeRailPosition(RelativePosition);
		}

		private Vector2 GetMirrorHalf()
		{
			return Util.GetRotatedVector(GetNormalizedRailVector(), Rotation) * MirrorLength;
		}

		private Vector2 GetMirrorPosition1()
		{
			// TODO: FacedUp/Down?
			return GetMirrorCenterPosition() + GetMirrorHalf();
		}

		private Vector2 GetMirrorPosition2()
		{
			return GetMirrorCenterPosition() - GetMirrorHalf();
		}

		public Line GetMirrorLine()
		{
			return new Line(GetMirrorPosition1(), GetMirrorPosition2());
		}

		private Vector2 GetRelativeRailPosition(float factor)
		{
			return RailPosition1 + GetRailVector() * factor;
		}

		// TODO: Up/Down oder Left/Right? (evtl abhängig vom RailVector (x > y bzw. y > x)
		public void MoveMirrorUp()
		{
			RelativePosition = Math.Max(0.06f, RelativePosition - MirrorMovementSpeed);
		}

		public void MoveMirrorDown()
		{
			RelativePosition = Math.Min(0.94f, RelativePosition + MirrorMovementSpeed);
		}

		public void RotateMirrorCW()
		{
			Rotation = Math.Max(-MaximumRotation, Rotation - RotationSpeed);
		}

		public void RotateMirrorCCW()
		{
			Rotation = Math.Min(MaximumRotation, Rotation + RotationSpeed);
		}

		public void DetermineAccessiblity(Vector2 playerPosition)
		{
			float distance = (GetCenterPosition() - playerPosition).Length;
			IsAccessible = distance <= MinAccessibilityDistance;
		}

		public void Reset()
		{
			RelativePosition = 0.5f;
			Rotation = Util.ConvertDegreeToRadian(StartingAngle);
		}

		public override List<Line> GetLines()
		{
			return new List<Line> { GetMirrorLine() };
		}
	}
}
