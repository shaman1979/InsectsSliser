using NUnit.Framework;
using Slicer.Application.Storages;
using Slicer.DebugMode;
using UnityEngine;

namespace Tests.Game
{
    public class ProgressDataTest
    {
        [Test]
        public void WhenRemoveProgress_AndSaveLastData_ThenProgressDataClear()
        {
            //Arrange
            var xp = PlayerPrefs.GetInt(PlayerPrefsKeyStorage.XP);
            var level = PlayerPrefs.GetInt(PlayerPrefsKeyStorage.LEVEL);
            var stars = PlayerPrefs.GetInt(PlayerPrefsKeyStorage.STARS);
            
            //Act
            ProgressDataReceiver.RemoveProgress();

            //Assert
            Assert.AreEqual(0, PlayerPrefs.GetInt(PlayerPrefsKeyStorage.XP));
            Assert.AreEqual(0, PlayerPrefs.GetInt(PlayerPrefsKeyStorage.LEVEL));
            Assert.AreEqual(0, PlayerPrefs.GetInt(PlayerPrefsKeyStorage.STARS));
            
            ProgressDataReceiver.SetProgressData(level, stars, xp);
        }
    }
}
