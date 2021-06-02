using System;
using System.Collections;
using Assets.Scripts.Tools;
using Slicer.EventAgregators;
using Slicer.Game;
using Slicer.Levels.Modifycations.Messages;
using UnityEngine;

namespace Slicer.Levels
{
    public class LevelTimerActivatorModify : ILevelModify
    {
        [SerializeField, Min(0)] private int startTime = 10;
        
        public void SetStartTime(int time)
        {
            startTime = time;
        }

        public void Apply(IEventsAgregator eventAgregator)
        {
            eventAgregator.Invoke(new TimerWindowActiveMessage());
            eventAgregator.Invoke(new TimerStartMessage(startTime));
        }

        public void Dispose()
        {
        }
    }
}