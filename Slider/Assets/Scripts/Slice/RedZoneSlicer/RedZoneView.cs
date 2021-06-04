using Slicer.EventAgregators;
using UnityEngine;
using Zenject;

namespace Slice.RedZoneSlicer
{
    public class RedZoneView : MonoBehaviour
    {
        private const string RedZoneShaderPath = "Custom/SlicableRedZone";
        private readonly int firstRedZonePointId = Shader.PropertyToID("_RedZonePoint1");
        private readonly int secondRedZonePointId = Shader.PropertyToID("_RedZonePoint2");
        
        [SerializeField] private Material redZoneMaterial;

        private IEventsAgregator eventsAgregator;
        
        [Inject]
        public void Setup(IEventsAgregator eventsAgregator)
        {
            this.eventsAgregator = eventsAgregator;
        }

        public void Init()
        {
            eventsAgregator.AddListener<RedZoneGeneratorMessage>(message => Generate(message.FirstRedZone, message.SecondRedZone));
        }

        public Vector3 GetFirstVector()
        {
            return redZoneMaterial.GetVector(firstRedZonePointId);
        }

        public Vector3 GetSecondVector()
        {
            return redZoneMaterial.GetVector(secondRedZonePointId);
        }

        public void MaterialInitialize()
        {
            redZoneMaterial = new Material(Shader.Find(RedZoneShaderPath));
        }

        private void Generate(Vector3 firstRedPoint, Vector3 secondRedPoint)
        {
            transform.position = firstRedPoint;
            var secondPoint = SecondPointCreate(secondRedPoint);
            
            redZoneMaterial.SetVector(firstRedZonePointId, firstRedPoint);
            redZoneMaterial.SetVector(secondRedZonePointId, secondRedPoint);
        }

        private Transform SecondPointCreate(Vector3 secondRedPoint)
        {
            var secondPoint = new GameObject("SecondPoint");
            secondPoint.transform.parent = transform;
            secondPoint.transform.position = secondRedPoint;
            
            return secondPoint.transform;
        }
    }
}