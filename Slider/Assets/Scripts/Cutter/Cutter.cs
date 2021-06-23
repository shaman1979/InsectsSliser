using Applications.Messages;
using UnityEngine;
using LightDev;
using LightDev.Core;
using DG.Tweening;
using SliceFramework;
using Slicer.Cutter;
using BzKovSoft.ObjectSlicerSamples;
using Slicer.EventAgregators;
using Slicer.Slice;
using Zenject;

namespace MeshSlice
{
    public class Cutter : Base
    {
        public Base cutter;

        public KnifeSliceableAsync objectToSlice;

        [Header("Slice Parameters")] public Transform slicePoint;

        [SerializeField] private MeshGenerator generator;

        [SerializeField] private CutterMovening cutterMovening;

        [SerializeField] private ParticalActivator particalActivator;

        [Inject] private IEventsAgregator eventsAgregator;
        
        private bool canCut;

        private void Awake()
        {
            Events.PointerUp += OnPointerUp;
            Events.PostReset += OnPostReset;

            generator.OnFinished += OnReset;
            cutterMovening.OnFinished += EnableCut;
            eventsAgregator.AddListener<GameFinishMessage>(message => ResetCutter());
        }

        private void ResetCutter()
        {
            cutterMovening.StartPositionMove();
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
            cutterMovening.StopMovening();
            cutterMovening.StartPositionMove();
        }

        private void EnableCut()
        {
            canCut = true;
        }

        private void OnReset()
        {
            cutterMovening.StartMovening();
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
            cutterMovening.StopMovening();

            cutterMovening.SequenceHelper.KillSequences();
            cutterMovening.SequenceHelper.Sequence(
                cutterMovening.SequenceHelper.MoveY(2.5f, 0.3f).SetEase(Ease.InSine),
                cutterMovening.SequenceHelper.MoveY(0, 0.5f).SetEase(Ease.InSine),
                cutterMovening.SequenceHelper.OnFinish(() =>
                {
                    objectToSlice.StartSlice();
                    particalActivator.Activate();
                }),
                cutterMovening.SequenceHelper.MoveY(2, 0.6f).SetEase(Ease.InOutQuad)
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