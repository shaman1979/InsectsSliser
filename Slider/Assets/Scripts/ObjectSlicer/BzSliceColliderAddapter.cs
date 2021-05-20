﻿using System;
using UnityEngine;

namespace BzKovSoft.ObjectSlicer
{
	class BzSliceColliderAddapter : IBzSliceAddapter
	{
		private Matrix4x4 localToWorldMatrix;
		private readonly Vector3[] vertices;

		public BzSliceColliderAddapter(Vector3[] vertices, GameObject gameObject)
		{
			this.vertices = vertices;
			localToWorldMatrix = gameObject.transform.localToWorldMatrix;
		}

		public Vector3 GetWorldPos(int index)
		{
			Vector3 position = vertices[index];
			return localToWorldMatrix.MultiplyPoint3x4(position);
		}

		public Vector3 GetWorldPos(BzMeshData meshData, int index)
		{
			return localToWorldMatrix.MultiplyPoint3x4(meshData.Vertices[index]);
		}

		public bool Check(BzMeshData meshData)
		{
#if DEBUG
			int trCount = 0;
			for (int i = 0; i < meshData.SubMeshes.Length; i++)
			{
				trCount += meshData.SubMeshes[i].Length;
			}

			if (trCount < 3)
				throw new Exception("FFFFF3");

			if (trCount % 3 != 0)
				throw new Exception("FFFFF4");
#endif

			return true;
		}

		public void RebuildMesh(Mesh mesh, Material[] materials, Renderer meshRenderer)
		{
			throw new NotSupportedException();
		}

		public Vector3 GetObjectCenterInWorldSpace()
		{
			return localToWorldMatrix.MultiplyPoint3x4(Vector3.zero);
		}
	}
}