using System.Collections;
using Applications.Messages;
using Assets.Scripts.Tools;
using Level.Messages.Timer;
using LightDev;
using Slicer.EventAgregators;
using Slicer.Levels.Modifycations.Messages;
using UnityEngine;
using Zenject;

namespace Slicer.Game
{
    public class Timer : IInitializable
    {
        private readonly IEventsAgregator eventsAgregator;
        private readonly AsyncHelper asyncHelper;

        private int currentTime = 0;

        public Timer(IEventsAgregator eventsAgregator, AsyncHelper asyncHelper)
        {
            this.asyncHelper = asyncHelper;
            this.eventsAgregator = eventsAgregator;
        }

        public void Initialize()
        {
            eventsAgregator.AddListener<TimerStartMessage>(message => StartTimer(message.StartTime));
        }

        private void StartTimer(int startTime)
        {
            currentTime = startTime;
            asyncHelper.StartCoroutine(TimerCountdown());
        }

        private IEnumerator TimerCountdown()
        {
            while (currentTime >= 0)
            {
                TimerUpdate(currentTime);
                currentTime--;
                yield return new WaitForSeconds(1);
            }
            
            eventsAgregator.Invoke(new GameFinishMessage());
            eventsAgregator.Invoke(new TimerWindowDeactiveMessage());
            Events.PostReset.Call();
            Events.GameFinish.Call();
        }

        private void TimerUpdate(int time)
        {
            eventsAgregator.Invoke(new TimerUpdateMessage(time));
        }
    }
}