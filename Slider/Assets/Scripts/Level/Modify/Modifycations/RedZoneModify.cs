using Slice.RedZoneSlicer;
using Slicer.EventAgregators;
using Slicer.Levels;
using UnityEngine;

namespace Level.Modify.Modifycations
{
    public class RedZoneModify : ILevelModify
    {
        public void Dispose()
        {
        }

        public void Apply(IEventsAgregator eventAgregator)
        {
            var firstRedZone = new Vector3(0f, 2f, 0f);
            var secondRedZone = new Vector3(0.5f, 0f,0f);
            
            eventAgregator.Invoke(new RedZoneGeneratorMessage(firstRedZone, secondRedZone));
        }
    }
}