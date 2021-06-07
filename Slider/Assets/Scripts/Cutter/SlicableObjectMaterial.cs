using Slicer.EventAgregators;
using Slicer.Slice;
using UnityEngine;
using View.Messages;
using Zenject;

namespace MeshSlice
{
    [ExecuteAlways]
    public class SlicableObjectMaterial : MonoBehaviour
    {
        public Material material;

        [Header("Points")]
        public Transform firstPoint;
        public Transform secondPoint;

        [SerializeField]
        private Texture2D texture;

        [SerializeField]
        private MeshGenerator meshGenerator;

        private readonly int firstPointId = Shader.PropertyToID("_Point1");
        private readonly int secondPointId = Shader.PropertyToID("_Point2");

        private readonly int mainTextureId = Shader.PropertyToID("_MainTex");
        private IEventsAgregator eventsAgregator;

        [Inject]
        private void Setup(IEventsAgregator eventsAgregator)
        {
            this.eventsAgregator = eventsAgregator;
        }
        
        private void Awake()
        {
            eventsAgregator.AddListener<MaterialChangeMessage>(message => MaterialChange(message.CurrentMaterial));
            meshGenerator.OnStarted += mesh => texture = mesh.Texture;
        }

        private void MaterialChange(Material currentMaterial)
        {
            material = currentMaterial;
        }

        private void Update()
        {
            if (firstPoint && secondPoint && texture)
            {
                material.SetVector(firstPointId, firstPoint.position);
                material.SetVector(secondPointId, secondPoint.position);

                material.SetTexture(mainTextureId, texture);
            }
        }
    }
}
