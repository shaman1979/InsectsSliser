using Slicer.Application.Storages;
using Slicer.Logger;
using Slicer.Tools;
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
            if (level.IsNegative().AssertTry($"Значение {nameof(level)} не может быть меньше нуля")
                | stars.IsNegative().AssertTry($"Значение {nameof(stars)} не может быть меньше нуля")
                | xp.IsNegative().AssertTry($"Значение {nameof(xp)} не может быть меньше нуля"))
                return;

            PlayerPrefs.SetInt(PlayerPrefsKeyStorage.LEVEL, level);
            PlayerPrefs.SetInt(PlayerPrefsKeyStorage.STARS, stars);
            PlayerPrefs.SetInt(PlayerPrefsKeyStorage.XP, xp);
        }
    }
}