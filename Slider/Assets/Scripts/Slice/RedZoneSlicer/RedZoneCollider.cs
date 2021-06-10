using System;
using Applications.Messages;
using BzKovSoft.ObjectSlicerSamples;
using LightDev;
using Slicer.EventAgregators;
using UnityEngine;
using Zenject;

namespace Slice.RedZoneSlicer
{
    [RequireComponent(typeof(BoxCollider))]
    public class RedZoneCollider : MonoBehaviour
    {
        private IEventsAgregator eventsAgregator;
        private BoxCollider boxCollider;

        public Vector3 GetColliderSize => boxCollider.size;

        public Vector3 GetColliderCenter => boxCollider.center;
        public bool IsTrigger => boxCollider.isTrigger;

        [Inject]
        public void Setup(IEventsAgregator eventsAgregator)
        {
            this.eventsAgregator = eventsAgregator;
        }

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (eventsAgregator != null)
                eventsAgregator.AddListener<RedZoneGeneratorMessage>(message => SetSize(3, message.Width, 1));

            boxCollider = GetComponent<BoxCollider>();
            boxCollider.isTrigger = true;
            SetCenter(new Vector3(0f, 0f, -2.20f));
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Knife>(out var knife))
            {
                GameFinish();
            }
        }

        private void SetCenter(Vector3 position)
        {
            boxCollider.center = position;
        }

        private void SetSize(float x, float y, float z)
        {
            boxCollider.size = new Vector3(x, y, z);
        }

        private void GameFinish()
        {
            Events.GameFinish.Call();
            Events.PostReset.Call();
            eventsAgregator.Invoke(new GameFinishMessage());
        }
    }
}