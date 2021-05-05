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
        public Base cutter;

        public KnifeSliceableAsync objectToSlice;

        [Header("Slice Parameters")]
        public Transform slicePoint;
        public Material material;

        [SerializeField]
        private CutterMovening movening;

        [SerializeField]
        private MeshGenerator generator;

        [SerializeField]
        private CutterMovening cutterMovening;

        private bool canCut;

        private void Awake()
        {
            Events.PointerUp += OnPointerUp;
            Events.PostReset += OnPostReset;

            generator.OnFinished += OnReset;
            cutterMovening.OnFinished += EnableCut;
        }

        private void OnDestroy()
        {
            generator.OnFinished -= OnReset;

            Events.PointerUp -= OnPointerUp;
            Events.PostReset -= OnPostReset;
            cutterMovening.OnFinished -= EnableCut;
        }

        private void OnPostReset()
        {
            canCut = false;
            movening.StopMovening();
        }

        private void EnableCut()
        {
            canCut = true;
        }

        private void OnReset()
        {
            movening.StartMovening();
        }

        private void OnPointerUp()
        {
            if (canCut)
            {
                AnimateCut();
            }
        }

        private void AnimateCut()
        {
            canCut = false;
            movening.StopMovening();
            SlicedHull hull = objectToSlice.gameObject.Slice(slicePoint.position, slicePoint.up, material);

            cutter.KillSequences();
            cutter.Sequence(
              cutter.MoveY(2.5f, 0.3f).SetEase(Ease.InSine),
              cutter.MoveY(0, 0.5f).SetEase(Ease.InSine),
              cutter.OnFinish(() =>
              {
                  objectToSlice.StartSlice();
              }),
              cutter.MoveY(2, 0.6f).SetEase(Ease.InOutQuad)
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
