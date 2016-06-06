using Fledermaus.Utils;
using Framework;
using Model;
using Model.GameObject;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus
{
	static class GameGraphicsLevelEditor
	{

		private enum Colors
		{
			Player,
			NPC,
			LightRay,
			RoomGround,
			Obstacle,
			MirrorActive,
			MirrorInactive,
			MirrorRailActive,
			MirrorRailInactive,
			SolarPanel,
			ExitOpen,
			ExitClosed
		}
        
		private static Vector2 _center = new Vector2(0.0f, 0.0f);
		private static float _scale = 1.0f;
		private static float _alpha = 1.0f;


		public static void SetDrawSettings(Vector2 center, float scale, float alpha)
		{
			_center = center;
			_scale = scale;
			_alpha = alpha;
		}

        // TODO: evtl vertical und horizontal scale

        public static void DrawLevel(Level level, Vector2? origin = null, float scale = 1.0f)
        {

            _center = origin ?? new Vector2(.0f, .0f);
            _scale = scale;
            if (level != null) {
                foreach (var room in level.Rooms)
                    DrawRoom(room);


            }
		}

        private static void DrawRoom(Model.GameObject.Room room)
        {
            _scale = 0.2f;
            var scPosition = room.Position * _scale;
            var scBounds = scaleBounds(room.RelativeBounds);

            //DrawSquare(scPosition, scBounds);

            if (room.Player != null)
            {
                SetColor(Colors.RoomGround);
                DrawSquare(scPosition, scBounds);

                DrawPlayer(room.Player);
            }
            else {
                SetColor(Colors.SolarPanel);
                DrawSquare(scPosition, scBounds);
            }
        }

        private static Vector3 GetColor(Colors color)
		{
			switch (color)
			{
				case Colors.Player: return new Vector3(1.0f, 0.8f, 0.6f);
				case Colors.NPC: return new Vector3(1.0f, 0.4f, 0.0f);
				case Colors.LightRay: return new Vector3(1.0f, 0.6f, 0.0f);
				case Colors.RoomGround: return new Vector3(0.16f, 0.16f, 0.16f);
				case Colors.Obstacle: return new Vector3(1.0f, 0.2f, 0.2f);
				case Colors.MirrorActive: return new Vector3(0.4f, 0.5f, 0.94f);
				case Colors.MirrorInactive: return new Vector3(0.4f, 0.45f, 0.5f);
				case Colors.MirrorRailActive: return new Vector3(0.6f, 0.6f, 0.6f);
				case Colors.MirrorRailInactive: return new Vector3(0.5f, 0.5f, 0.5f);
				case Colors.SolarPanel: return new Vector3(0.3f, 0.3f, 1.0f);
				case Colors.ExitOpen: return new Vector3(0.4f, 1.0f, 0.3f);
				case Colors.ExitClosed: return new Vector3(1.0f, 0.4f, 0.6f);
			}

			return new Vector3(0.0f, 0.0f, 0.0f);
		}

		private static void SetColor(Colors color)
		{
			Vector3 colorVector = GetColor(color);

			colorVector *= _alpha;

			GL.Color4(colorVector.X, colorVector.Y, colorVector.Z, 1.0f);
		}

		private static void DrawPlayer(Player player)
		{
			SetColor(Colors.Player);
            DrawSquare(_scale * player.InitialPosition, scaleBounds(player.RelativeBounds));

            //DrawBounds(_scale*player.InitialPosition, scaleBounds( player.RelativeBounds), 0.002f);
		}

		private static void DrawBounds(Vector2 position,List<Vector2> bounds, float thickness= 0.002f)
		{
            
            List<Vector2> absbounds = new List<Vector2>();
            foreach (var bound in bounds)
                absbounds.Add(position + bound);
            for (int i = 0; i < absbounds.Count - 1; i++)
                DrawLine(absbounds[i], absbounds[i+1], thickness);
            DrawLine(absbounds.Last(), absbounds.First(), thickness);
        }

/*		private static void DrawLine(Line line, float thickness)
		{
			DrawLine(GetTransformedVector(line.Point1), GetTransformedVector(line.Point2), thickness);
		}*/

		private static void DrawLine(Vector2 p1, Vector2 p2, float thickness)
		{
			Vector2 normal = (p2 - p1);
			normal.Normalize();

			normal *= thickness;

			Vector2 lR = Util.GetOrthogonalVectorCW(normal);
			Vector2 rR = Util.GetOrthogonalVectorCCW(normal);

			Vector2 p11 = /*_center + _scale **/ (p1 + lR);
			Vector2 p12 = /*_center + _scale **/ (p1 + rR);
			Vector2 p21 = /*_center + _scale **/ (p2 + lR);
			Vector2 p22 = /*_center + _scale **/ (p2 + rR);

             p11 =GetTransformedVector(p1 + lR);
             p12 =  GetTransformedVector(p1 + rR);
             p21 = GetTransformedVector(p2 + lR);
             p22 =  GetTransformedVector(p2 + rR);

            GL.Begin(PrimitiveType.Triangles);
			GL.Vertex2( p11 );
			GL.Vertex2( p12 );
			GL.Vertex2( p22 );
			GL.End();

			GL.Begin(PrimitiveType.Triangles);
			GL.Vertex2( p11 );
			GL.Vertex2( p21 );
			GL.Vertex2( p22 );
			GL.End();
		}
        private static List<Vector2> scaleBounds(List<Vector2> bounds ) {
            var scBounds = new List<Vector2>();
            foreach (var bound in bounds)
                scBounds.Add(_scale * bound);

            return scBounds;

        }
        private static Vector2 GetTransformedVector(Vector2 vector)
		{
			return new Vector2(vector.X * _scale + _center.X, vector.Y * _scale + _center.Y);
		}
        private static void DrawSquare(Vector2 position, List<Vector2> bounds)
        {
            GL.Begin(PrimitiveType.Quads);
            foreach (var bound in bounds)
                GL.Vertex2(position.X + bound.X, position.Y + bound.Y);
            GL.End();
        }
        private static void DrawSquare(Vector2 topLeft, Vector2 bottomRight)
		{
			GL.Begin(PrimitiveType.Quads);
			GL.Vertex2(topLeft.X, topLeft.Y);
			GL.Vertex2(bottomRight.X, topLeft.Y);
			GL.Vertex2(bottomRight.X, bottomRight.Y);
			GL.Vertex2(topLeft.X, bottomRight.Y);
			GL.End();
		}

	}
}
