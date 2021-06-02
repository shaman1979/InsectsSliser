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
        private Text timerText;
        
        [SetUp]
        public void Setup()
        {
            timerWindow = new GameObject("TimerWindow").AddComponent<TimerWindow>();
            eventsAgregator = new EventsAgregator();
            timerText = new GameObject("TimerText").AddComponent<Text>();
        }
        
        [Test]
        [TestCase(40)]
        [TestCase(0)]
        [TestCase(-19999)]
        public void WhenTimerActive_AndTimerInteger_ThenTextChanged(int timerValue)
        {
            //Arrange
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
            Object.Destroy(timerText.gameObject);
            eventsAgregator = null;
        }
    }
}