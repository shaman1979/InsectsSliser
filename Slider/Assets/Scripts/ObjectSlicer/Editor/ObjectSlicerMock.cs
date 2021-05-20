using BzKovSoft.ObjectSlicer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BzKovSoft.ObjectSlicer.Editor
{
	public class ObjectSlicerMock : BzSliceableObjectBase
	{
		protected override BzSliceTryData PrepareData(Plane plane)
		{
			GameObject o;
			var colliders = (o = gameObject).GetComponentInChildren<Collider>();

			return new BzSliceTryData()
			{
				componentManager = new StaticComponentManager(o, plane, colliders),
				plane = plane,
			};
		}

		protected override void OnSliceFinished(BzSliceTryResult result)
		{
		}
	}
}
