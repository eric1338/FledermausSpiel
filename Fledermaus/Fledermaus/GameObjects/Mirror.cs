using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	class Mirror
	{

		// TODO: bool FaceUpwards?

		// TODO: evtl auslagern
		public const float MirrorMovementSpeed = 0.02f;
		public const float RotationSpeed = 0.4f;
		public const float PlayerMirrorDistance = 0.05f;
		public const float MinAccessibilityDistance = 0.20f;

		public float MirrorLength = 0.2f;

		public float RelativePosition { get; set; }
		public float Rotation { get; set; }

		public float MaximumRotation { get; set; }

		public Vector2 RailPosition1 { get; set; }
		public Vector2 RailPosition2 { get; set; }

		public bool IsAccessible { get; set; }

		public Mirror()
		{
			RelativePosition = 0.5f;
			Rotation = 20.0f;
			MaximumRotation = 40.0f;
		}

		// evtl Pos1 und Pos2 selbst bestimmen (Pos1.x immer < Pos2.x)

		public Vector2 GetRelativePlayerPosition()
		{
			Vector2 v = GetNormalizedRailVector() * PlayerMirrorDistance;

			// je nach FaceUpwards CW oder CCW
			Vector2 orth = Util.GetOrthogonalVectorCW(v);

			return GetMirrorCenterPosition() + orth;
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

		public Vector2 GetCenterPosition()
		{
			return GetRelativeRailPosition(0.5f);
		}

		public Vector2 GetMirrorCenterPosition()
		{
			return GetRelativeRailPosition(RelativePosition);
		}

		public Vector2 GetMirrorPosition1()
		{
			// TODO: FacedUp/Down?
			Vector2 rotation = Util.GetRotatedVector(GetNormalizedRailVector(), Rotation) * MirrorLength;

			return GetMirrorCenterPosition() + rotation;
		}

		public Vector2 GetMirrorPosition2()
		{
			Vector2 rotation = Util.GetRotatedVector(GetNormalizedRailVector(), Rotation) * MirrorLength;

			return GetMirrorCenterPosition() - rotation;
		}

		private Vector2 GetRelativeRailPosition(float factor)
		{
			return RailPosition1 + GetRailVector() * factor;
		}

		// TODO: Up/Down oder Left/Right? (evtl abhängig vom RailVector (x > y bzw. y > x)
		public void MoveMirrorUp()
		{
			RelativePosition = Math.Min(0.94f, RelativePosition + MirrorMovementSpeed);
		}

		public void MoveMirrorDown()
		{
			RelativePosition = Math.Max(0.06f, RelativePosition - MirrorMovementSpeed);
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
		}

	}
}
