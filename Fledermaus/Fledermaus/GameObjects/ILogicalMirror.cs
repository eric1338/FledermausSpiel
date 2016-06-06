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

		Vector2 RailPosition1 { get; set; }
		Vector2 RailPosition2 { get; set; }

		float Rotation { get; set; }
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

		Line GetMirrorLine();
		
	}
}
