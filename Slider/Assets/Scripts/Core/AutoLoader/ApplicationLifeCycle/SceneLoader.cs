using LightDev;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Slicer.Game
{
    public class SceneLoader : IInitializable
    {
        private bool isNeedToChangeTimeScale = false;

        public void Initialize()
        {
            OnSceneLoaded();
        }

        private void OnSceneLoaded()
        {
            Events.SceneLoaded.Call();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus)
            {
                if (isNeedToChangeTimeScale)
                    Time.timeScale = 1;
                Events.ApplicationResumed.Call();
            }
            else
            {
                if (isNeedToChangeTimeScale)
                    Time.timeScale = 0;
                Events.ApplicationPaused.Call();
            }
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                if (isNeedToChangeTimeScale)
                    Time.timeScale = 0;
                Events.ApplicationPaused.Call();
            }
            else
            {
                if (isNeedToChangeTimeScale)
                    Time.timeScale = 1;
                Events.ApplicationResumed.Call();
            }
        }
    }
}
