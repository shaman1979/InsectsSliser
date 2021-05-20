using System.Collections;
using System.Collections.Generic;
using LightDev.UI;
using NUnit.Framework;
using Slicer.Application.Storages;
using Slicer.DebugMode;
using Slicer.DebugMode.Messages;
using Slicer.EventAgregators;
using Slicer.UI.Elements;
using Slicer.UI.Windows;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class DebugModeTest
    {
        [Test]
        public void WhenButtonClick_AndDataIsNotEmpty_ThenProgressDataClear()
        {
            var xp = PlayerPrefs.GetInt(PlayerPrefsKeyStorage.XP);
            var level = PlayerPrefs.GetInt(PlayerPrefsKeyStorage.LEVEL);
            var stars = PlayerPrefs.GetInt(PlayerPrefsKeyStorage.STARS);

            //Act
            var button = new GameObject("Button").AddComponent<ButtonElement>();
            button.AddListener(ProgressDataReceiver.RemoveProgress);
            button.Click();

            //Assert
            Assert.AreEqual(0, PlayerPrefs.GetInt(PlayerPrefsKeyStorage.XP));
            Assert.AreEqual(0, PlayerPrefs.GetInt(PlayerPrefsKeyStorage.LEVEL));
            Assert.AreEqual(0, PlayerPrefs.GetInt(PlayerPrefsKeyStorage.STARS));

            ProgressDataReceiver.SetProgressData(level, stars, xp);
        }

        [Test]
        public void WhenDebugModeActive_AndMessagePublish_ThenDebugWindowActive()
        {
            //arrange
            EventsAgregator eventAgregator = new EventsAgregator();
            var debugWindow = new GameObject("DebugWindow").AddComponent<DebugWindow>();
            debugWindow.gameObject.SetActive(true);
            
            //act
            debugWindow.Subscribe(eventAgregator);
            eventAgregator.Invoke(new DebugModeActiveMessage());
            
            //assert
            Assert.IsTrue(debugWindow.gameObject.activeSelf);
        }
    }
}