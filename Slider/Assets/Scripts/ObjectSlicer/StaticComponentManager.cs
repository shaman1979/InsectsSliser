using BzKovSoft.ObjectSlicer.MeshGenerator;
using System;
using UnityEngine;

namespace BzKovSoft.ObjectSlicer
{
    public class StaticComponentManager : IComponentManager
    {
        private readonly GameObject originalObject;
        private readonly Plane plane;
        private readonly ColliderSliceResult colliderResult;

        public bool Success => colliderResult != null;

        public StaticComponentManager(GameObject gameObject, Plane plane, Collider collider)
        {
            originalObject = gameObject;
            this.plane = plane;

            colliderResult = SliceColliders(plane, collider);
        }

        public void OnSlicedWorkerThread(SliceTryItem[] items)
        {
            if (colliderResult.SliceResult == SliceResult.Sliced)
            {
                colliderResult.SliceResult = colliderResult.MeshDissector.Slice();
            }
        }

        private static ColliderSliceResult SliceColliders(Plane plane, Collider collider)
        {
            var colliderM = collider as MeshCollider;

            ColliderSliceResult result;

            if (colliderM != null)
            {
                var mesh = UnityEngine.Object.Instantiate(colliderM.sharedMesh);
                result = PrepareSliceCollider(Vector3.zero, collider, mesh, plane);
            }
            else
                throw new NotSupportedException("Not supported collider type '" + collider.GetType().Name + "'");

            var colliderExistsNeg = result.SliceResult == SliceResult.Sliced | result.SliceResult == SliceResult.Neg;
            var colliderExistsPos = result.SliceResult == SliceResult.Sliced | result.SliceResult == SliceResult.Pos;
            
            var sliced = colliderExistsNeg & colliderExistsPos;
            return sliced ? result : null;
        }

        private static ColliderSliceResult PrepareSliceCollider(Vector3 localPosition, Collider collider, Mesh mesh,
            Plane plane)
        {
            var result = new ColliderSliceResult();
            var adapter = new BzSliceColliderAddapter(mesh.vertices, collider.gameObject);
            var meshDissector = new BzMeshDataDissector(mesh, plane, null, adapter, null);

            result.SliceResult = SliceResult.Sliced;
            result.MeshDissector = meshDissector;

            return result;
        }

        private class ColliderSliceResult
        {
            public BzMeshDataDissector MeshDissector;
            public SliceResult SliceResult;
        }
    }
}