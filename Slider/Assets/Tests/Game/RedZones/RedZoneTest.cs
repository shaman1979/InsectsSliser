using System.Collections;
using Level.Modify.Modifycations;
using NUnit.Framework;
using Slice.RedZoneSlicer;
using Slicer.EventAgregators;
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