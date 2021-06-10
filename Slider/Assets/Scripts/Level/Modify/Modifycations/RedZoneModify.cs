using Slice.RedZoneSlicer;
using Slice.RedZoneSlicer.Factoryes;
using Slicer.EventAgregators;
using Slicer.Levels;
using UnityEngine;

namespace Level.Modify.Modifycations
{
    public class RedZoneModify : ILevelModify
    {
        [SerializeField]
        private HalfRedZoneFactory factory;

        private Vector3 firstPoint;
        private Vector3 secondPoint;

        public void Dispose()
        {
        }

        public void Apply(IEventsAgregator eventAgregator)
        {
            factory.Create(out firstPoint, out secondPoint);
            eventAgregator.Invoke(new RedZoneGeneratorMessage(firstPoint, secondPoint, factory.GetWidth()));
        }

        public void SetFactory(HalfRedZoneFactory zoneFactory)
        {
            factory = zoneFactory;
        }

        public Vector3 GetFirstPoint()
        {
            return firstPoint;
        }

        public Vector3 GetSecondPoint()
        {
            return secondPoint;
        }
    }
}