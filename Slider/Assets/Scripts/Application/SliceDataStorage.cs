using UnityEngine;

namespace Applications
{
    public static class SliceDataStorage
    {
        public static readonly Quaternion RedZoneRotation = Quaternion.Euler(-90f, 0f, -32f);
        public static readonly Vector3 SecondPointPosition = new Vector3(0.5f, 0f, 0f);
        public static readonly float HalfRedZoneOffset = 1.1f;
        public static readonly float HalfRedZoneWidth = 2f;
    }
}
