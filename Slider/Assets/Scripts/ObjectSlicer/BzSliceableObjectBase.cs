using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Threading;
using BzKovSoft.ObjectSlicer;

namespace BzKovSoft.ObjectSlicer
{
	/// <summary>
	/// Base class for sliceable object
	/// </summary>
	public abstract class BzSliceableObjectBase : BzSliceableBase
	{
		protected override AdapterAndMesh GetAdapterAndMesh(MeshFilter meshFilter)
		{
			if (meshFilter != null)
			{
				var result = new AdapterAndMesh();
				result.mesh = meshFilter.sharedMesh;
				result.adapter = new BzSliceMeshFilterAddapter(result.mesh.vertices, meshFilter.gameObject);
				return result;
			}

			return null;
		}
	}
}