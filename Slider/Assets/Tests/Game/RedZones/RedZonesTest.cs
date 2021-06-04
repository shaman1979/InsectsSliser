using System.Collections;
using System.Collections.Generic;
using Level.Modify.Modifycations;
using NUnit.Framework;
using Slice.RedZoneSlicer;
using Slicer.EventAgregators;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class RedZonesTest
    {
        private IEventsAgregator eventsAgregator;

        [SetUp]
        public void Setup()
        {
            eventsAgregator = new EventsAgregator();
        }

        [Test]
        public void WhenRedZoneActivateMessagePublish_AndRedZoneViewSubcribe_ThenRedZoneDraw()
        {
            //Arrange
            var redZoneView = new GameObject(nameof(RedZoneView)).AddComponent<RedZoneView>();

            redZoneView.Setup(eventsAgregator);
            redZoneView.Initialize();
            redZoneView.MaterialInitialize();
            //Act
            
            var firstRedZone = Vector3.zero;
            var secondRedZone = Vector3.zero + new Vector3(0.5f, 0f, 0f);
            eventsAgregator.Invoke(new RedZoneGeneratorMessage(firstRedZone, secondRedZone));

            //Assert
            var resultFirstVector = redZoneView.GetFirstVector();
            var resultSecondVector = redZoneView.GetSecondVector();


            Assert.AreEqual(resultFirstVector, firstRedZone);
            Assert.AreEqual(resultSecondVector, secondRedZone);
        }

        [Test]
        public void WhenRedZoneApply_AndSubscribeSing_ThenMessageShouldReach()
        {
            //Arrange
            bool isGenerate = false;
            
            eventsAgregator.AddListener<RedZoneGeneratorMessage>(message => isGenerate = true);
            //Act
            var redZoneModify = new RedZoneModify();
            redZoneModify.Apply(eventsAgregator);

            //Assert
            Assert.IsTrue(isGenerate);
        }

        [TearDown]
        public void TearDown()
        {
            eventsAgregator.Clear();
        }
    }
}
