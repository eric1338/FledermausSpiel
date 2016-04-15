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
		public const float BatMirrorDistance = 0.05f;

		public float RelativePosition { get; set; }
		public float Rotation { get; set; }

		public Vector2 RailPosition1 { get; set; }
		public Vector2 RailPosition2 { get; set; }

		public Mirror()
		{
			RelativePosition = 0.5f;


		}

		// evtl Pos1 und Pos2 selbst bestimmen (Pos1.x immer < Pos2.x)

		public Vector2 GetBatPosition()
		{
			Vector2 rail = RailPosition2 - RailPosition1;
			rail.Normalize();
			rail *= BatMirrorDistance;

			// je nach FaceUpwards CW oder CCW
			Vector2 orth = Util.GetOrthogonalVectorCW(rail);

			return GetCenterPosition() + orth;
		}

		public Vector2 GetCenterPosition()
		{
			return GetRelativeRailPosition(0.5f);
		}

		public Vector2 GetMirrorPosition()
		{
			return GetRelativeRailPosition(RelativePosition);
		}

		private Vector2 GetRelativeRailPosition(float factor)
		{
			return RailPosition1 + (RailPosition2 - RailPosition1) * factor;
		}

		// TODO: Up/Down oder Left/Right? (evtl abhängig vom RailVector (x > y bzw. y > x)
		public void MoveMirrorUp()
		{
			RelativePosition = Math.Min(0.98f, RelativePosition + MirrorMovementSpeed);
		}

		public void MoveMirrorDown()
		{
			RelativePosition = Math.Max(0.02f, RelativePosition - MirrorMovementSpeed);
		}

	}
}
