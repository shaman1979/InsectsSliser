using UnityEngine;
using LightDev;
using LightDev.Core;
using DG.Tweening;
using SliceFramework;
using Slicer.Cutter;
using BzKovSoft.ObjectSlicerSamples;
using Slicer.Slice;

namespace MeshSlice
{
    public class Cutter : Base
    {
        [Header("Cutter")]
        public Base cutter;
        public float minCutterLocalPosZ;
        public float maxCutterLocalPosZ;

        [Header("Object To Slice")]
        public GameObject objectToSlice;
        public float objectToSliceMinLocalPosZ;
        public float objectToSliceMaxLocalPosZ;

        [Header("Sliced Objects")]
        public Transform slicedObjectLeftPos;
        public Transform slicedObjectRightPos;

        [Header("Slice Parameters")]
        public Transform slicePoint;
        public Material material;

        [SerializeField]
        private CutterMovening movening;

        [SerializeField]
        private BzKnife knife;

        [SerializeField]
        private MeshGenerator generator;

        private bool canCut;

        private void OnValidate()
        {
            maxCutterLocalPosZ = Mathf.Max(minCutterLocalPosZ, maxCutterLocalPosZ);
            objectToSliceMaxLocalPosZ = Mathf.Max(objectToSliceMinLocalPosZ, objectToSliceMaxLocalPosZ);
        }

        private void Awake()
        {
            Events.PointerUp += OnPointerUp;
            Events.PostReset += OnPostReset;

            generator.OnFinished += OnReset;
        }

        private void OnDestroy()
        {
            Events.PointerUp -= OnPointerUp;
            Events.PostReset -= OnPostReset;
            generator.OnFinished -= OnReset;
        }

        private void Update()
        {
            if (canCut)
            {
                //float cutterLocalZ = cutter.GetLocalPositionZ() - InputManager.GetHorizontal();
                //cutterLocalZ = Mathf.Clamp(cutterLocalZ, minCutterLocalPosZ, maxCutterLocalPosZ);

                //cutter.MoveLocalZ(maxCutterLocalPosZ, 1f * Time.deltaTime).Loops();
                //cutter.SetLocalPositionZ(cutterLocalZ);
            }
        }

        private void OnPostReset()
        {
            canCut = false;
            movening.StopMovening();
            objectToSlice.SetActive(false);
        }

        private void OnReset()
        {
            canCut = true; movening.StartMovening();
        }

        private void OnPointerUp()
        {
            if (canCut)
            {
                AnimateCut();
            }
        }

        private void AnimateNextMesh()
        {
            if (LevelsManager.HasNextMesh())
            {
                MeshInfo info = LevelsManager.GetNextMeshInfo();
                objectToSlice.GetComponent<MeshFilter>().mesh = info.mesh;
                objectToSlice.GetComponent<MeshCollider>().sharedMesh = info.mesh;
                Base b = objectToSlice.GetComponent<Base>();
                b.SetLocalPositionZ(0);
                b.SetScale(0);
                b.SetRotationY(info.rotation.y - 180);
                b.Activate();
                b.Sequence(
                  b.Scale(1.5f, 0.4f).SetEase(Ease.OutBack)
                );
                b.Sequence(
                  b.RotateY(info.rotation.y, 0.4f).SetEase(Ease.InSine),
                  OnFinish(() => { canCut = true; movening.StartMovening(); })
                );
            }
            else
            {
                Events.RequestFinish.Call();
            }
        }

        private void AnimateCut()
        {
            canCut = false;
            movening.StopMovening();
            SlicedHull hull = objectToSlice.Slice(slicePoint.position, slicePoint.up, material);

            cutter.KillSequences();
            cutter.Sequence(
              cutter.MoveY(2.5f, 0.3f).SetEase(Ease.InSine),
              cutter.MoveY(0, 0.5f).SetEase(Ease.InSine),
              cutter.OnFinish(() =>
              {
                  objectToSlice.SetActive(false);
                  knife.BeginNewSlice();
                  //Cut(hull);
              }),
              cutter.MoveY(2, 0.6f).SetEase(Ease.InOutQuad),
              cutter.OnFinish(() =>
              {
                  if (hull == null)
                  {
                      canCut = true;
                      movening.StartMovening();
                  }
              })
            );
        }

#if UNITY_EDITOR
        /**
         * This is for Visual debugging purposes in the editor 
         */
        public void OnDrawGizmos()
        {
            if (slicePoint == null) return;

            SliceFramework.Plane cuttingPlane = new SliceFramework.Plane();
            cuttingPlane.Compute(slicePoint);
            cuttingPlane.OnDebugDraw();
        }
#endif
    }
}
