using Applications;
using Applications.Messages;
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
        private readonly int widthRedZoneId = Shader.PropertyToID("_RedZoneWidth");
        
        [SerializeField] private Material redZoneMaterial;

        private IEventsAgregator eventsAgregator;
        private Transform firstPoint;
        private Transform secondPoint;

        [Inject]
        public void Setup(IEventsAgregator eventsAgregator)
        {
            this.eventsAgregator = eventsAgregator;
        }

        public void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (eventsAgregator != null)
            {
                eventsAgregator.AddListener<RedZoneGeneratorMessage>(message =>
                    Generate(message.FirstRedZone, message.SecondRedZone, message.Width));
                
                eventsAgregator.AddListener<GameFinishMessage>(message => OnGameFinish());
            }
        }
        
        public Vector3 GetFirstVector()
        {
            return redZoneMaterial.GetVector(firstRedZonePointId);
        }

        public Vector3 GetSecondVector()
        {
            return redZoneMaterial.GetVector(secondRedZonePointId);
        }

        public Transform GetFirstPoint()
        {
            return firstPoint;
        }

        public Transform GetSecondPoint()
        {
            return secondPoint;
        }

        public float  GetLineWidth()
        {
            return redZoneMaterial.GetFloat(widthRedZoneId);
        }

        public void MaterialInitialize()
        {
            redZoneMaterial = new Material(Shader.Find(RedZoneShaderPath));
        }

        public void TransformReset()
        {
            firstPoint.position = Vector3.zero;
        }

        private void OnGameFinish()
        {
            transform.position = new Vector3(1000, 1000, 1000);
        }
        
        private void Generate(Vector3 firstRedPoint, Vector3 secondRedPoint, float widthLine)
        {
            firstPoint = transform;
            
            TransformReset();
            firstPoint.Translate(firstRedPoint);
            firstPoint.rotation = SliceDataStorage.RedZoneRotation;

            secondPoint = SecondPointCreate(secondRedPoint);
            
            redZoneMaterial.SetFloat(widthRedZoneId, widthLine);
            redZoneMaterial.SetVector(firstRedZonePointId, firstPoint.position);
            redZoneMaterial.SetVector(secondRedZonePointId, secondPoint.position);
        }

        private Transform SecondPointCreate(Vector3 secondRedPoint)
        {
            var secondPoint = new GameObject("SecondPoint");
            secondPoint.transform.parent = firstPoint;
            secondPoint.transform.localPosition = secondRedPoint;
            secondPoint.transform.localRotation = Quaternion.identity;
            
            return secondPoint.transform;
        }
    }
}