using System.Collections;
using MeshSlice;
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

        [Test]
        public void WhenGetPositionCurrentAxis_AndTransformHasPosition_ThenPositionEqualsOne()
        {
            //Arrange
            transform.position = new Vector3(1f, 2f, 3f);
            
            //Act
            var x = transform.GetPositionX();
            var y = transform.GetPositionY();
            var z = transform.GetPositionZ();

            //Assert
            Assert.AreEqual(1, x);
            Assert.AreEqual(2, y);
            Assert.AreEqual(3, z);
        }

        [Test]
        public void WhenVector3Multiplication_AndVectorNotEqualsZero_ThenVectorMultiplied()
        {
            //Arrange
            var multiplier = new Vector3(1f, 1f, 1f);
            var multipliable = new Vector3(2f, 2f, 2f);
            
            //Act
            var result = multipliable.Vector3Multiplication(multiplier);

            //Assert
            Assert.AreEqual(new Vector3(2f,2f,2f), result);
        }
        
        [Test]
        public void WhenGetLocalPosition_AndTransformHasParent_ThenLocalPositionEqualOne()
        {
            //Arrange
            var parent = new GameObject("Parent").transform;
            transform.SetParent(parent);
            transform.localPosition = Vector3.one;
            
            //Act
            var relustLocalPosition = transform.GetLocalPosition();

            //Assert
            Assert.AreEqual(transform.localPosition, relustLocalPosition);
            
            transform.UnParent();
            Object.Destroy(parent.gameObject);
        }

        [Test]
        public void WhenGetLocalPositionAxis_AndTransformHasParent_ThenLocalPositionEqualsOne()
        {
            //Arrange
            var parent = new GameObject("Parent").transform;
            transform.SetParent(parent);
            transform.localPosition = new Vector3(1, 2, 3);
            
            //Act
            var localPosition = transform.localPosition;
            
            //Assert
            Assert.AreEqual(localPosition.x, transform.GetLocalPositionX());
            Assert.AreEqual(localPosition.y, transform.GetLocalPositionY());
            Assert.AreEqual(localPosition.z, transform.GetLocalPositionZ());
            
            transform.UnParent();
            Object.Destroy(parent.gameObject);
        }

        [Test]
        public void WhenGetRotation_AndTransformHasRotation_ThenRotationEqualsIdentity()
        {
            //Arrange
            var rotation = transform.rotation;
            
            //Act
            var result = transform.GetRotation();
            
            //Assert
            Assert.AreEqual(rotation, result);
        }
        
        [Test]
        public void WhenGetLocalRotation_AndTransformHasLocalRotation_ThenLocalRotationEqualsIdentity()
        {
            //Arrange
            var rotation = transform.localRotation;
            
            //Act
            var result = transform.GetLocalRotation();
            
            //Assert
            Assert.AreEqual(rotation, result);
        }
        
        [Test]
        public void WhenGetEulerRotation_AndTransformHasEulerRotation_ThenEulerRotationEqualsZero()
        {
            //Arrange
            var rotation = transform.eulerAngles;
            
            //Act
            var result = transform.GetEulerRotation();
            
            //Assert
            Assert.AreEqual(rotation, result);
        }
        
        [Test]
        public void WhenGetLocalEulerRotation_AndTransformHasLocalEulerRotation_ThenLocalEulerRotationEqualsZero()
        {
            //Arrange
            var rotation = transform.localEulerAngles;
            
            //Act
            var result = transform.GetLocalEulerRotation();
            
            //Assert
            Assert.AreEqual(rotation, result);
        }

        [Test]
        public void WhenGetEulerRotationAxis_AndVectorNotEqualsZero_ThenAxisEqualsOne()
        {
            //Arrange

            //Act
            var x = transform.GetEulerRotationX();
            var y = transform.GetEulerRotationY();
            var z = transform.GetEulerRotationZ();

            //Assert
            
            Assert.AreEqual(transform.eulerAngles.x, x);
            Assert.AreEqual(transform.eulerAngles.y, y);
            Assert.AreEqual(transform.eulerAngles.z, z);
        }
        
        [Test]
        public void WhenGetEulerLocalRotationAxis_AndVectorNotEqualsZero_ThenAxisEqualsOne()
        {
            //Arrange

            //Act
            var x = transform.GetEulerLocalRotationX();
            var y = transform.GetEulerLocalRotationY();
            var z = transform.GetEulerLocalRotationZ();

            //Assert
            
            Assert.AreEqual(transform.localEulerAngles.x, x);
            Assert.AreEqual(transform.localEulerAngles.y, y);
            Assert.AreEqual(transform.localEulerAngles.z, z);
        }

        [Test]
        public void WhenSetPositionAxis_AndTransformPositionEqualsZero_ThenPositionEqualsNewPosition()
        {
            //Arrange
            transform.position = Vector3.zero;
            
            //Act
            var newPosition = new Vector3(1f,2f,3f);
            
            transform.SetPositionX(newPosition.x);
            transform.SetPositionY(newPosition.y);
            transform.SetPositionZ(newPosition.z);

            //Assert
            Assert.AreEqual(newPosition.x, transform.GetPositionX());
            Assert.AreEqual(newPosition.y, transform.GetPositionY());
            Assert.AreEqual(newPosition.z, transform.GetPositionZ());
        }
        
        [TearDown]
        public void TearDown()
        {
            Object.Destroy(transform.gameObject);
        }
    }
}