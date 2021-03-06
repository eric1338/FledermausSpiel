﻿using System.Collections.Generic;

namespace Framework
{
	public class Obj2Mesh
	{
		private class VertexComparer : IEqualityComparer<ObjParser.Vertex>
		{
			public bool Equals(ObjParser.Vertex a, ObjParser.Vertex b)
			{
				return (a.idNormal == b.idNormal) && (a.idPos == b.idPos) && (a.idTexCoord == b.idTexCoord);
			}

			public int GetHashCode(ObjParser.Vertex obj)
			{
				return obj.idPos;
			}
		}

		public static Mesh FromObj(byte[] objByteData)
		{
			var parser = new ObjParser(objByteData);
			var uniqueVertexIDs = new Dictionary<ObjParser.Vertex, uint>(new VertexComparer());

			var mesh = new Mesh();

			foreach (var face in parser.faces)
			{
				//only accept triangles
				if (3 != face.Count) continue;
				foreach (var vertex in face)
				{
					uint index;
					if (uniqueVertexIDs.TryGetValue(vertex, out index))
					{
						mesh.ids.Add(index);
					}
					else
					{
						uint id = (uint) mesh.positions.Count;
						//add vertex data to mesh
						mesh.positions.Add(parser.position[vertex.idPos]);
						mesh.normals.Add(parser.normals[vertex.idNormal]);
						mesh.uvs.Add(parser.texCoords[vertex.idTexCoord]);
						mesh.ids.Add(id);
						//new id
						uniqueVertexIDs[vertex] = id;
					}
				}
			}
			return mesh;
		}
	}
}
