using System.Collections;
using Level.Modify.Modifycations;
using NUnit.Framework;
using Slice.RedZoneSlicer;
using Slice.RedZoneSlicer.Factoryes;
using Slicer.EventAgregators;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class RedZoneTest
    {
        private IEventsAgregator eventsAgregator;

        [SetUp]
        public void Setup()
        {
            eventsAgregator = new EventsAgregator();
        }

        [Test]
        public void WhenRedZoneApply_AndSubscribeSing_ThenMessageShouldReach()
        {
            //Arrange
            var isGenerate = false;
            
            eventsAgregator.AddListener<RedZoneGeneratorMessage>(message => isGenerate = true);
            //Act
            var redZoneModify = new RedZoneModify();
            redZoneModify.SetFactory(new HalfRedZoneFactory(0.1f));
            redZoneModify.Apply(eventsAgregator);

            //Assert
            Assert.IsTrue(isGenerate);
        }

        [Test]
        public void WhenHalfRedZoneGenerate_AndPositionPointNotZero_ThenPointNotAreEquals()
        {
            //arrange
            var offsetHalf = 0.1f;
            var zoneFactory = new HalfRedZoneFactory(offsetHalf);
            //act

            var redZoneModify = new RedZoneModify();
            redZoneModify.SetFactory(zoneFactory);
            redZoneModify.Apply(eventsAgregator);
            
            //assert
            Vector3 firstPoint = redZoneModify.GetFirstPoint();
            Vector3 secondPoint = redZoneModify.GetSecondPoint();
            
            Assert.AreEqual(firstPoint, new Vector3(0f, offsetHalf, 2f));
            Assert.AreNotEqual(firstPoint, secondPoint);
        }

        [TearDown]
        public void TearDown()
        {
            eventsAgregator.Clear();
        }
    }
}