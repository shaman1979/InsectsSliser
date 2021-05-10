using LightDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Levels
{
    public static class LevelModifyEvents
    {
        public static Event<float> SpeedChanged;

        static LevelModifyEvents()
        {
            SpeedChanged = new Event<float>(nameof(SpeedChanged));
        }
    }
}