using System.Collections;
using NUnit.Framework;
using Tools;
using UI.Elements;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Game.Base
{
    public class UIMoveTest
    {
        private UIMove uiElement = null;

        [SetUp]
        public void Setup()
        {
            uiElement = new GameObject("uiElement").AddComponent<UIMove>();
            uiElement.gameObject.AddComponent<RectTransform>();
        }
        
        [UnityTest]
        public IEnumerator WhenUIActive_AndUIDeActive_ThenUIMove()
        {
            //Arrange
            uiElement.gameObject.AddComponent<RectTransform>();

            uiElement.Setup(Vector3.one, Vector3.zero);
            //Act
            uiElement.Activate();

            yield return new WaitForSeconds(0.5f);
            //Assert
            var newPosition = uiElement.transform.GetPosition();
            Assert.AreEqual(new Vector3(1,1,0), newPosition);
        }

        [UnityTest]
        public IEnumerator WhenUIDeActive_AndUIActive_ThenUIMove()
        {
            //Arrange
            var deActivePosition = Vector3.zero;

            uiElement.gameObject.AddComponent<RectTransform>();
            uiElement.Setup(Vector3.one, deActivePosition);
            //Act
            uiElement.Activate();
            yield return new WaitForSeconds(0.5f);
            uiElement.Deactivate();
            yield return new WaitForSeconds(0.5f);
            //Assert
            var newPosition = uiElement.transform.GetPosition();
            
            Assert.AreEqual(deActivePosition, newPosition);
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(uiElement);
        }
    }
}
