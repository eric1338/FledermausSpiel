using Fledermaus.GameObjects;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fledermaus
{
	class GameLogic
	{

		private static GameLogic instance = new GameLogic();

		public static GameLogic GetInstance()
		{
			return instance;
		}

		public void MakeActions(Inputs inputs, Level level)
		{
			// TODO: anders

			Player player = level.Player;
			Mirror currentMirror = player.CurrentMirror;

			bool mirrorMovement = currentMirror != null;

			if (mirrorMovement)
			{
				if (inputs.UserActionStatus[UserAction.MoveLeft])
				{
					currentMirror.MoveMirrorUp();
					player.Position = currentMirror.GetRelativePlayerPosition();
				}
				if (inputs.UserActionStatus[UserAction.MoveRight])
				{
					currentMirror.MoveMirrorDown();
					player.Position = currentMirror.GetRelativePlayerPosition();
				}

				if (inputs.UserActionStatus[UserAction.RotateMirrorCW]) currentMirror.RotateMirrorCW();
				if (inputs.UserActionStatus[UserAction.RotateMirrorCCW]) currentMirror.RotateMirrorCCW();
			}
			else
			{
				float dx = 0.0f;
				float dy = 0.0f;

				if (inputs.UserActionStatus[UserAction.MoveUp]) dy = 0.01f;
				if (inputs.UserActionStatus[UserAction.MoveDown]) dy = -0.01f;
				if (inputs.UserActionStatus[UserAction.MoveLeft]) dx = -0.01f;
				if (inputs.UserActionStatus[UserAction.MoveRight]) dx = 0.01f;

				TryToMovePlayer(level, dx, dy);
			}

			int n = inputs.SingleUserActions.Count;
			
			for (int i = 0; i < n; i++)
			{
				UserAction nextUserAction = inputs.SingleUserActions.Dequeue();

				if (nextUserAction == UserAction.ToggleMirrorLock)
				{
					ToggleMirrorLock(level);

				}
				else if (nextUserAction == UserAction.ResetLevel)
				{
					level.Reset();
				}
			}
		}

		private void ToggleMirrorLock(Level level)
		{
			Player player = level.Player;

			if (player.CurrentMirror != null)
			{
				player.CurrentMirror = null;
			}
			else
			{
				foreach (Mirror m in level.Mirrors)
				{
					if (m.IsAccessible)
					{
						player.CurrentMirror = m;
						player.Position = m.GetRelativePlayerPosition();
					}
				}
			}
		}

		private void TryToMovePlayer(Level level, float dx, float dy)
		{
			//Vector2 newPosition = level.Player.Position + new Vector2(dx, dy);

			//Room room = level.Room;

			//bool movePlayer = true;

			//// anders (am linken Rand blockiert W+A)
			//if (room.LeftX > newPosition.X || room.RightX < newPosition.X ||
			//	room.BottomY > newPosition.Y || room.TopY < newPosition.Y) movePlayer = false;

			//// CheckBounds

			//if (movePlayer) level.Player.Position = newPosition;

			// evtl Move
			// CheckRay
		}

		public void DoLogic(Level level)
		{
			DetermineMirrorAccessibility(level);
			CalculateLightRay(level);
			CheckExit();
		}

		private void DetermineMirrorAccessibility(Level level)
		{
			foreach (Mirror m in level.Mirrors)
			{
				m.DetermineAccessiblity(level.Player.Position);
			}
		}

		private void CalculateLightRay(Level level)
		{
            List<Vertex> vertices = level.Room.Vertices;

            LightRay ray = level.LightRay;

            foreach (Vertex v in vertices)
            {
                Vector2? i = VectorIntersection(v.OriginVector, v.DirectionVector, ray.Origin, ray.Origin + ray.LightVector * 100);

                if (i != null)
                {
                    Console.WriteLine("i: " + i);
                    ray.EndVector = (Vector2) i;
                }
            }

            //Vector2 v1 = new Vector2(0.0f, 1.0f);
            //Vector2 v2 = new Vector2(0.8f, -0.9f);

            //Vector2 i2 = VectorIntersection(v1, v2 - v1, ray.Origin, ray.LightVector);
            //if (i2 != new Vector2(0.13f, 0.13f))
            //{
            //    Console.WriteLine("i2: " + i2);
            //    ray.EndVector = (Vector2)i2;
            //}

            //Vector2 i3 = VectorIntersection(v1, v2, ray.Origin, new Vector2(-0.9f, -0.9f));
            //if (i3 != new Vector2(0.13f, 0.13f))
            //{
            //    Console.WriteLine("i3: " + i3);
            //    ray.EndVector = (Vector2)i3;
            //}
        }

        public static Vector2? VectorIntersection(Vector2 p1o, Vector2 p1d, Vector2 p2o, Vector2 p2d)
        {
            float x1 = p1o.X;
            float y1 = p1o.Y;
            float x2 = p1d.X;
            float y2 = p1d.Y;
            float x3 = p2o.X;
            float y3 = p2o.Y;
            float x4 = p2d.X;
            float y4 = p2d.Y;


            //float t = (x1 * y4 - x3 * y4 - x4 * y1 + x4 * y3) / (x4 * y2 - x2 * y4);
            float t = (-x1 * y4 + x3 * y4 + x4 * y1 - x4 * y3) / (x2 * y4 - x4 * y2);
            float u = (y1 + t * y2 - y3) / y4;
            float v = (x1 + t * x2 - x3) / x4;

            //Console.WriteLine(t + " | " + u + " | " + v);

            //Console.WriteLine(t + " / " + u);

            Vector2 sol = p1o + p1d * t;
            Vector2 sol2 = p1o + p1d * u;

            if (t >= 0 && t <= 1 && u >= 0 && u <= 1)
            {
                return p1o + (p1d - p1o) * u;
            }
            else
                return null;

            //Console.WriteLine(sol + " , " + sol2);

            //Matrix2x3 matrix = new Matrix2x3();
            //matrix.M11 = x2 - x1;
            //matrix.M12 = -(x4 - x3);
            //matrix.M13 = x3 - x1;

            //matrix.M21 = y2 - y1;
            //matrix.M22 = -(y4-y3);
            //matrix.M23 = y3 - y1;




            //Matrix2 a1 = new Matrix2(matrix.M13, matrix.M12, matrix.M23, matrix.M22);
            //Matrix2 a = new Matrix2(matrix.M11, matrix.M12, matrix.M21, matrix.M22);
            //Matrix2 a2 = new Matrix2(matrix.M11, matrix.M13, matrix.M21, matrix.M23);

            //float xx1 = a1.Determinant / a.Determinant;
            //float xx2 = a2.Determinant / a.Determinant;

            //Console.WriteLine("M: " + matrix);

            //Console.WriteLine(matrix.M11 + " | " + matrix.M12 + " | " + matrix.M13);
            //Console.WriteLine(matrix.M21 + " | " + matrix.M22 + " | " + matrix.M23);

            //Matrix2x3.
        }

		private void CheckExit()
		{
			// Exit.IsOpen = SolarPanel.HitByRay()
		}

	}
}
