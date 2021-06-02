using System.Collections;
using Level.Messages.Timer;
using NUnit.Framework;
using Slicer.EventAgregators;
using Slicer.Levels.Modifycations.Messages;
using Slicer.UI.Windows;
using UI.Elements;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests.Game
{
    public class TimerWindowTest
    {
        private IEventsAgregator eventsAgregator;
        private TimerWindow timerWindow;
        
        [SetUp]
        public void Setup()
        {
            timerWindow = new GameObject("TimerWindow").AddComponent<TimerWindow>();
            eventsAgregator = new EventsAgregator();
        }
        
        [Test]
        [TestCase(40)]
        [TestCase(0)]
        [TestCase(-19999)]
        public void WhenTimerActive_AndTimerInteger_ThenTextChanged(int timerValue)
        {
            //Arrange
            var timerText = new GameObject("TimerText").AddComponent<Text>();
            timerWindow.Setup(timerText);
            timerWindow.Subscribe(eventsAgregator);
            //Act
            eventsAgregator.Invoke(new TimerUpdateMessage(timerValue));

            var result = int.Parse(timerText.GetText());
            //Assert
            Assert.AreEqual(timerValue, result);
        }

        [UnityTest]
        public IEnumerator WhenTimeActive_AndTimerWindowShow_ThenTimeWindowHide()
        {
            //Arrange
            var timerText = new GameObject("TimerText").AddComponent<Text>();
            
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
        public void WhenTimerActive_AndTimerWindowHide_ThenTimerWindowShow()
        {
            //Arrange
            var timerText = new GameObject("Timer").AddComponent<Text>();
            
            timerWindow.Setup(timerText);
            timerWindow.Subscribe(eventsAgregator);
            timerWindow.gameObject.SetActive(false);

            //Act
            eventsAgregator.Invoke(new TimerWindowActiveMessage());

            //Assert
            Assert.IsTrue(timerWindow.gameObject.activeSelf);
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(timerWindow.gameObject);
            eventsAgregator = null;
        }
    }
}