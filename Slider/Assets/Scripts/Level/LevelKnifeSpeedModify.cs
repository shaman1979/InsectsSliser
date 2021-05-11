using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Levels
{
    public class LevelKnifeSpeedModify : ILevelModify
    {
        [SerializeField]
        private float acceleration;

        public void Apply()
        {
            LevelModifyEvents.SpeedChanged.Call(acceleration);
        }
    }
}