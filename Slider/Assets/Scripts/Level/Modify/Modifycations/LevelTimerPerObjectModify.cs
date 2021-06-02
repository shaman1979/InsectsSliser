using System;
using Level.Messages.Timer;
using Slice.Messages;
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

        private IEventsAgregator eventsAgregator;
        
        public void Apply(IEventsAgregator eventAgregator)
        {
            eventAgregator.Invoke(new TimerWindowActiveMessage());
            eventAgregator.Invoke(new TimerStartMessage(startTime));
            
            eventAgregator.AddListener<NextMeshMessage>(RestartMesh);

            this.eventsAgregator = eventAgregator;
        }

        private void RestartMesh(NextMeshMessage message)
        {
            eventsAgregator.Invoke(new RestartTimerMessage(startTime));
        }

        public void SetStartTimer(int startTime)
        {
            this.startTime = startTime;
        }

        public void Dispose()
        {
            eventsAgregator.RemoveListener<NextMeshMessage>(RestartMesh);
        }
    }
}