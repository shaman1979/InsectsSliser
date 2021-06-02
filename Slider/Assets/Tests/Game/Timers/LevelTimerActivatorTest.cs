using System.Collections;
using Applications.Messages;
using Assets.Scripts.Tools;
using Level.Messages.Timer;
using NUnit.Framework;
using Slicer.EventAgregators;
using Slicer.Game;
using Slicer.Levels;
using Slicer.Levels.Modifycations.Messages;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Game
{
    public class LevelTimerActivatorTest
    {
        private IEventsAgregator eventsAgregator;
        private AsyncHelper asyncHelper;
        private LevelTimerActivatorModify timerModify;
        
        [SetUp]
        public void Setup()
        {
            asyncHelper = new GameObject(nameof(asyncHelper)).AddComponent<AsyncHelper>();
            eventsAgregator = new EventsAgregator();
            timerModify = new LevelTimerActivatorModify();
        }
        
        [Test]
        public void WhenTimerModifyApplyAndSubscriberSignThenMessageShouldReach()
        {
            //arrange
            var isMessageReached = false;
            eventsAgregator.AddListener<TimerWindowActiveMessage>(message => isMessageReached = true);
            
            //act
            timerModify.Apply(eventsAgregator);

            //assert
            Assert.IsTrue(isMessageReached);
        }

        [UnityTest]
        public IEnumerator WhenTimerApplyAndTimerSubscribeThenTimerValueReducedBy1()
        {
            //arrange
            var currentTimer = 0;
            eventsAgregator.AddListener<TimerUpdateMessage>(message => currentTimer = message.Value);

            var timer = new Timer(eventsAgregator, asyncHelper);
            timer.Initialize();
            
            //act
            var startTime = 10;
            
            timerModify.SetStartTime(startTime);
            timerModify.Apply(eventsAgregator);

            //assert
            Assert.AreEqual(startTime, currentTimer);
            yield return new WaitForSeconds(1.5f);
            Assert.AreEqual(1, startTime - currentTimer);
        }

        [Test]
        public void WhenLevelTimerApply_AndTimerWindowShowMessageSubscribe_ThenWindowShow()
        {
            //Arrange
            bool isShow = false;
            
            eventsAgregator.AddListener<TimerWindowActiveMessage>(message => isShow = true);
            //Act
            timerModify.Apply(eventsAgregator);

            //Assert
            Assert.IsTrue(isShow);
        }

        [UnityTest]
        public IEnumerator WhenTimerEnd_AndSubscribeSing_ThenTimeShouldBeZero()
        {
            //Arrange
            var endTime = 0;
            var time = 0;

            var isFinish = false; 
            
            eventsAgregator.AddListener<TimerUpdateMessage>(message => time = message.Value);
            eventsAgregator.AddListener<GameFinishMessage>(message => isFinish = true);

            var timer = new Timer(eventsAgregator, asyncHelper);
            timer.Initialize();
            //Act
            var startTime = 1;

            timerModify.SetStartTime(startTime);
            timerModify.Apply(eventsAgregator);

            //Assert
            yield return new WaitForSeconds(2f);
            Assert.AreEqual(endTime, time);
            yield return new WaitForSeconds(1.5f);
            Assert.IsTrue(isFinish);
        }

        [TearDown]
        public void TearDown()
        {
            timerModify = null;
            Object.Destroy(asyncHelper.gameObject);
            eventsAgregator = null;
        }
    }
}