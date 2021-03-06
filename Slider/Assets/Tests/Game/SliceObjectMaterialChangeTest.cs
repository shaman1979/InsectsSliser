using System.Collections;
using Applications;
using Applications.Messages;
using NUnit.Framework;
using Slicer.EventAgregators;
using UnityEngine;
using UnityEngine.TestTools;
using View;
using View.Messages;

namespace Tests.Game
{
    public class SliceObjectMaterialChangeTest
    {
        private IEventsAgregator eventsAgregator;
        private MaterialChanger changer;

        [SetUp]
        public void Setup()
        {
            eventsAgregator = new EventsAgregator();
            changer = new GameObject(nameof(MaterialChanger)).AddComponent<MaterialChanger>();
            changer.GetComponent<MeshRenderer>().material = new Material(Shader.Find(ShaderStorage.Slice));
        }
        
        [Test]
        public void WhenMaterialChangeMessagePublish_AndMaterialChangerSubscribe_ThenMaterialChange()
        {
            //Arrange
            changer.Setup(eventsAgregator);
            changer.Initialize();
            
            //Act
            var changeMaterial = new Material(Shader.Find(ShaderStorage.RedZone));
            eventsAgregator.Invoke(new MaterialChangeMessage(changeMaterial));

            //Assert
            var material = changer.GetMaterial();
            Assert.AreEqual(changeMaterial.shader, material.shader);
        }

        [Test]
        public void WhenGameFinished_AndMaterialChangerSubscribe_ThenMaterialDefault()
        {
            //Arrange
            changer.Setup(eventsAgregator);
            changer.Initialize();

            //Act
            eventsAgregator.Invoke(new GameFinishMessage());
            var material = changer.GetMaterial();

            //Assert
            var defaultMaterial = new Material(Shader.Find(ShaderStorage.Slice));
            Assert.AreEqual(defaultMaterial.shader, material.shader);
        }

        [TearDown]
        public void TearDown()
        {
            eventsAgregator.Clear();
            Object.DestroyImmediate(changer.gameObject);
        }
    }
}