using DG.Tweening;
using LightDev;
using LightDev.Core;
using MeshSlice;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Slice
{
    public class MeshGenerator : MonoBehaviour
    {
        public event Action OnFinished;

        [SerializeField]
        private Base objectToSlice;

        [SerializeField]
        private SlicebleItemMovening itemMovening;

        private void Start()
        {
            itemMovening.OnMoveFinished += AnimateNextMesh;
            Events.GameStart += AnimateNextMesh;
        }

        private void AnimateNextMesh()
        {
            if (LevelsManager.HasNextMesh())
            {
                MeshInfo info = LevelsManager.GetNextMeshInfo();
                objectToSlice.GetComponent<MeshFilter>().mesh = info.mesh;
                objectToSlice.GetComponent<MeshCollider>().sharedMesh = info.mesh;

                objectToSlice.SetLocalPositionZ(0);
                objectToSlice.SetScale(0);
                objectToSlice.SetRotationY(info.rotation.y - 180);
                objectToSlice.Activate();
                objectToSlice.Sequence(
                  objectToSlice.Scale(1.5f, 0.4f).SetEase(Ease.OutBack)
                );

                objectToSlice.Sequence(
                    objectToSlice.RotateY(info.rotation.y, 0.4f).SetEase(Ease.InSine),
                    DOTween.Sequence().AppendCallback(() => OnFinished?.Invoke())
                );
            }
            else
            {
                Events.RequestFinish.Call();
            }
        }
    }
}