using Fledermaus.Utils;
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
	class BasicGraphics
	{

		private static TextureFont _font;
		private static float _characterSpacing = 0.5f;

		public static float WindowWidth = 1;
		public static float WindowHeight = 1;

		private static void InitializeFont()
		{
			_font = new TextureFont(TextureLoader.FromBitmap(Resources.font), 10, 32, 1, 1, _characterSpacing);
		}

		private static float GetXScale()
		{
			//return 1;

			return WindowHeight / WindowWidth;
		}

		private static Vector2 GetTransformedVector(Vector2 vector)
		{
			return new Vector2(vector.X * GetXScale(), vector.Y);
		}

		public static void DrawLine(Vector2 point1, Vector2 point2, float thickness)
		{
			point1 = GetTransformedVector(point1);
			point2 = GetTransformedVector(point2);

			Vector2 normal = point2 - point1;
			normal.Normalize();

			normal *= thickness;

			Vector2 cwRotation = Util.GetOrthogonalVectorCW(normal);
			Vector2 ccwRotation = Util.GetOrthogonalVectorCCW(normal);

			Vector2 point11 = point1 + cwRotation;
			Vector2 point12 = point1 + ccwRotation;
			Vector2 point21 = point2 + cwRotation;
			Vector2 point22 = point2 + ccwRotation;

			GL.Begin(PrimitiveType.Triangles);
			GL.Vertex2(point11);
			GL.Vertex2(point12);
			GL.Vertex2(point22);
			GL.End();

			GL.Begin(PrimitiveType.Triangles);
			GL.Vertex2(point11);
			GL.Vertex2(point21);
			GL.Vertex2(point22);
			GL.End();
		}

		public static void DrawSquare(Vector2 topLeft, Vector2 bottomRight)
		{
			topLeft = GetTransformedVector(topLeft);
			bottomRight = GetTransformedVector(bottomRight);

			GL.Begin(PrimitiveType.Quads);
			GL.Vertex2(topLeft.X, bottomRight.Y);
			GL.Vertex2(topLeft.X, topLeft.Y);
			GL.Vertex2(bottomRight.X, topLeft.Y);
			GL.Vertex2(bottomRight.X, bottomRight.Y);
			GL.End();
		}

		public static void DrawRectangularTexture(Texture texture, Vector2 topLeft, Vector2 bottomRight, float alpha = 1.0f)
		{
			Vector2 bottomLeft = new Vector2(topLeft.X, bottomRight.Y);
			Vector2 topRight = new Vector2(bottomRight.X, topLeft.Y);

			DrawTexture(texture, bottomLeft, bottomRight, topRight, topLeft, alpha);
		}

		public static void DrawTexture(Texture texture, Vector2 bottomLeft, Vector2 bottomRight, Vector2 topRight, Vector2 topLeft, float alpha = 1.0f)
		{
			texture.BeginUse();

			GL.Color3(alpha, alpha, alpha);

			GL.Begin(PrimitiveType.Quads);
			GL.TexCoord2(0.0f, 0.0f);
			GL.Vertex2(GetTransformedVector(bottomLeft));
			GL.TexCoord2(1.0f, 0.0f);
			GL.Vertex2(GetTransformedVector(bottomRight));
			GL.TexCoord2(1.0f, 1.0f);
			GL.Vertex2(GetTransformedVector(topRight));
			GL.TexCoord2(0.0f, 1.0f);
			GL.Vertex2(GetTransformedVector(topLeft));
			GL.End();

			texture.EndUse();
		}

		public static void DrawDefaultBackground()
		{
			Textures.Instance.LoadTextures();

			Texture backgroundTexture = Textures.Instance.FloorTexture;

			float length = 2 * 512.0f / WindowHeight;

			float xBounds = 1 / GetXScale();

			float x = -xBounds;
			float y = 1;

			Vector2 topLeft;
			Vector2 bottomRight;

			while (y > -1)
			{
				topLeft = new Vector2(x, y);
				bottomRight = new Vector2(x + length, y - length);

				DrawRectangularTexture(backgroundTexture, topLeft, bottomRight);

				x += length;

				if (x > xBounds)
				{
					x = -xBounds;
					y -= length;
				}
			}
		}

		public static void DrawText(string text, Vector2 position, float size, Vector3 color)
		{
			GL.Color3(color);

			DrawText(text, position, size);
		}

		public static void DrawText(string text, Vector2 position, float size)
		{
			if (_font == null) InitializeFont();

			_font.Print(position.X, position.Y, 0.0f, size, text, GetXScale());
		}

		public static void DrawCenteredText(string text, float y, float size)
		{
			float width = _font.Width(text, size, _characterSpacing);
			float x = 0.0f - (width / 2f);

			DrawText(text, new Vector2(x, y), size);
		}

	}
}
