﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BzKovSoft.ObjectSlicer;
using System.Diagnostics;

namespace BzKovSoft.ObjectSlicerSamples
{
	/// <summary>
	/// This script will invoke slice method of IBzSliceableAsync interface if knife slices this GameObject.
	/// The script must be attached to a GameObject that have rigidbody on it and
	/// IBzSliceable implementation in one of its parent.
	/// </summary>
	[DisallowMultipleComponent]
	[RequireComponent(typeof(IBzSliceableAsync))]
	public class KnifeSliceableAsync : MonoBehaviour
	{
		private BzKnife knife;

		private IBzSliceableAsync sliceableAsync;
		
		public void StartSlice()
        {
			StartCoroutine(Slice(knife));
		}

		private void Start()
		{
			sliceableAsync = GetComponent<IBzSliceableAsync>();
		}

		private void OnTriggerEnter(Collider other)
		{
			if(other.gameObject.TryGetComponent<BzKnife>(out var knife))
            {
				this.knife = knife;
            }
		}

		private IEnumerator Slice(BzKnife knife)
		{
			yield return null;

			Vector3 point = GetCollisionPoint(knife);
			Vector3 normal = Vector3.Cross(knife.MoveDirection, knife.BladeDirection);
			Plane plane = new Plane(normal, point);

			if (sliceableAsync != null)
			{
				sliceableAsync.Slice(plane, knife.SliceID, null);
			}
		}

		private Vector3 GetCollisionPoint(BzKnife knife)
		{
			Vector3 distToObject = transform.position - knife.Origin;
			Vector3 proj = Vector3.Project(distToObject, knife.BladeDirection);

			Vector3 collisionPoint = knife.Origin + proj;
			return collisionPoint;
		}
	}
}