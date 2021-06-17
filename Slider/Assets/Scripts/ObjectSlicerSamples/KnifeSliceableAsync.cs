using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BzKovSoft.ObjectSlicer;
using System.Diagnostics;
using LightDev;

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
        private Knife knife;

        private IBzSliceableAsync sliceableAsync;

        public void StartSlice()
        {
            StartCoroutine(Slice(knife));
        }

        private void Start()
        {
            sliceableAsync = GetComponent<IBzSliceableAsync>();
            Events.RequestFinish += OnReset;
            Events.GameStart += OnReset;
        }

        private void OnReset()
        {
            knife = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Knife>(out var knife))
            {
                this.knife = knife;
            }
        }

        private IEnumerator Slice(Knife knife)
        {
            yield return null;

            if (knife != null)
            {
                var point = GetCollisionPoint(knife);
                var normal = Vector3.Cross(knife.MoveDirection, knife.BladeDirection);
                var plane = new Plane(normal, point);

                if (sliceableAsync != null)
                {
                    sliceableAsync.Slice(plane, knife.SliceID, null);
                }
            }
        }

        private Vector3 GetCollisionPoint(Knife knife)
        {
            var distToObject = transform.position - knife.Origin;
            var proj = Vector3.Project(distToObject, knife.BladeDirection);

            var collisionPoint = knife.Origin + proj;
            return collisionPoint;
        }
    }
}