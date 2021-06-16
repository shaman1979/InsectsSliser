using System.Collections;
using System.Collections.Generic;
using LightDev.UI;
using MeshSlice.UI;
using NUnit.Framework;
using Slicer.EventAgregators;
using Slicer.Game;
using Slicer.HP;
using Slicer.HP.Messages;
using UI.Elements;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests.Game
{
    public class HPTest
    {
        [Test]
        [TestCase(0.5f, 25)]
        [TestCase(2f, 0)]
        [TestCase(-1f, 0)]
        [TestCase(1f , 50)]
        public void WhenIncreaseProgress_AndCurrentProgressIsEmpty_ThenCurrentProgressChanged(float value, int result)
        {
            //arrange
            var hpInitializer = new HpInitializer(null, new EventsAgregator(), null);
            
            //act
            hpInitializer.IncreaseProgress(value);

            //assert
            Assert.AreEqual(result, hpInitializer.CurrentProgress);
        }

        [Test]
        [TestCase(50, 100,0.5f, "50/100")]
        [TestCase(10, 200,0.05f, "10/200")]
        [TestCase(0, 0,0f, "0/0")]
        public void WhenIncreaseProgress_AndMessagePublish_ThenGameWindowUIProgressUpdate(int currentProgress, int allProgress, float fillAmount, string amountText)
        {
            //Arrange
            var progressImage = new GameObject("ProgressImage").AddComponent<Image>();
            var progressText = new GameObject("ProgressText").AddComponent<Text>();
            var progressMove = new GameObject("UIMove").AddComponent<UIMove>();

            IEventsAgregator eventsAgregator = new EventsAgregator();
            var gameWindow = new GameObject("GameWindow").AddComponent<GameWindow>();
            gameWindow.Subscribe(eventsAgregator);

            gameWindow.Setup(null, progressText, progressImage, progressMove);

            //Act
            eventsAgregator.Invoke(new CurrentProgressMessage(currentProgress));
            eventsAgregator.Invoke(new MaxProgressMessage(allProgress));
            
            //Assert
            Assert.AreEqual(fillAmount,progressImage.fillAmount);
            Assert.AreEqual(amountText,progressText.GetText());
        }
    }
}
