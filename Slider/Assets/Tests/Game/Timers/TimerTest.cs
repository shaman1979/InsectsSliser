using System.Collections;
using Assets.Scripts.Tools;
using Level.Messages.Timer;
using NUnit.Framework;
using Slicer.EventAgregators;
using Slicer.Game;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Game
{
    public class TimerTest
    {
        private IEventsAgregator eventsAgregator;
        private AsyncHelper asyncHelper;
        
        [SetUp]
        public void Setup()
        {
            eventsAgregator = new EventsAgregator();
            asyncHelper = new GameObject(nameof(AsyncHelper)).AddComponent<AsyncHelper>();
        }
        
        [UnityTest]
        public IEnumerator WhenTimerStartMessagePublisher_AndTimerSubscribe_ThenCountdownOn()
        {
            //Arrange
            var resultTime = 11;
            

            var timer = new Timer(eventsAgregator, asyncHelper);
            timer.Initialize();

            eventsAgregator.AddListener<TimerUpdateMessage>(message => resultTime = message.Value);

            //Act
            var startTime = 10;
            eventsAgregator.Invoke(new TimerStartMessage(startTime));
            
            yield return new WaitForSeconds(1.5f);
            
            //Assert
            Assert.Less(resultTime, startTime);
        }
        
        [UnityTest]
        public IEnumerator WhenTimerRestartMessagePublisher_AndTimerSubscribe_ThenTimerRestart()
        {
            //Arrange
            var resultTime = 0;
            
            eventsAgregator.AddListener<TimerUpdateMessage>(message => resultTime = message.Value);
            var timer = new Timer(eventsAgregator, asyncHelper);
            timer.Initialize();
            
            //Act
            var startTime = 10;
            eventsAgregator.Invoke(new TimerStartMessage(startTime));

            yield return new WaitForSeconds(1.5f);
            
            var restartTime = 4;
            eventsAgregator.Invoke(new RestartTimerMessage(restartTime));

            //Assert
            Assert.AreEqual(resultTime, restartTime);
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(asyncHelper);
            eventsAgregator = null;
        }
    }
}