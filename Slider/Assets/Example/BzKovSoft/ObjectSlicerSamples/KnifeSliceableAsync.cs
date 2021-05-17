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
                Vector3 point = GetCollisionPoint(knife);
                Vector3 normal = Vector3.Cross(knife.MoveDirection, knife.BladeDirection);
                Plane plane = new Plane(normal, point);

                if (sliceableAsync != null)
                {
                    sliceableAsync.Slice(plane, knife.SliceID, null);
                }
            }
            else
            {
                Events.GameFinish.Call();
            }
        }

        private Vector3 GetCollisionPoint(Knife knife)
        {
            Vector3 distToObject = transform.position - knife.Origin;
            Vector3 proj = Vector3.Project(distToObject, knife.BladeDirection);

            Vector3 collisionPoint = knife.Origin + proj;
            return collisionPoint;
        }
    }
}