using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus.GameObjects
{
	interface ILogicalMirror : IBounded
	{

		bool IsAccessible { get; set; }

		void MoveMirrorUp(float deltaDistance);
		void MoveMirrorDown(float deltaDistance);

		void RotateCW(float deltaAngle);
		void RotateCCW(float deltaAngle);

		Vector2 GetMirrorPosition();
		Vector2 GetMirrorNormal1();
		Vector2 GetMirrorNormal2();

		Line GetMirrorLine();
		
	}
}
