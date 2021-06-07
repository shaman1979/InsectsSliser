using System.Collections;
using System.Collections.Generic;
using Applications;
using Applications.Messages;
using BzKovSoft.ObjectSlicerSamples;
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
            eventsAgregator.Invoke(new RedZoneGeneratorMessage(firstRedZone, secondRedZone, SliceDataStorage.HalfRedZoneWidth));

            //Assert
            var resultFirstVector = redZoneView.GetFirstVector();
            var resultSecondVector = redZoneView.GetSecondVector();
            var resultWidth = redZoneView.GetLineWidth();

            var secondRedVectorWorldPosition = new Vector3(0.424024045f, 0f, 0.264959663f);

            Assert.AreEqual(SliceDataStorage.HalfRedZoneWidth, resultWidth);
            Assert.AreEqual(resultFirstVector, firstRedZone);
            Assert.AreEqual(resultSecondVector, secondRedVectorWorldPosition);
        }

        [Test]
        public void WhenRedZoneGenerateFinished_AndRedZoneNotZero_ThenRedZonePositionEqualsZero()
        {
            //Arrange
            redZoneView.Initialize();
            //Act
            eventsAgregator.Invoke(new RedZoneGeneratorMessage(Vector3.back, Vector3.up, 0.5f));
            
            Assert.AreNotEqual(Vector3.zero, redZoneView.transform.position);
            
            redZoneView.TransformReset();
            
            //Assert
            Assert.AreEqual(Vector3.zero, redZoneView.transform.position);
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

            eventsAgregator.Invoke(new RedZoneGeneratorMessage(firstRedZone, secondRedZone, SliceDataStorage.HalfRedZoneWidth));
            
            resultFirstRotation = redZoneView.GetFirstPoint().localRotation;
            resultSecondRotation = redZoneView.GetSecondPoint().localRotation;
            
            //Assert
            var firstVectorRotation = SliceDataStorage.RedZoneRotation;

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
            var firstPointPosition = Vector3.zero;
            var secondPointPosition = Vector3.zero + new Vector3(0.5f, 0f, 0f);
            eventsAgregator.Invoke(new RedZoneGeneratorMessage(firstPointPosition, secondPointPosition, SliceDataStorage.HalfRedZoneWidth));

            //Assert

            var resultFirstPointPosition = redZoneView.GetFirstPoint().localPosition;
            var resultSecondPointPosition = redZoneView.GetSecondPoint().localPosition;
            
            Assert.AreEqual(firstPointPosition, resultFirstPointPosition);
            Assert.AreEqual(secondPointPosition, resultSecondPointPosition);
        }

        [Test]
        public void WhenRedZoneGenerate_AndRedZoneColliderCreate_ThenBoxColliderSizeCorrect()
        {
            //Arrange
            var redZoneCollider = new GameObject("RedZoneCollider").AddComponent<RedZoneCollider>();
            redZoneCollider.Setup(eventsAgregator);
            redZoneCollider.Initialize();
            
            //Act
            float lineWidth = 2;
            eventsAgregator.Invoke(new RedZoneGeneratorMessage(Vector3.zero, Vector3.zero, lineWidth));

            //Assert

            var resultSize = redZoneCollider.GetColliderSize;
            var resultCenter = redZoneCollider.GetColliderCenter;
            
            var boxColliderSize = new Vector3(3f, lineWidth, 1f);
            var boxColliderCenter = new Vector3(0f,0f,-2.20f);


            Assert.AreEqual(boxColliderSize, resultSize);
            Assert.AreEqual(boxColliderCenter, resultCenter);
            Assert.IsTrue(redZoneCollider.IsTrigger);
        }

        [Test]
        public void WhenRedZoneCollide_AndSubscribeSing_ThenGameFinishMessagePublisher()
        {
            //Arrange
            var isFinished = false;
            eventsAgregator.AddListener<GameFinishMessage>(message => isFinished = true);

            //Act
            var collider = new GameObject("GameObject").AddComponent<BoxCollider>();
            collider.gameObject.AddComponent<Knife>();
            
            var redZoneCollider = new GameObject(nameof(RedZoneCollider)).AddComponent<RedZoneCollider>();
            redZoneCollider.Setup(eventsAgregator);
            redZoneCollider.OnTriggerEnter(collider);
            
            //Assert
            Assert.IsTrue(isFinished);
        }

        [TearDown]
        public void TearDown()
        {
            eventsAgregator.Clear();
            Object.Destroy(redZoneView.gameObject);
        }
    }
}
