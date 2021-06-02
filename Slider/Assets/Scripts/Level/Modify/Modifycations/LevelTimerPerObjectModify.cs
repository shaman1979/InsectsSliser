using Slicer.EventAgregators;
using Slicer.Game;
using Slicer.Levels;
using Slicer.Levels.Modifycations.Messages;
using UnityEngine;

namespace Level.Modify.Modifycations
{
    public class LevelTimerPerObjectModify : ILevelModify
    {
        [SerializeField, Min(0)]
        private int startTime;

        public void Apply(IEventsAgregator eventAgregator)
        {
            eventAgregator.Invoke(new TimerWindowActiveMessage());
            eventAgregator.Invoke(new TimerStartMessage(startTime));
        }

        public void SetStartTimer(int startTime)
        {
            this.startTime = startTime;
        }
    }
}