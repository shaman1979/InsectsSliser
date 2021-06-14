using System.Collections;
using NUnit.Framework;
using Tools;
using UI.Elements;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests.UI
{
    public class UIElementsTest
    {
        private UIMove uiElementMove;
        private UIFade uiElementFade;

        [SetUp]
        public void Setup()
        {
            uiElementMove = new GameObject("uiElement").AddComponent<UIMove>();
            uiElementMove.gameObject.AddComponent<RectTransform>();

            uiElementFade = new GameObject(nameof(UIFade)).AddComponent<UIFade>();
        }

        [UnityTest]
        public IEnumerator WhenUIActive_AndUIDeActive_ThenUIMove()
        {
            //Arrange
            uiElementMove.Setup(Vector3.one, Vector3.zero);
            //Act
            uiElementMove.Activate();

            yield return new WaitForSeconds(0.5f);
            //Assert
            var newPosition = uiElementMove.transform.GetPosition();
            Assert.AreEqual(new Vector3(1, 1, 0), newPosition);
        }

        [UnityTest]
        public IEnumerator WhenUIDeActive_AndUIActive_ThenUIMove()
        {
            //Arrange
            var deActivePosition = Vector3.zero;

            uiElementMove.Setup(Vector3.one, deActivePosition);
            //Act
            uiElementMove.Activate();
            yield return new WaitForSeconds(0.5f);
            uiElementMove.Deactivate();
            yield return new WaitForSeconds(0.5f);
            //Assert
            var newPosition = uiElementMove.transform.GetPosition();

            Assert.AreEqual(deActivePosition, newPosition);
        }

        [UnityTest]
        public IEnumerator WhenFadeStop_AndUIActive_ThenUIFadeStart()
        {
            //arrange


            //act
            uiElementFade.StartFade();

            //assert
            Assert.IsTrue(uiElementFade.gameObject.activeSelf);
            yield return new WaitForSeconds(0.1f);
            Assert.IsFalse(uiElementFade.gameObject.activeSelf);
        }

        [Test]
        public void WhenSetFade_AndFadeZero_ThenFadeEqualsOne()
        {
            //Arrange
            uiElementFade.Awake();
            uiElementFade.Setup(new GameObject("Text").AddComponent<Text>());
            
            //Act
            uiElementFade.SetFade(1);
            
            //Assert
            Assert.AreEqual(1, uiElementFade.GetFade());
        }
        
        [Test]
        public void WhenGetFade_AndFadeOne_ThenFadeEqualsOne()
        {
            //Arrange
            uiElementFade.Awake();
            var text = new GameObject("Text").AddComponent<Text>();
            uiElementFade.Setup(text);
            
            //Act
            var resultFade = uiElementFade.GetFade();
            
            //Assert
            Assert.AreEqual(text.color.a, resultFade);
        }

        [Test]
        public void WhenGetFadeImage_AndImageFadeOne_ThenImageFadeEqualsOne()
        {
            //Arrange
            var image = new GameObject("Image").AddComponent<Image>();

            //Act
            var resultFade = image.GetFade();

            //Assert
            Assert.AreEqual(image.color.a, resultFade);
        }

        [Test]
        public void WhenSetFadeImage_AndImageFadeZero_ThenFadeEqualsOne()
        {
            //Arrange
            var image = new GameObject("Image").AddComponent<Image>();

            //Act
            image.SetFade(1);

            //Assert
            Assert.AreEqual(1, image.GetFade());
        }

        [Test]
        public void WhenGetFadeText_AndTextFadeOne_ThenFadeEqualsOne()
        {
            //Arrange
            var text = new GameObject("Text").AddComponent<Text>();

            //Act
            var resultFade = text.GetFade();

            //Assert
            Assert.AreEqual(text.color.a, resultFade);
        }

        [Test]
        public void WhenSetFadeText_AndTextFadeOne_ThenFadeEqualsOne()
        {
            //Arrange
            var text = new GameObject("Text").AddComponent<Text>();

            //Act
            text.SetFade(1);
            
            //Assert
            Assert.AreEqual(1, text.GetFade());
        }

        [Test]
        public void WhenGetImageColor_AndImageColorWhite_ThenColorEqualsWhite()
        {
            //Arrange
            var image = new GameObject("Image").AddComponent<Image>();

            //Act
            var resultColor = image.GetColor();

            //Assert
            Assert.AreEqual(image.color, resultColor);
        }

        [Test]
        public void WhenSetImageColor_AndImageColorWhite_ThenColorEqualsRed()
        {
            //Arrange
            var image = new GameObject("Image").AddComponent<Image>();

            //Act
            image.SetColor(Color.red);

            //Assert
            Assert.AreEqual(Color.red, image.GetColor());
        }

        [Test]
        public void WhenGetTextColor_AndTextColorWhite_ThenColorEqualsWhite()
        {
            //Arrange
            var text = new GameObject("Text").AddComponent<Text>();

            //Act
            var resultColor = text.GetColor();
            
            //Assert
            Assert.AreEqual(text.color, resultColor);
        }

        [Test]
        public void WhenSetTextColor_AndTextColorWhite_ThenColorEqualsRed()
        {
            //Arrange
            var text = new GameObject("Text").AddComponent<Text>();

            //Act
            text.SetColor(Color.red);

            //Assert
            Assert.AreEqual(Color.red, text.GetColor());
        }
        
        [TearDown]
        public void TearDown()
        {
            Object.Destroy(uiElementMove);
            Object.Destroy(uiElementFade);
        }
    }
}