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
            gameObject.SetActive(true);

            //Act
            gameObject.Deactivate();

            //Assert
            Assert.IsFalse(gameObject.activeSelf);
        }

        [Test]
        public void WhenGameObjectActive_AndGameObjectDeActiveInStart_ThenGameObjectActiveSelfIsTrue()
        {
            //Arrange
            gameObject.SetActive(false);
            //Act
            gameObject.Activate();
            
            //Assert
            Assert.IsTrue(gameObject.activeSelf);
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(gameObject);
        }
    }
}
