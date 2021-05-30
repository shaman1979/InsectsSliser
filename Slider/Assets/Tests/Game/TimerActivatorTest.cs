using System;
using System.Collections;
using LightDev.UI;
using NUnit.Framework;
using Slicer.EventAgregators;
using Slicer.Levels.Modifycations.Messages;
using Slicer.UI.Windows;
using UnityEngine;
using UnityEngine.TestTools;

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
            var timerText = new GameObject("TimerText").AddComponent<BaseText>();
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
            var timerText = new GameObject("Timer").AddComponent<BaseText>();

            IEventsAgregator eventsAgregator = new EventsAgregator();
            
            timerWindow.Setup(timerText);
            timerWindow.Subscribe(eventsAgregator);
            timerWindow.gameObject.SetActive(false);

            //Act
            eventsAgregator.Invoke(new TimerWindowActiveMessage());
            
            //Assert
            Assert.IsTrue(timerWindow.gameObject.activeSelf);
        }
    }
}