using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Slicer.EventAgregators;
using Slicer.Items;
using Slicer.Slice;
using Slicer.Slice.Messages;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ResultCalculateTest
    {
        [Test]
        [TestCase(50, 50, 50, 50)]
        [TestCase(20, 80, 22, 78)]
        [TestCase(0, 0, 0, 0)]
        public void WhenStartCalculate_AndSuccessfulSliceSubscribe_ThenResultCalculate(int leftCount, int rightCount,
            int rightProgress, int leftProgress)
        {
            //Arrange
            var rightResult = 0;
            var leftResult = 0;

            IEventsAgregator eventsAgregator = new EventsAgregator();
            eventsAgregator.AddListener<ResultPercentageMessage>(message =>
            {
                rightResult = message.Right;
                leftResult = message.Left;
            });

            //Act
            var itemMovening = new GameObject("ItemMovening").AddComponent<SlicebleItemMovening>();

            var resultCalculate = new GameObject("ResultCalculate").AddComponent<ResultCalculate>();
            resultCalculate.Setup(itemMovening, eventsAgregator);
            resultCalculate.StartCalculate(rightCount, leftCount);

            //Assert

            Assert.AreEqual(rightProgress, rightResult);
            Assert.AreEqual(leftProgress, leftResult);
        }

        [Test]
        [TestCase(50, 50, 1)]
        [TestCase(0, 100, 0)]
        [TestCase(25, 75, 0.5f)]
        public void WhenStartCalculate_AndCalculateEndedSubscribe_ThenPercentageDeltaCalculate(int left, int right,
            float percentageDelta)
        {
            //Arrange
            var percentageDeltaResult = 0f;
            var resultCalculate = new GameObject("ResultCalculate").AddComponent<ResultCalculate>();
            resultCalculate.OnProgressCalculateEnded += result => percentageDeltaResult = result; 

            //Act
            resultCalculate.IncreaseGameProgress(left, right);
            
            //Assert
            Assert.AreEqual(
                percentageDelta, percentageDeltaResult);
        }
    }
}