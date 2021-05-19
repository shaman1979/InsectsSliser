using Slicer.Application.Storages;
using UnityEngine;

namespace Slicer.DebugMode
{
    public static class ProgressDataReceiver
    {
        public static void RemoveProgress()
        {
            SetProgressData(0, 0, 0);
        }

        public static void SetProgressData(int level, int stars, int xp)
        {
            PlayerPrefs.SetInt(PlayerPrefsKeyStorage.LEVEL, level);
            PlayerPrefs.SetInt(PlayerPrefsKeyStorage.STARS, stars);
            PlayerPrefs.SetInt(PlayerPrefsKeyStorage.XP, xp);
        }
    }
}