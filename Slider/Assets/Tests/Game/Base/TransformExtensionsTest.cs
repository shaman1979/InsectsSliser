using System.Collections;
using NUnit.Framework;
using Tools;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Game.Base
{
    public class TransformExtensionsTest
    {
        public Transform transform;

        [SetUp]
        public void Setup()
        {
            transform = new GameObject("Transform").transform;
        }

        [Test]
        public void WhenUnParent_AndParentHas_ThenParentIsNull()
        {
            //Arrange
            var parent = new GameObject("Parent").transform;
            transform.SetParent(parent);

            //Act
            transform.UnParent();
            
            //Assert
            Assert.IsNull(transform.parent);
        }

        [Test]
        public void WhenGetPosition_AndTransformHasPosition_ThenPositionEqualsZero()
        {
            //Arrange
            var position = transform.position;

            //Act
            var currentPosition = transform.GetPosition();
            
            //Assert
            Assert.AreEqual(position, currentPosition);
        }
        
        [TearDown]
        public void TearDown()
        {
            Object.Destroy(transform.gameObject);
        }
    }
}