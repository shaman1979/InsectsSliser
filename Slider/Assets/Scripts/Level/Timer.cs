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
        private Coroutine timerCoroutine = null;

        public Timer(IEventsAgregator eventsAgregator, AsyncHelper asyncHelper)
        {
            this.asyncHelper = asyncHelper;
            this.eventsAgregator = eventsAgregator;
        }

        public void Initialize()
        {
            eventsAgregator.AddListener<TimerStartMessage>(message => StartTimer(message.StartTime));
            eventsAgregator.AddListener<GameFinishMessage>(message => StopTimer());
            eventsAgregator.AddListener<RestartTimerMessage>(message => Restart(message.RestartTime));
        }

        private void Restart(int restartTime)
        {
            if (timerCoroutine != null)
            {
                asyncHelper.StopCoroutine(timerCoroutine);
                timerCoroutine = null;
            }

            StartTimer(restartTime);
        }

        private void StartTimer(int startTime)
        {
            if (timerCoroutine != null)
            {
                Debug.LogWarning($"Таймер уже запущен");
                return;
            }
            
            currentTime = startTime;
            timerCoroutine = asyncHelper.StartCoroutine(TimerCountdown());
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
            Events.PostReset.Call();
            Events.GameFinish.Call();
            
            StopTimer();
        }

        private void StopTimer()
        {
            if (timerCoroutine != null)
            {
                asyncHelper.StopCoroutine(timerCoroutine);
                timerCoroutine = null;
            }
            
            eventsAgregator.Invoke(new TimerWindowDeactiveMessage());
        }

        private void TimerUpdate(int time)
        {
            eventsAgregator.Invoke(new TimerUpdateMessage(time));
        }
    }
}