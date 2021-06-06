using System;
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
            SetCenter(Vector3.zero);
        }

        private void SetCenter(Vector3 position)
        {
            boxCollider.center = position;
        }
        
        private void SetSize(float x, float y, float z)
        {
            boxCollider.size = new Vector3(x, y, z);
        }
    }
}