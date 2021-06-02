using System.Collections;
using System.Collections.Generic;
using Slicer.EventAgregators;
using UnityEngine;

namespace Slicer.Levels
{
    public class LevelKnifeSpeedModify : ILevelModify
    {
        [SerializeField]
        private float acceleration;

        public void Apply(IEventsAgregator eventAgregator)
        {
            LevelModifyEvents.SpeedChanged.Call(acceleration);
        }

        public void Dispose()
        {
        }
    }
}