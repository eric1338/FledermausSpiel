using Fledermaus.GameObjects;
using Framework;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus
{
	class GameGraphics
	{

		private static GameGraphics instance = new GameGraphics();

		public static GameGraphics GetInstance()
		{
			return instance;
		}

		public void DrawLevel(Level level)
		{
			//DrawBG / DrawScreen

			// evtl nur bewegte Objekte neu zeichnen

			DrawRoom(level.Room);
			DrawLightRay(level.LightRay);
			DrawPlayer(level.Player);
			DrawMirrors(level.Mirrors);
			DrawObstacles(level.Obstacles);
			DrawSolarPanel(level.SolarPanel);
			DrawExit(level.Exit);
		}

		private void DrawRoom(RectangularGameObject room)
		{
			GL.Color3(0.2f, 0.2f, 0.2f);
			DrawRectangularGameObject(room);
		}

		private void DrawLightRay(LightRay lightRay)
		{
			GL.Color3(1.0f, 0.6f, 0.0f);
			Vector2 p2 = lightRay.Origin + lightRay.LightVector * 5.0f;

			DrawLine(lightRay.Origin, lightRay.EndVector, 0.005f);
		}

		private void DrawPlayer(Player player)
		{
			GL.Color3(1.0f, 0.8f, 0.6f);
			DrawTest(player.Position, 0.1f);
		}

		private void DrawMirrors(List<Mirror> mirrors)
		{
			foreach (Mirror m in mirrors) DrawMirror(m);
		}

		private void DrawMirror(Mirror Mirror)
		{
			if (Mirror.IsAccessible) GL.Color3(0.6f, 0.6f, 0.6f);
			else GL.Color3(0.5f, 0.5f, 0.5f);
			DrawLine(Mirror.RailPosition1, Mirror.RailPosition2, 0.005f);
			
			if (Mirror.IsAccessible) GL.Color3(0.4f, 0.5f, 0.94f);
			else GL.Color3(0.4f, 0.45f, 0.5f);

			DrawLine(Mirror.GetMirrorPosition1(), Mirror.GetMirrorPosition2(), 0.006f);
		}

		private void DrawObstacles(List<Obstacle> obstacles)
		{
			foreach (Obstacle o in obstacles) DrawObstacle(o);
		}

		private void DrawObstacle(Obstacle obstacle)
		{
			GL.Color3(1.0f, 0.2f, 0.2f);
			DrawGameObjectLines(obstacle, 0.003f);
		}

		private void DrawSolarPanel(SolarPanel solarPanel)
		{
			GL.Color3(0.3f, 0.3f, 1.0f);
			DrawRectangularGameObject(solarPanel);
		}

		private void DrawExit(Exit exit)
		{
			if (exit.IsOpen) GL.Color3(0.4f, 1.0f, 0.3f);
			else GL.Color3(1.0f, 0.4f, 0.6f);

			DrawRectangularGameObject(exit);
		}

		// --- //

		private void DrawRectangularGameObject(RectangularGameObject rectangularGameObject)
		{
			DrawAABR(rectangularGameObject.aabr);
		}

		private void DrawGameObjectLines(GameObject gameObject, float thickness)
		{
			DrawLines(gameObject.GetLines(), thickness);
		}

		private void DrawLines(List<Line> lines, float thickness)
		{
			foreach (Line v in lines) DrawLine(v, thickness);
		}

		private void DrawLine(Line line, float thickness)
		{
			DrawLine(line.Point1, line.Point2, thickness);
		}

		private void DrawLine(Vector2 p1, Vector2 p2, float thickness)
		{
			Vector2 normal = p2 - p1;
			normal.Normalize();

			normal *= thickness;

			Vector2 lR = Util.GetOrthogonalVectorCW(normal);
			Vector2 rR = Util.GetOrthogonalVectorCCW(normal);

			Vector2 p11 = p1 + lR;
			Vector2 p12 = p1 + rR;
			Vector2 p21 = p2 + lR;
			Vector2 p22 = p2 + rR;

			GL.Begin(PrimitiveType.Triangles);
			GL.Vertex2(p11);
			GL.Vertex2(p12);
			GL.Vertex2(p22);
			GL.End();

			GL.Begin(PrimitiveType.Triangles);
			GL.Vertex2(p11);
			GL.Vertex2(p21);
			GL.Vertex2(p22);
			GL.End();
		}

		private void DrawTest(Vector2 center)
		{
			DrawTest(center, 0.05f);
		}

		private void DrawTest(Vector2 center, float w)
		{
			AABR aabr = new AABR(center.X - (w / 2), center.Y - (w / 2), w, w);
			DrawAABR(aabr);
		}

		private void DrawAABR(AABR bounds)
		{
			GL.Begin(PrimitiveType.Quads);
			GL.Vertex2(bounds.X, bounds.Y);
			GL.Vertex2(bounds.MaxX, bounds.Y);
			GL.Vertex2(bounds.MaxX, bounds.MaxY);
			GL.Vertex2(bounds.X, bounds.MaxY);
			GL.End();
		}

	}
}
