using DG.Tweening;
using LightDev;
using LightDev.Core;
using MeshSlice;
using Slicer.Game;
using System;
using UnityEngine;
using Zenject;

namespace Slicer.Slice
{
    public class MeshGenerator : MonoBehaviour
    {
        public event Action OnFinished;

        [Inject]
        private LevelsInitializer levelsInitializer;

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
            if (levelsInitializer.TryNextMesh(out var mesh))
            {                
                objectToSlice.GetComponent<MeshFilter>().mesh = mesh.mesh;
                objectToSlice.GetComponent<MeshCollider>().sharedMesh = mesh.mesh;

                objectToSlice.SetLocalPositionZ(0);
                objectToSlice.SetScale(0);
                objectToSlice.SetRotationY(mesh.rotation.y - 180);
                objectToSlice.Activate();
                objectToSlice.Sequence(
                  objectToSlice.Scale(1.5f, 0.4f).SetEase(Ease.OutBack)
                );

                objectToSlice.Sequence(
                    objectToSlice.RotateY(mesh.rotation.y, 0.4f).SetEase(Ease.InSine),
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