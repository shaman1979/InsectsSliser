using Applications;
using UnityEngine;

namespace Slice.RedZoneSlicer.Factoryes
{
    public class HalfRedZoneFactory
    {
        [SerializeField]
        private float offsetHalf;

        public HalfRedZoneFactory(float offsetHalf)
        {
            this.offsetHalf = offsetHalf;
        }

        public void Create(out Vector3 firstPoint, out Vector3 secondPoint)
        {
            firstPoint = new Vector3(0f, offsetHalf + SliceDataStorage.HalfRedZoneOffset, 2f);
            secondPoint = SliceDataStorage.SecondPointPosition;
        }

        public float GetWidth()
        {
            return SliceDataStorage.HalfRedZoneWidth;
        }
    }
}