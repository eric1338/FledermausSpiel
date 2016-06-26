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

		public enum Colors
		{
			LightRay,
			MirrorActive,
			MirrorInactive,
			MirrorRailActive,
			MirrorRailInactive,
			InnerMirrorRail,
			Exit,
			DefaultText,
			HeaderText,
			SpecialText,
			ButtonText,
			SelectedText,
			DisabledText,
			SelectedDisabledText
		}

		private static TextureFont _font;
		private static float _characterSpacing = 0.5f;

		public static float WindowWidth = 1;
		public static float WindowHeight = 1;

		private static void InitializeFont()
		{
			_font = new TextureFont(TextureLoader.FromBitmap(Resources.font), 10, 32, 1, 1, _characterSpacing);
		}

		public static float GetXScale()
		{
			return WindowHeight / WindowWidth;
		}

		public static Vector2 GetTransformedVector(Vector2 vector)
		{
			return new Vector2(vector.X * GetXScale(), vector.Y);
		}

		private static Vector3 GetColor(Colors color)
		{
			switch (color)
			{
				case Colors.LightRay: return new Vector3(1.0f, 0.6f, 0.0f);
				case Colors.MirrorActive: return new Vector3(0.0f, 0.5f, 1.0f);
				case Colors.MirrorInactive: return new Vector3(0.2f, 0.4f, 0.6f);
				case Colors.MirrorRailActive: return new Vector3(0.6f, 0.6f, 0.6f);
				case Colors.MirrorRailInactive: return new Vector3(0.5f, 0.5f, 0.5f);
				case Colors.InnerMirrorRail: return new Vector3(0.1f, 0.1f, 0.1f);
				case Colors.Exit: return new Vector3(1.0f, 0.4f, 0.6f);
				case Colors.DefaultText: return new Vector3(1.0f, 0.83f, 0.66f);
				case Colors.HeaderText: return new Vector3(1.0f, 0.5f, 0.3f);
				case Colors.SpecialText: return new Vector3(1.0f, 0.38f, 0.38f);
				case Colors.ButtonText: return new Vector3(1.0f, 0.5f, 0.3f);
				case Colors.SelectedText: return new Vector3(1.0f, 0.91f, 0.78f);
				case Colors.DisabledText: return new Vector3(0.5f, 0.5f, 0.5f);
				case Colors.SelectedDisabledText: return new Vector3(0.8f, 0.8f, 0.8f);
			}

			return new Vector3(0.0f, 0.0f, 0.0f);
		}

		public static void SetColor(Colors color, float alpha = 1.0f)
		{
			GL.Color3(GetColor(color) * alpha);
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

			cwRotation = GetTransformedVector(cwRotation);
			ccwRotation = GetTransformedVector(ccwRotation);

			Vector2 point11 = point1 + cwRotation;
			Vector2 point12 = point1 + ccwRotation;
			Vector2 point21 = point2 + cwRotation;
			Vector2 point22 = point2 + ccwRotation;

			//GL.Begin(PrimitiveType.Quads);
			//GL.Vertex2(point11);
			//GL.Vertex2(point12);
			//GL.Vertex2(point21);
			//GL.Vertex2(point22);
			//GL.End();

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

		public static void DrawOpenGLLine(Vector2 point1, Vector2 point2)
		{
			GL.Begin(PrimitiveType.Lines);
			GL.Vertex2(GetTransformedVector(point1));
			GL.Vertex2(GetTransformedVector(point2));
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

		public static void DrawCenteredText(string text, Vector2 center, float size)
		{
			float width = _font.Width(text, size, _characterSpacing);
			float x = center.X - (width / 2f);

			//x *= GetXScale();
			//Console.WriteLine(x + "  / " + width);

			DrawText(text, new Vector2(x, center.Y), size);
		}

	}
}
