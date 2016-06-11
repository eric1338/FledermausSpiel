using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
    public interface ILogicalMirror : IBounded
	{

		bool IsAccessible { get; set; }

		void MoveMirrorRight(float deltaDistance);
		void MoveMirrorLeft(float deltaDistance);
		void MoveMirrorUp(float deltaDistance);
		void MoveMirrorDown(float deltaDistance);

		void RotateCW(float deltaAngle);
		void RotateCCW(float deltaAngle);

		Vector2 GetMirrorPosition();
		Vector2 GetMirrorNormal1();
		Vector2 GetMirrorNormal2();
		Vector2 GetDistance1FromMirrorCenter(float distanceFactor);
		Vector2 GetDistance2FromMirrorCenter(float distanceFactor);

		Line GetMirrorLine();

	}
}
