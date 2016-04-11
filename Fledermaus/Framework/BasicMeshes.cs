﻿using OpenTK;

namespace Framework
{
	public static class BasicMeshes
	{
		public static Mesh CreateCube(float size = 1.0f)
		{
			float s2 = size * 0.5f;
			var mesh = new Mesh();

			//corners
			mesh.positions.Add(new Vector3(s2, s2, -s2)); //0
			mesh.positions.Add(new Vector3(s2, s2, s2)); //1
			mesh.positions.Add(new Vector3(-s2, s2, s2)); //2
			mesh.positions.Add(new Vector3(-s2, s2, -s2)); //3
			mesh.positions.Add(new Vector3(s2, -s2, -s2)); //4
			mesh.positions.Add(new Vector3(-s2, -s2, -s2)); //5
			mesh.positions.Add(new Vector3(-s2, -s2, s2)); //6
			mesh.positions.Add(new Vector3(s2, -s2, s2)); //7

			//Top Face
			mesh.ids.Add(0);
			mesh.ids.Add(1);
			mesh.ids.Add(2);
			mesh.ids.Add(0);
			mesh.ids.Add(2);
			mesh.ids.Add(3);
			//Bottom Face
			mesh.ids.Add(4);
			mesh.ids.Add(5);
			mesh.ids.Add(6);
			mesh.ids.Add(4);
			mesh.ids.Add(6);
			mesh.ids.Add(7);
			//Front Face
			mesh.ids.Add(1);
			mesh.ids.Add(7);
			mesh.ids.Add(6);
			mesh.ids.Add(1);
			mesh.ids.Add(6);
			mesh.ids.Add(2);
			//Back Face
			mesh.ids.Add(0);
			mesh.ids.Add(3);
			mesh.ids.Add(5);
			mesh.ids.Add(0);
			mesh.ids.Add(5);
			mesh.ids.Add(4);
			//Left face
			mesh.ids.Add(2);
			mesh.ids.Add(6);
			mesh.ids.Add(5);
			mesh.ids.Add(2);
			mesh.ids.Add(5);
			mesh.ids.Add(3);
			//Right face
			mesh.ids.Add(1);
			mesh.ids.Add(0);
			mesh.ids.Add(4);
			mesh.ids.Add(1);
			mesh.ids.Add(4);
			mesh.ids.Add(7);
			return mesh;
		}

		public static Mesh CreateSphere(float radius_ = 1.0f, uint subdivision = 1)
		{
			//idea: subdivide icosahedron
			const float X = 0.525731112119133606f;
			const float Z = 0.850650808352039932f;

			var vdata = new float[12,3] {
				{ -X, 0.0f, Z}, { X, 0.0f, Z}, { -X, 0.0f, -Z }, { X, 0.0f, -Z },
				{ 0.0f, Z, X }, { 0.0f, Z, -X }, { 0.0f, -Z, X }, { 0.0f, -Z, -X },
				{ Z, X, 0.0f }, { -Z, X, 0.0f }, { Z, -X, 0.0f }, { -Z, -X, 0.0f }
			};
			var tindices = new uint[20,3] {
				{ 0, 4, 1 }, { 0, 9, 4 }, { 9, 5, 4 }, { 4, 5, 8 }, { 4, 8, 1 },
				{ 8, 10, 1 }, { 8, 3, 10 }, { 5, 3, 8 }, { 5, 2, 3 }, { 2, 7, 3 },
				{ 7, 10, 3 }, { 7, 6, 10 }, { 7, 11, 6 }, { 11, 0, 6 }, { 0, 1, 6 },
				{ 6, 1, 10 }, { 9, 0, 11 }, { 9, 11, 2 }, { 9, 2, 5 }, { 7, 2, 11 } };

			Mesh mesh = new Mesh();
			for (int i = 0; i < 12; ++i)
			{
				var p = new Vector3(vdata[i, 0], vdata[i, 1], vdata[i, 2]);
				mesh.normals.Add(p);
				mesh.positions.Add(p);
			}
			for (int i = 0; i < 20; ++i)
			{
				Subdivide(mesh, tindices[i, 0], tindices[i, 1], tindices[i, 2], subdivision);
			}

			//scale
			for (int i = 0; i < mesh.positions.Count; ++i)
			{
				mesh.positions[i] *= radius_;
			}

			return mesh;
		}

		public static Mesh CreateIcosahedron(float radius)
		{
			return CreateSphere(radius, 0);
		}

		public static Mesh CreatePlane(float sizeX, float sizeZ, uint segmentsX, uint segmentsZ)
		{
			float deltaX = (1.0f / segmentsX) * sizeX;
			float deltaZ = (1.0f / segmentsZ) * sizeZ;
			Mesh m = new Mesh();
			//add vertices
			for (uint u = 0; u < segmentsX + 1; ++u)
			{
				for (uint v = 0; v < segmentsZ + 1; ++v)
				{
					float x = -sizeX / 2.0f + u * deltaX;
					float z = -sizeZ / 2.0f + v * deltaZ;
					m.positions.Add(new Vector3(x, 0.0f, z));
				}
			}
			uint verticesZ = segmentsZ + 1;
			//add ids
			for (uint u = 0; u < segmentsX; ++u)
			{
				for (uint v = 0; v < segmentsZ; ++v)
				{
					m.ids.Add(u* verticesZ + v);
					m.ids.Add(u* verticesZ + v + 1);
					m.ids.Add((u + 1) * verticesZ + v);

					m.ids.Add((u + 1) * verticesZ + v);
					m.ids.Add(u* verticesZ + v + 1);
					m.ids.Add((u + 1) * verticesZ + v + 1);
				}
			}
			return m;
		}

		private static uint CreateID(Mesh m, uint id1, uint id2)
		{
			//todo: could detect if edge already calculated and reuse....
			uint i12 = (uint)m.positions.Count;
			Vector3 v12 = m.positions[(int)id1] + m.positions[(int)id2];
			v12.Normalize();
			m.normals.Add(v12);
			m.positions.Add(v12);
			return i12;
		}

		private static void Subdivide(Mesh m, uint id1, uint id2, uint id3, uint depth)
		{
			if (0 == depth)
			{
				m.ids.Add(id1);
				m.ids.Add(id2);
				m.ids.Add(id3);
				return;
			}
			uint i12 = CreateID(m, id1, id2);
			uint i23 = CreateID(m, id2, id3);
			uint i31 = CreateID(m, id3, id1);

			Subdivide(m, id1, i12, i31, depth - 1);
			Subdivide(m, id2, i23, i12, depth - 1);
			Subdivide(m, id3, i31, i23, depth - 1);
			Subdivide(m, i12, i23, i31, depth - 1);
		}
	}
}
