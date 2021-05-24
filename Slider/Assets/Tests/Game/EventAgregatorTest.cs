using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Slicer.EventAgregators;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Game
{
    public class EventAgregatorTest
    {
        [Test]
        public void WhenObjectSubscribe_AndSubscribersIsEmpty_ThenSubscribersIsNotEmpty()
        {
            //arrange
            IEventsAgregator eventsAgregator = new EventsAgregator();
            
            //act

            eventsAgregator.AddListener<int>(message => Debug.Log(message));
            
            //assert
            Assert.IsNotEmpty(eventsAgregator.Subscribers);
        }

        [Test]
        public void WhenMessagePublish_AndSubscribeMessage_ThenSubscriberReceivedMessage()
        {
            //arrange
            IEventsAgregator eventsAgregator = new EventsAgregator();
            
            var isMessageReceived = false;
            eventsAgregator.AddListener<bool>(message => isMessageReceived = message);
            
            //act
            eventsAgregator.Invoke(true);

            //assert
            Assert.IsTrue(isMessageReceived);
        }
    }
}
