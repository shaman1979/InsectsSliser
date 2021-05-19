using NUnit.Framework;
using Slicer.Application.Storages;
using Slicer.DebugMode;
using UnityEngine;

namespace Tests.Game
{
    public class ProgressDataTest
    {
        [Test]
        public void WhenRemoveProgress_ThenProgressDataClear()
        {
            //Act
            ProgressDataReceiver.RemoveProgress();
            
            //Assert
            Assert.AreEqual(0, PlayerPrefs.GetInt(PlayerPrefsKeyStorage.XP));
            Assert.AreEqual(0, PlayerPrefs.GetInt(PlayerPrefsKeyStorage.LEVEL));
            Assert.AreEqual(0, PlayerPrefs.GetInt(PlayerPrefsKeyStorage.STARS));
        }
    }
}
