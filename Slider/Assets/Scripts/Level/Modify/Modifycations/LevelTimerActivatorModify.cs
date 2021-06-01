using System;
using System.Collections;
using Assets.Scripts.Tools;
using Slicer.EventAgregators;
using Slicer.Levels.Modifycations.Messages;
using UnityEngine;

namespace Slicer.Levels
{
    public class LevelTimerActivatorModify : ILevelModify
    {
        [SerializeField] private int startTime = 10;
        
        private readonly IEventsAgregator eventsAgregator;
        private readonly AsyncHelper asyncHelper;

        private int currentTime = 0;
        
        public LevelTimerActivatorModify(IEventsAgregator eventsAgregator, AsyncHelper asyncHelper)
        {
            this.asyncHelper = asyncHelper;
            this.eventsAgregator = eventsAgregator;
        }

        public void SetStartTime(int time)
        {
            startTime = time;
        }

        public void Apply()
        {
            eventsAgregator.Invoke(new TimerWindowActiveMessage());
            StartTimer();
        }

        private void StartTimer()
        {
            currentTime = startTime;
            asyncHelper.StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            while (currentTime >= 0)
            {
                TimerUpdate(currentTime);
                currentTime--;
                yield return new WaitForSeconds(1);
            }
        }
            

        private void TimerUpdate(int time)
        {
            eventsAgregator.Invoke(new TimerUpdateMessage(time));
        }
    }
}