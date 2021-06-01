using System.Collections;
using Applications.Messages;
using Assets.Scripts.Tools;
using Level.Messages.Timer;
using NUnit.Framework;
using Slicer.EventAgregators;
using Slicer.Game;
using Slicer.Levels;
using Slicer.Levels.Modifycations.Messages;
using Slicer.UI.Windows;
using UI.Elements;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests.Game
{
    public class TimerActivatorTest
    {
        [Test]
        [TestCase(40)]
        [TestCase(0)]
        [TestCase(-19999)]
        public void WhenTimerActive_AndTimerInteger_ThenTextChanged(int timerValue)
        {
            //Arrange
            IEventsAgregator eventsAgregator = new EventsAgregator();

            var timerMenu = new GameObject("TimerMenu").AddComponent<TimerWindow>();
            var timerText = new GameObject("TimerText").AddComponent<Text>();
            timerMenu.Setup(timerText);

            timerMenu.Subscribe(eventsAgregator);
            //Act
            eventsAgregator.Invoke(new TimerUpdateMessage(timerValue));

            var result = int.Parse(timerText.GetText());
            //Assert
            Assert.AreEqual(timerValue, result);
        }

        [Test]
        public void WhenTimerActive_AndTimerWindowHide_ThenTimerWindowShow()
        {
            //Arrange
            var timerWindow = new GameObject("TimerMenu").AddComponent<TimerWindow>();
            var timerText = new GameObject("Timer").AddComponent<Text>();

            IEventsAgregator eventsAgregator = new EventsAgregator();

            timerWindow.Setup(timerText);
            timerWindow.Subscribe(eventsAgregator);
            timerWindow.gameObject.SetActive(false);

            //Act
            eventsAgregator.Invoke(new TimerWindowActiveMessage());

            //Assert
            Assert.IsTrue(timerWindow.gameObject.activeSelf);
        }

        [UnityTest]
        public IEnumerator WhenTimeActive_AndTimerWindowShow_ThenTimeWindowHide()
        {
            //Arrange
            var timerWindow = new GameObject("TimerWindow").AddComponent<TimerWindow>();
            var timerText = new GameObject("TimerText").AddComponent<Text>();

            IEventsAgregator eventsAgregator = new EventsAgregator();
            timerWindow.Setup(timerText);
            timerWindow.Subscribe(eventsAgregator);
            timerWindow.gameObject.SetActive(true);

            //Act
            eventsAgregator.Invoke(new TimerWindowDeactiveMessage());

            yield return null;

            //Assert
            Assert.IsFalse(timerWindow.gameObject.activeSelf);
        }

        [Test]
        public void WhenTimerModifyApplyAndSubscriberSignThenMessageShouldReach()
        {
            //arrange
            var isMessageReached = false;
            IEventsAgregator eventsAgregator = new EventsAgregator();

            eventsAgregator.AddListener<TimerWindowActiveMessage>(message => isMessageReached = true);

            var asyncHelper = new GameObject("Async").AddComponent<AsyncHelper>();

            //act
            var timerModify = new LevelTimerActivatorModify();
            timerModify.Apply(eventsAgregator);

            //assert
            Assert.IsTrue(isMessageReached);
        }

        [UnityTest]
        public IEnumerator WhenTimerApplyAndTimerSubscribeThenTimerValueReducedBy1()
        {
            //arrange
            IEventsAgregator eventsAgregator = new EventsAgregator();

            var currentTimer = 0;
            eventsAgregator.AddListener<TimerUpdateMessage>(message => currentTimer = message.Value);
            var asyncHelper = new GameObject("Async").AddComponent<AsyncHelper>();

            var timer = new Timer(eventsAgregator, asyncHelper);
            timer.Initialize();
            
            //act
            var startTime = 10;

            var timerModify = new LevelTimerActivatorModify();
            timerModify.SetStartTime(startTime);
            timerModify.Apply(eventsAgregator);

            //assert
            Assert.AreEqual(startTime, currentTimer);
            yield return new WaitForSeconds(1.5f);
            Assert.AreEqual(1, startTime - currentTimer);
        }

        [UnityTest]
        public IEnumerator WhenTimerEnd_AndSubscribeSing_ThenTimeShouldBeZero()
        {
            //Arrange
            var endTime = 0;
            var time = 0;

            var isFinish = false; 
            
            IEventsAgregator eventsAgregator = new EventsAgregator();

            eventsAgregator.AddListener<TimerUpdateMessage>(message => time = message.Value);
            eventsAgregator.AddListener<GameFinishMessage>(message => isFinish = true);
            var asyncHelper = new GameObject("Async").AddComponent<AsyncHelper>();

            var timer = new Timer(eventsAgregator, asyncHelper);
            timer.Initialize();
            //Act
            var startTime = 1;

            var timerModify = new LevelTimerActivatorModify();
            timerModify.SetStartTime(startTime);
            timerModify.Apply(eventsAgregator);

            //Assert
            yield return new WaitForSeconds(2f);
            Assert.AreEqual(endTime, time);
            yield return new WaitForSeconds(1.5f);
            Assert.IsTrue(isFinish);
        }
    }
}