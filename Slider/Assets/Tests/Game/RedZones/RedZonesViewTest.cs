using System.Collections;
using System.Collections.Generic;
using Applications;
using Level.Modify.Modifycations;
using NUnit.Framework;
using Slice.RedZoneSlicer;
using Slicer.EventAgregators;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class RedZonesViewTest
    {
        private IEventsAgregator eventsAgregator;
        private RedZoneView redZoneView;

        [SetUp]
        public void Setup()
        {
            eventsAgregator = new EventsAgregator();
            
            redZoneView = new GameObject(nameof(RedZoneView)).AddComponent<RedZoneView>();

            redZoneView.Setup(eventsAgregator);
            redZoneView.MaterialInitialize();
        }

        [Test]
        public void WhenRedZoneActivateMessagePublish_AndRedZoneViewSubcribe_ThenRedZoneDraw()
        {
            //Arrange
            redZoneView.Initialize();

            //Act
            
            var firstRedZone = Vector3.zero;
            var secondRedZone = Vector3.zero + new Vector3(0.5f, 0f, 0f);
            eventsAgregator.Invoke(new RedZoneGeneratorMessage(firstRedZone, secondRedZone));

            //Assert
            var resultFirstVector = redZoneView.GetFirstVector();
            var resultSecondVector = redZoneView.GetSecondVector();

            var secondRedVectorWorldPosition = new Vector3(0.424024045f, 0f, 0.264959663f);

            Assert.AreEqual(resultFirstVector, firstRedZone);
            Assert.AreEqual(resultSecondVector, secondRedVectorWorldPosition);
        }

        [Test]
        public void WhenRedZoneGenerate_AndVectorNotZero_ThenVectorsRotationInCorrect()
        {
            //Arrange
            redZoneView.Initialize();
            
            //Act
            var firstRedZone = Vector3.zero;
            var secondRedZone = Vector3.zero + new Vector3(0.5f, 0f, 0f);
            
            var resultFirstRotation = Quaternion.identity;
            var resultSecondRotation = Quaternion.identity;

            eventsAgregator.Invoke(new RedZoneGeneratorMessage(firstRedZone, secondRedZone));
            
            resultFirstRotation = redZoneView.GetFirstPoint().localRotation;
            resultSecondRotation = redZoneView.GetSecondPoint().localRotation;
            
            //Assert
            var firstVectorRotation = SliceRotationStorage.RedZoneRotation;

            var secondVectorRotation = Quaternion.identity;

            Assert.AreEqual(firstVectorRotation.eulerAngles, resultFirstRotation.eulerAngles);
            Assert.AreEqual(secondVectorRotation.eulerAngles, resultSecondRotation.eulerAngles);
        }
        
        [Test]
        public void WhenRedZoneGenerate_AndVectorNotZero_ThenVectorsPositionInCorrect()
        {
            //Arrange
            
            redZoneView.Initialize();
            
            //Act
            Vector3 firstPointPosition = Vector3.zero;
            Vector3 secondPointPosition = Vector3.zero + new Vector3(0.5f, 0f, 0f);
            eventsAgregator.Invoke(new RedZoneGeneratorMessage(firstPointPosition, secondPointPosition));

            //Assert

            Vector3 resultFirstPointPosition = redZoneView.GetFirstPoint().localPosition;
            Vector3 resultSecondPointPosition = redZoneView.GetSecondPoint().localPosition;
            
            Assert.AreEqual(firstPointPosition, resultFirstPointPosition);
            Assert.AreEqual(secondPointPosition, resultSecondPointPosition);
        }

        [TearDown]
        public void TearDown()
        {
            eventsAgregator.Clear();
            Object.Destroy(redZoneView.gameObject);
        }
    }
}
