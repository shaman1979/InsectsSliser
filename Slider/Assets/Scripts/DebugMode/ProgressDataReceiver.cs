using Slicer.Application.Storages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.DebugMode
{
    public static class ProgressDataReceiver
    {
        public static void RemoveProgress()
        {
            PlayerPrefs.SetInt(PlayerPrefsKeyStorage.LEVEL, 0);
            PlayerPrefs.SetInt(PlayerPrefsKeyStorage.STARS, 0);
            PlayerPrefs.SetInt(PlayerPrefsKeyStorage.XP, 0);
        }
    }
}