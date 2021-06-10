using System.Collections;
using Applications;
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
        public void WhenHalfRedZoneGenerate_AndFirstPositionPointNetZero_ThenFirstPointsInCorrect()
        {
            //Arrange
            var offset = 0.1f;
            var zoneFactory = new HalfRedZoneFactory(0.1f);
            
            //Act
            var resultFirstPoint = Vector3.zero;
            var resultSecondPoint = Vector3.zero;
            zoneFactory.Create(out resultFirstPoint, out resultSecondPoint);

            //Assert
            var firstPoint = new Vector3(0f, offset + SliceDataStorage.HalfRedZoneOffset, 2f);
            var secondPoint = SliceDataStorage.SecondPointPosition;
            
            Assert.AreEqual(firstPoint, resultFirstPoint);
            Assert.AreEqual(secondPoint, resultSecondPoint);
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
            var firstPoint = redZoneModify.GetFirstPoint();
            var secondPoint = redZoneModify.GetSecondPoint();
            
            Assert.AreEqual(firstPoint, new Vector3(0f, offsetHalf + SliceDataStorage.HalfRedZoneOffset, 2f));
            Assert.AreNotEqual(firstPoint, secondPoint);
        }

        [TearDown]
        public void TearDown()
        {
            eventsAgregator.Clear();
        }
    }
}