using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Assets.Scripts.Tools;
using UnityEngine;

namespace BzKovSoft.ObjectSlicer
{
	public class BzMeshDataDissector
	{
		private const float MinWidth = 0.001f;
		private readonly IBzSliceAddapter adapter;
		private Plane plane;
		private bool sliced = false;

		private BzMeshData meshDataNeg;
		private readonly BzMeshData meshDataPos;
		private readonly int[][] subMeshes;

		public Material DefaultSliceMaterial { get; set; }

		public BzMeshData SliceResultNeg => meshDataNeg;
		public BzMeshData SliceResultPos => meshDataPos;
		private BzSliceConfiguration Configuration { get; }
		public List<PolyMeshData> CapsNeg { get; private set; }
		public List<PolyMeshData> CapsPos { get; private set; }

		public BzMeshDataDissector(Mesh mesh, Plane plane, Material[] materials, IBzSliceAddapter adapter, BzSliceConfiguration configuration)
		{
			this.adapter = adapter;
			this.plane = plane;
			Configuration = configuration;

			if (Configuration != null && Configuration.SliceMaterial == null)
			{
				Configuration.SliceMaterial = null;
			}

			meshDataNeg = new BzMeshData(mesh, materials);
			meshDataPos = new BzMeshData(mesh, materials);

			subMeshes = new int[mesh.subMeshCount][];
			for (var subMeshIndex = 0; subMeshIndex < mesh.subMeshCount; ++subMeshIndex)
			{
				subMeshes[subMeshIndex] = mesh.GetTriangles(subMeshIndex);
			}
		}
		
		// ReSharper disable Unity.PerformanceAnalysis
		public SliceResult Slice()
		{
			if (sliced)
				throw new InvalidOperationException("Object already sliced");

			sliced = true;

			if (Configuration == null)
				return SliceMesh(DefaultSliceMaterial);

			switch (Configuration.SliceType)
			{
				case SliceType.Slice:
					return SliceMesh(Configuration.SliceMaterial ? Configuration.SliceMaterial : DefaultSliceMaterial);

				case SliceType.KeepOne:
					return plane.GetSide(adapter.GetObjectCenterInWorldSpace()) ?
						SliceResult.Pos : SliceResult.Neg;

				case SliceType.Duplicate:
					return SliceResult.Duplicate;

				default: throw new NotSupportedException();
			}
		}

		private SliceResult SliceMesh(Material sectionViewMaterial)
		{
			var planeInverted = new Plane(-plane.normal, -plane.distance);

			var meshEditorNeg = new BzMeshDataEditor(meshDataNeg, plane, adapter);
			var meshEditorPos = new BzMeshDataEditor(meshDataPos, planeInverted, adapter);

			for (var subMeshIndex = 0; subMeshIndex < subMeshes.Length; ++subMeshIndex)
			{
				var newTriangles = subMeshes[subMeshIndex];

				var trCount = newTriangles.Length / 3;
				var trianglesNeg = new List<BzTriangle>(trCount);
				var trianglesPos = new List<BzTriangle>(trCount);
				var trianglesNegSliced = new List<BzTriangle>(trCount / 10);
				var trianglesPosSliced = new List<BzTriangle>(trCount / 10);

				for (var i = 0; i < trCount; ++i)
				{
					var trIndex = i * 3;
					var bzTriangle = new BzTriangle(
						newTriangles[trIndex + 0],
						newTriangles[trIndex + 1],
						newTriangles[trIndex + 2]);

					var v1 = adapter.GetWorldPos(bzTriangle.i1);
					var v2 = adapter.GetWorldPos(bzTriangle.i2);
					var v3 = adapter.GetWorldPos(bzTriangle.i3);
					var side1 = plane.GetSide(v1);
					var side2 = plane.GetSide(v2);
					var side3 = plane.GetSide(v3);
					var PosSide = side1 | side2 | side3;
					var NegSide = !side1 | !side2 | !side3;

					if (NegSide & PosSide)
					{
						bzTriangle.DivideByPlane(
							meshEditorNeg, meshEditorPos,
							trianglesNegSliced, trianglesPosSliced,
							side1, side2, side3);
					}
					else if (NegSide)
					{
						trianglesNeg.Add(bzTriangle);
					}
					else if (PosSide)
					{
						trianglesPos.Add(bzTriangle);
					}
					else
						throw new InvalidOperationException();
				}

				OptimizeEdgeTriangles(meshEditorNeg, meshDataNeg, trianglesNegSliced);
				OptimizeEdgeTriangles(meshEditorPos, meshDataPos, trianglesPosSliced);
				meshDataNeg.SubMeshes[subMeshIndex] = MakeTriangleToList(trianglesNeg, trianglesNegSliced);
				meshDataPos.SubMeshes[subMeshIndex] = MakeTriangleToList(trianglesPos, trianglesPosSliced);
			}

			CapsNeg = meshEditorNeg.CapSlice(sectionViewMaterial);
			CapsPos = meshEditorPos.CapSlice(sectionViewMaterial);

			meshEditorNeg.DeleteUnusedVertices();
			meshEditorPos.DeleteUnusedVertices();

			if (!CheckNewMesh(meshDataNeg))
			{
				return SliceResult.Pos;
			}
			if (!CheckNewMesh(meshDataPos))
			{
				return SliceResult.Neg;
			}

			return SliceResult.Sliced;
		}

		private static void OptimizeEdgeTriangles(BzMeshDataEditor meshEditor, BzMeshData meshData, List<BzTriangle> bzTriangles)
		{
			var edgeLoops = meshEditor.GetEdgeLoopsByIndex();

			var trToDelete = new bool[bzTriangles.Count];

			var edgeLoopsNode = edgeLoops.First;
			while (edgeLoopsNode != null)
			{
				var edgeLoop = edgeLoopsNode.Value;
				edgeLoopsNode = edgeLoopsNode.Next;

				var edge = edgeLoop.first;
				var counter = edgeLoop.size;
				while (counter > 0 & edgeLoop.size >= 3)
				{
					--counter;

					var edgeItem1 = edge;
					var edgeItem2 = edgeItem1.next;
					var edgeItem3 = edgeItem2.next;

					var i1 = edgeItem1.value;
					var i2 = edgeItem2.value;
					var i3 = edgeItem3.value;

					var v1 = meshData.Vertices[i1];
					var v2 = meshData.Vertices[i2];
					var v3 = meshData.Vertices[i3];

					if (v1 == v2)
					{
						EmptyRedundantIndex(i2, i3, bzTriangles, trToDelete);
						edgeItem2.Remove();
						continue;
					}

					var dir1 = (v2 - v1).normalized;
					var dir2 = (v3 - v2).normalized;

					if (dir1 == dir2)
					{
						EmptyRedundantIndex(i2, i3, bzTriangles, trToDelete);
						edgeItem2.Remove();
					}
					else
						edge = edge.next;
				}
			}

			// remove empty
			var count = 0;
			for (var i = 0; i < bzTriangles.Count; i++)
			{
				var value = bzTriangles[i];
				bzTriangles[count] = value;

				if (!trToDelete[i])
					++count;
			}

			bzTriangles.RemoveRange(count, bzTriangles.Count - count);
		}

		public void RebuildNegMesh(Renderer meshRenderer)
		{
			var mesh = meshDataNeg.GenerateMesh();
			adapter.RebuildMesh(mesh, meshDataNeg.Materials, meshRenderer);
		}

		public void RebuildPosMesh(Renderer meshRenderer)
		{
			var mesh = meshDataPos.GenerateMesh();
			adapter.RebuildMesh(mesh, meshDataPos.Materials, meshRenderer);
		}

		private static void EmptyRedundantIndex(int indexMiddle, int indexNext, List<BzTriangle> bzTriangles, bool[] trToDelete)
		{
			// make redundants empty
			for (var i = 0; i < bzTriangles.Count; i++)
			{
				var tr = bzTriangles[i];
				if (trToDelete[i])
					continue;

				if (tr.i1 == indexMiddle | tr.i2 == indexMiddle | tr.i3 == indexMiddle)
				{
					if (tr.i1 == indexNext | tr.i2 == indexNext | tr.i3 == indexNext)
					{
						trToDelete[i] = true;
					}
					else
					{
						if (tr.i1 == indexMiddle)
						{
							bzTriangles[i] = new BzTriangle(indexNext, tr.i2, tr.i3);
						}
						else if (tr.i2 == indexMiddle)
						{
							bzTriangles[i] = new BzTriangle(tr.i1, indexNext, tr.i3);
						}
						else if (tr.i3 == indexMiddle)
						{
							bzTriangles[i] = new BzTriangle(tr.i1, tr.i2, indexNext);
						}
					}
				}
			}
		}

		private bool CheckNewMesh(BzMeshData meshData)
		{
			if (meshData.SubMeshes.All(s => s.Length == 0))
				return false;

			if (!CheckMinWidth(meshData))
				return false;

			return adapter.Check(meshData);
		}

		private bool CheckMinWidth(BzMeshData meshData)
		{
			if (meshData.Vertices.Count < 3)
				return false;

			for (var i = 0; i < meshData.Vertices.Count; i++)
			{
				var pos = adapter.GetWorldPos(meshData, i);
				var dist = plane.GetDistanceToPoint(pos);
				if (Math.Abs(dist) > MinWidth)
					return true;
			}

			return false;
		}

		/// <summary>
		/// Get mesh triangle list from BzTriangle list
		/// </summary>
		static int[] MakeTriangleToList(List<BzTriangle> bzTriangles, List<BzTriangle> bzTrianglesExtra)
		{
			var triangles = new int[(bzTriangles.Count + bzTrianglesExtra.Count) * 3];
			for (var i = 0; i < bzTriangles.Count; ++i)
			{
				var tr = bzTriangles[i];
				var shift = i * 3;
				triangles[shift + 0] = tr.i1;
				triangles[shift + 1] = tr.i2;
				triangles[shift + 2] = tr.i3;
			}

			for (var i = 0; i < bzTrianglesExtra.Count; ++i)
			{
				var tr = bzTrianglesExtra[i];
				var shift = (bzTriangles.Count + i) * 3;
				triangles[shift + 0] = tr.i1;
				triangles[shift + 1] = tr.i2;
				triangles[shift + 2] = tr.i3;
			}

			return triangles;
		}
	}
}
