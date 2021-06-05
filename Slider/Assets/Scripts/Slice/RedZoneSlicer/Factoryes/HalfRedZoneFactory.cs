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
            firstPoint = new Vector3(0f, offsetHalf, 2f);
            secondPoint = new Vector3(0.5f, 0f, 0f);
        }
    }
}