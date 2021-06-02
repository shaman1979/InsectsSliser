using System.Collections;
using Assets.Scripts.Tools;
using Level.Messages.Timer;
using Level.Modify.Modifycations;
using NUnit.Framework;
using Slice.Messages;
using Slicer.EventAgregators;
using Slicer.Game;
using Slicer.Levels;
using Slicer.Levels.Modifycations.Messages;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Game
{
    public class LevelTimerPerTest
    {
        private IEventsAgregator eventsAgregator;
        private Timer timer;
        private AsyncHelper asyncHelper;
        private LevelTimerPerObjectModify perObjectModify;
        
        [SetUp]
        public void Setup()
        {
            asyncHelper = new GameObject(nameof(AsyncHelper)).AddComponent<AsyncHelper>();
            eventsAgregator = new EventsAgregator();
            timer = new Timer(eventsAgregator, asyncHelper);
            perObjectModify = new LevelTimerPerObjectModify();
        }

        [Test]
        public void WhenTimerPerApply_AndTimerWindowShowMessage_ThenWindowShow()
        {
            //Arrange
            var isShow = false;
            
            eventsAgregator.AddListener<TimerWindowActiveMessage>(message => isShow = true);
            //Act
            perObjectModify.Apply(eventsAgregator);
            
            //Assert
            Assert.IsTrue(isShow);
        }
        
        [Test]
        public void WhenTimerPerApply_AndTimerSubscribe_ThenTimerStart()
        {
            //Arrange
            var currentTime = 0;
            eventsAgregator.AddListener<TimerUpdateMessage>(message => currentTime = message.Value);
            timer.Initialize();
            
            //Act
            var startTime = 10;
            perObjectModify.SetStartTimer(startTime);
            perObjectModify.Apply(eventsAgregator);
            
            //Assert
            Assert.AreEqual(currentTime, startTime);
        }

        [UnityTest]
        public IEnumerator WhenMeshNext_AndTimerPerObjectApply_ThenTimerRestart()
        {
            //Arrange
            var currentTime = 0;
            eventsAgregator.AddListener<TimerUpdateMessage>(message => currentTime = message.Value);
            timer.Initialize();
            
            //Act
            var startTime = 4;
            
            perObjectModify.SetStartTimer(4);
            perObjectModify.Apply(eventsAgregator);

            yield return new WaitForSeconds(1.5f);
            
            eventsAgregator.Invoke(new NextMeshMessage());
            
            //Assert
            Assert.AreEqual(currentTime, startTime);
        }
        
        [TearDown]
        public void TearDown()
        {
            timer = null;
            eventsAgregator = null;
            perObjectModify = null;
            Object.Destroy(asyncHelper.gameObject);
        }
    }
}