using UnityEngine;

namespace Slice.RedZoneSlicer
{
    public class RedZoneGeneratorMessage
    {
        public Vector3 FirstRedZone { get; }
        public Vector3 SecondRedZone { get; }

        public RedZoneGeneratorMessage(Vector3 firstRedZone, Vector3 secondRedZone)
        {
            FirstRedZone = firstRedZone;
            SecondRedZone = secondRedZone;
        }
    }
}