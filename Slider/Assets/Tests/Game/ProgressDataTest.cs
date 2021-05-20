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

        [Test]
        public void WhenSetProgressData_AndSaveDataPositive_ThenSaveData()
        {
            //Arrange
            var xpLast = PlayerPrefs.GetInt(PlayerPrefsKeyStorage.XP);
            var levelLast = PlayerPrefs.GetInt(PlayerPrefsKeyStorage.LEVEL);
            var starsLast = PlayerPrefs.GetInt(PlayerPrefsKeyStorage.STARS);
            
            var xp = Random.Range(0, int.MaxValue);
            var level = Random.Range(0, int.MaxValue);
            var stars = Random.Range(0, int.MaxValue);
            //Act
            ProgressDataReceiver.SetProgressData(level, stars, xp);

            //Asset
            Assert.AreEqual(xp, PlayerPrefs.GetInt(PlayerPrefsKeyStorage.XP));
            Assert.AreEqual(level, PlayerPrefs.GetInt(PlayerPrefsKeyStorage.LEVEL));
            Assert.AreEqual(stars, PlayerPrefs.GetInt(PlayerPrefsKeyStorage.STARS));

            ProgressDataReceiver.SetProgressData(levelLast, starsLast, xpLast);
        }

        [Test]
        public void WhenSetProgressData_AndSaveDataNegative_ThenDontSaveData()
        {
            //Arrange
            var xpLast = PlayerPrefs.GetInt(PlayerPrefsKeyStorage.XP);
            var levelLast = PlayerPrefs.GetInt(PlayerPrefsKeyStorage.LEVEL);
            var starsLast = PlayerPrefs.GetInt(PlayerPrefsKeyStorage.STARS);
            
            var xp = Random.Range(int.MinValue, -1);
            var level = Random.Range(int.MinValue, -1);
            var stars = Random.Range(int.MinValue, -1);

            //Act
            ProgressDataReceiver.SetProgressData(level, stars, xp);

            //Asset
            Assert.AreEqual(xpLast, PlayerPrefs.GetInt(PlayerPrefsKeyStorage.XP));
            Assert.AreEqual(levelLast, PlayerPrefs.GetInt(PlayerPrefsKeyStorage.LEVEL));
            Assert.AreEqual(starsLast, PlayerPrefs.GetInt(PlayerPrefsKeyStorage.STARS));

            ProgressDataReceiver.SetProgressData(levelLast, starsLast, xpLast);
        }
    }
}