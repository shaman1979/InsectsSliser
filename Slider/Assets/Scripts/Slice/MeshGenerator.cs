﻿using DG.Tweening;
using LightDev;
using LightDev.Core;
using MeshSlice;
using Slicer.Game;
using System;
using Level.Messages;
using Slice.Messages;
using Slicer.EventAgregators;
using Tools;
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

        [Inject] private IEventsAgregator eventsAgregator;

        [SerializeField]
        private Base objectToSlice;

        [SerializeField]
        private SlicebleItemMovening itemMovening;

        private SequenceHelper sequenceHelper;

        private void Start()
        {
            sequenceHelper = new SequenceHelper(objectToSlice.transform);
            
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
                eventsAgregator.Invoke(new NextMeshMessage());
                MeshSetup(mesh, () => OnFinished?.Invoke());
            }
            else
            {
                eventsAgregator.Invoke(new MeshEndMessage());
            }
        }

        private void MeshSetup(MeshInfo mesh, Action onFinished = null)
        {
            OnStarted?.Invoke(mesh);

            objectToSlice.GetComponent<MeshFilter>().mesh = mesh.Mesh;
            objectToSlice.GetComponent<MeshCollider>().sharedMesh = mesh.Mesh;

            objectToSlice.SetLocalPositionZ(0);
            objectToSlice.SetScale(0);
            objectToSlice.SetRotationY(mesh.Rotation.y - 180);
            objectToSlice.gameObject.Activate();
            
            sequenceHelper.Sequence(
                sequenceHelper.Scale(1.5f, 0.4f).SetEase(Ease.OutBack)
            );

            sequenceHelper.Sequence(
                sequenceHelper.RotateY(mesh.Rotation.y, 0.4f).SetEase(Ease.InSine),
                DOTween.Sequence().AppendCallback(() => onFinished?.Invoke())
            );
        }
    }
}