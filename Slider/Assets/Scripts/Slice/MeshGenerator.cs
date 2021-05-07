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
        public event Action<MeshInfo> OnStarted;

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
            Events.PostReset += FirstMeshInitialize;
        }

        private void FirstMeshInitialize()
        {
            MeshSetup(levelsInitializer.GetFirstMesh());
        }

        private void AnimateNextMesh()
        {
            if (levelsInitializer.TryNextMesh(out var mesh))
            {
                OnStarted?.Invoke(mesh);
                MeshSetup(mesh, () => OnFinished?.Invoke());
            }
            else
            {
                Events.RequestFinish.Call();
            }
        }

        private void MeshSetup(MeshInfo mesh, Action onFinished = null)
        {
            objectToSlice.GetComponent<MeshFilter>().mesh = mesh.Mesh;
            objectToSlice.GetComponent<MeshCollider>().sharedMesh = mesh.Mesh;

            objectToSlice.SetLocalPositionZ(0);
            objectToSlice.SetScale(0);
            objectToSlice.SetRotationY(mesh.Rotation.y - 180);
            objectToSlice.Activate();
            objectToSlice.Sequence(
              objectToSlice.Scale(1.5f, 0.4f).SetEase(Ease.OutBack)
            );

            objectToSlice.Sequence(
                objectToSlice.RotateY(mesh.Rotation.y, 0.4f).SetEase(Ease.InSine),
                DOTween.Sequence().AppendCallback(() => onFinished?.Invoke())
            );
        }
    }
}