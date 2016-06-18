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

		public Vector2 RailPosition1 { get; set; }
		public Vector2 RailPosition2 { get; set; }

		public float StartingAngle;
		public float StartingRelativePosition;

		public float MirrorLength = 0.15f;

		public float RelativePosition { get; set; }
		public float Rotation { get; set; }

		public float MaximumRotation { get; set; }
		public float MinimumRotation { get; set; }

		public bool IsAccessible { get; set; }

		public Mirror(Vector2 railPosition1, Vector2 railPosition2) : this(railPosition1, railPosition2, 0.5f, 0.5f)
		{

		}

		public Mirror(Vector2 railPosition1, Vector2 railPosition2, float startingAngle, float startingRelativePosition)
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

			StartingAngle = startingAngle;
			Rotation = StartingAngle;

			StartingRelativePosition = startingRelativePosition;
			RelativePosition = StartingRelativePosition;

			MinimumRotation = -0.7f;
			MaximumRotation = 0.7f;
		}

		public void SetRotationBounds(float minimumRotation, float maximumRotation)
		{
			MinimumRotation = minimumRotation;
			MaximumRotation = maximumRotation;
		}

		private Vector2 GetRailVector()
		{
			return RailPosition2 - RailPosition1;
		}

		public Vector2 GetMirrorPosition()
		{
			return RailPosition1 + GetRailVector() * RelativePosition;
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

		public Vector2 GetDistance1FromMirrorCenter(float distanceFactor)
		{
			return GetMirrorPosition() + GetMirrorNormal1() * distanceFactor;
		}

		public Vector2 GetDistance2FromMirrorCenter(float distanceFactor)
		{
			return GetMirrorPosition() + GetMirrorNormal2() * distanceFactor;
		}

		public Line GetMirrorLine()
		{
			Vector2 railVector = GetRailVector();
			railVector.Normalize();

			Vector2 mirrorHalf = Util.GetRotatedVector(railVector, Rotation) * MirrorLength;

			Vector2 mirrorPosition1 = GetMirrorPosition() + mirrorHalf;
			Vector2 mirrorPosition2 = GetMirrorPosition() - mirrorHalf;

			return new Line(mirrorPosition1, mirrorPosition2);
		}

		// Movement
		
		public void MoveMirrorRight(float deltaDistance)
		{
			RelativePosition = Math.Min(0.94f, RelativePosition + deltaDistance);
		}

		public void MoveMirrorLeft(float deltaDistance)
		{
			RelativePosition = Math.Max(0.06f, RelativePosition - deltaDistance);
		}

		public void MoveMirrorUp(float deltaDistance)
		{
			if (RailPosition1.Y < RailPosition2.Y) MoveMirrorRight(deltaDistance);
			else MoveMirrorLeft(deltaDistance);
		}

		public void MoveMirrorDown(float deltaDistance)
		{
			if (RailPosition1.Y < RailPosition2.Y) MoveMirrorLeft(deltaDistance);
			else MoveMirrorRight(deltaDistance);
		}

		public void RotateCW(float deltaAngle)
		{
			Rotation = Math.Min(MaximumRotation, Rotation + deltaAngle);
		}

		public void RotateCCW(float deltaAngle)
		{
			Rotation = Math.Max(MinimumRotation, Rotation - deltaAngle);
		}

		public void Reset()
		{
			RelativePosition = StartingRelativePosition;
			Rotation = StartingAngle;
		}

		public IEnumerable<Line> GetLines()
		{
			return new List<Line> { GetMirrorLine() };
		}
	}
}
