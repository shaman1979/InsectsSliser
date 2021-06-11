using System.Collections;
using NUnit.Framework;
using Tools;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Game.Base
{
    public class GameObjectExtensionsTest
    {
        private GameObject gameObject;

        [SetUp]
        public void Setup()
        {
            gameObject = new GameObject("GameObject");
        }
        
        [Test]
        public void WhenGameObjectDeActive_AndGameObjectActiveInStart_ThenGameObjectActiveSelfIsFalse()
        {
            //Arrange
            gameObject = new GameObject("GameObject");
            gameObject.SetActive(true);

            //Act
            gameObject.Deactivate();

            //Assert
            Assert.IsFalse(gameObject.activeSelf);
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(gameObject);
        }
    }
}
