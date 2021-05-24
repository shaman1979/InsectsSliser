using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Slicer.EventAgregators;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class EventAgregatorTest
    {
        [Test]
        public void WhenObjectSubscribe_AndSubscribersIsEmpty_ThenSubscribersShoudBeOne()
        {
            //arrange
            IEventsAgregator eventsAgregator = new EventsAgregator();
            
            //act

            eventsAgregator.AddListener<int>(message => Debug.Log(message));
            
            //assert
            Assert.IsNotEmpty(eventsAgregator.Subscribers);
        }
    }
}
