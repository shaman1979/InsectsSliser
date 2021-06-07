using System;
using Slicer.EventAgregators;
using UnityEngine;
using View.Messages;
using Zenject;

namespace View
{
    [RequireComponent(typeof(MeshRenderer))]
    public class MaterialChanger : MonoBehaviour
    {
        private MeshRenderer meshRenderer;
        private IEventsAgregator eventsAgregator;

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
            if (eventsAgregator == null)
            {
                return;
            }

            meshRenderer = GetComponent<MeshRenderer>();
            eventsAgregator.AddListener<MaterialChangeMessage>(message => ChangeMaterial(message.CurrentMaterial));
        }

        public Material GetMaterial() => meshRenderer.material;
        
        private void ChangeMaterial(Material material)
        {
            meshRenderer.material = material;
        }
    }
}
