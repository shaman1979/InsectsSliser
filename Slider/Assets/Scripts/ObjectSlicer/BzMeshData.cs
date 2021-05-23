using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Profiling;

namespace BzKovSoft.ObjectSlicer
{
	public class BzMeshData
	{
		public readonly List<Vector3> Vertices;
		public readonly List<Vector3> Normals;

		public readonly List<Color> Colors;
		public readonly List<Color32> Colors32;

		public readonly List<Vector2> UV;
		public readonly List<Vector2> UV2;
		public readonly List<Vector2> UV3;
		public readonly List<Vector2> UV4;
		public readonly List<Vector4> Tangents;

		public readonly List<BoneWeight> BoneWeights;
		private readonly Matrix4x4[] bindposes;

		public int[][] SubMeshes;

		public Material[] Materials;

		public bool NormalsExists => Normals != null;
		public bool ColorsExists => Colors != null;
		public bool Colors32Exists => Colors32 != null;
		public bool UVExists => UV != null;
		public bool UV2Exists => UV2 != null;
		public bool UV3Exists => UV3 != null;
		public bool UV4Exists => UV4 != null;
		public bool TangentsExists => Tangents != null;
		public bool BoneWeightsExists => BoneWeights != null;
		public bool MaterialsExists => Materials != null;

		public BzMeshData(Mesh initFrom, Material[] materials)
        {
            Materials = materials;
            int vertCount = initFrom.vertexCount / 3;
            bindposes = initFrom.bindposes;
            if (bindposes.Length == 0)	bindposes = null;

            Vertices = new List<Vector3>(vertCount);
            Normals = new List<Vector3>(vertCount);
            Colors = new List<Color>();
            Colors32 = new List<Color32>();
            UV = new List<Vector2>(vertCount);
            UV2 = new List<Vector2>();
            UV3 = new List<Vector2>();
            UV4 = new List<Vector2>();
            Tangents = new List<Vector4>();
            BoneWeights = new List<BoneWeight>(bindposes == null ? 0 : vertCount);

            initFrom.GetVertices(Vertices);
            initFrom.GetNormals(Normals);
            initFrom.GetColors(Colors);
            initFrom.GetColors(Colors32);
            initFrom.GetUVs(0, UV);
            initFrom.GetTangents(Tangents);
            initFrom.GetBoneWeights(BoneWeights);

            SubMeshes = new int[initFrom.subMeshCount][];
            //for (int subMeshIndex = 0; subMeshIndex < initFrom.subMeshCount; ++subMeshIndex)
            //	SubMeshes[subMeshIndex] = initFrom.GetTriangles(subMeshIndex);

            if (Normals.Count == 0)		Normals = null;
            if (Colors.Count == 0)		Colors = null;
            if (Colors32.Count == 0)	Colors32 = null;
            if (UV.Count == 0)			UV = null;
            if (UV2.Count == 0)			UV2 = null;
            if (UV3.Count == 0)			UV3 = null;
            if (UV4.Count == 0)			UV4 = null;
            if (Tangents.Count == 0)	Tangents = null;
            if (BoneWeights.Count == 0)	BoneWeights = null;
        }

        public IEnumerator GenerateMesh(Action<Mesh> handler)
		{
			var mesh = new Mesh();
			
			mesh.SetVertices(Vertices);
			if (NormalsExists)
				mesh.SetNormals(Normals);

			if (ColorsExists)
				mesh.SetColors(Colors);
			if (Colors32Exists)
				mesh.SetColors(Colors32);

			if (UVExists)
				mesh.SetUVs(0, UV);
			if (UV2Exists)
				mesh.SetUVs(1, UV2);
			if (UV3Exists)
				mesh.SetUVs(2, UV3);
			if (UV4Exists)
				mesh.SetUVs(3, UV4);

			if (TangentsExists)
				mesh.SetTangents(Tangents);

			if (BoneWeightsExists)
			{
				mesh.boneWeights = BoneWeights.ToArray();
				mesh.bindposes = bindposes;
			}
			
			mesh.subMeshCount = SubMeshes.Length;
			for (int subMeshIndex = 0; subMeshIndex < SubMeshes.Length; ++subMeshIndex)
			{
				mesh.SetTriangles(SubMeshes[subMeshIndex], subMeshIndex);

				if (subMeshIndex % 50 == 0)
				{
					yield return null;
				}
			}

			handler?.Invoke(mesh);
		}
    }
}
