using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Slicer.UI.Elements;
using UI.UI.Button;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class UIButtonTest
    {
        private ShopButton shopButton;
        private ButtonElement button;

        private Sprite avaliableItem;
        private Sprite unavailableItem;
        private Sprite selectedItem;

        [SetUp]
        public void Setup()
        {
            button = new GameObject("Button").AddComponent<ButtonElement>();


            avaliableItem = Sprite.Create(null, Rect.zero, Vector2.zero);
            unavailableItem = Sprite.Create(null, Rect.zero, Vector2.zero);
            selectedItem = Sprite.Create(null, Rect.zero, Vector2.zero);

            shopButton = new GameObject("ShopButton").AddComponent<ShopButton>();
            shopButton.Setup(avaliableItem, unavailableItem, selectedItem);
        }

        [Test]
        public void WhenGetButtonImage_AndImageNull_ThenImageEqualsButtonImage()
        {
            //Arrange
            
            //Act
            var currentImage = button.GetImage();

            //Assert
            Assert.AreEqual(button.Image.sprite ,currentImage);
        }

        [Test]
        public void WhenSetButtonImage_AndImageIsNotNull_ThenImageEqualsButtonImage()
        {
            //Arrange
            var image = Sprite.Create(null, Rect.zero, Vector2.zero);
            //Act
            button.SetImage(image);

            //Assert
            Assert.AreEqual(image, button.GetImage());
        }
        
        [Test]
        public void WhenButtonAvaliable_AndAvaliableSpriteIsNotNull_ThenButtonChangeImage()
        {
            //Arrange

            //Act
            shopButton.Avaliable();
            
            //Assert
            Assert.AreEqual(avaliableItem, shopButton.GetImage());
        }

        [Test]
        public void WhenButtonUnavaliable_AndUnavaliableSpriteIsNotNull_ThenButtonImageChange()
        {
            //Arrange

            //Act
            shopButton.Unavaliable();

            //Assert
            Assert.AreEqual(unavailableItem, shopButton.GetImage());
        }

        [Test]
        public void WhenSelectedButton_AndSelectedSpriteIsNotNull_ThenButtonImageChange()
        {
            //Arrange

            //Act
            shopButton.Select();
            
            //Assert
            Assert.AreEqual(selectedItem, shopButton.GetImage());
        }
        
        [TearDown]
        public void TearDown()
        {
            Object.Destroy(button.gameObject);
            Object.Destroy(shopButton);
        }
    }
}
