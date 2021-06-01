using System;
using System.Collections;
using Assets.Scripts.Tools;
using Level.Messages.Timer;
using LightDev.UI;
using NUnit.Framework;
using Slicer.EventAgregators;
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
            var timerModify = new LevelTimerActivatorModify(eventsAgregator, asyncHelper);
            timerModify.Apply();

            //assert
            Assert.IsTrue(isMessageReached);
        }

        [UnityTest]
        public IEnumerator WhenTimerApplyAndSubsctiberSignThenTimerValueReducedBy1()
        {
            //arrange
            IEventsAgregator eventsAgregator = new EventsAgregator();

            int currentTimer = 0;
            eventsAgregator.AddListener<TimerUpdateMessage>(message => currentTimer = message.Value);
            var asyncHelper = new GameObject("Async").AddComponent<AsyncHelper>();
            
            //act
            var startTime = 10;

            var timerModify = new LevelTimerActivatorModify(eventsAgregator, asyncHelper);
            timerModify.SetStartTime(startTime);
            timerModify.Apply();

            //assert
            Assert.AreEqual(startTime, currentTimer);
            yield return new WaitForSeconds(1.5f);
            Assert.AreEqual(1, startTime - currentTimer);
        }
    }
}