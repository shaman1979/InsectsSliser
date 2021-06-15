using LightDev;
using MeshSlice;
using Slicer.Application.Storages;
using Slicer.Game;
using System.Collections;
using System.Collections.Generic;
using Slicer.EventAgregators;
using Slicer.HP;
using Slicer.HP.Messages;
using UnityEngine;
using Zenject;

namespace Slicer.UI
{
    public class StarsActivator : MonoBehaviour
    {
        [SerializeField]
        private StarView starView;

        [Inject]
        private HpInitializer HPInitializer;

        [Inject] private IEventsAgregator eventsAgregator;

        private static int totalStar = 0;
        private static int starSession = 0;

        public static int GetTotalStar()
        {
            return totalStar;
        }

        public static bool HasLevelUp()
        {
            return starSession > 0;
        }

        private void Awake()
        {
            eventsAgregator.AddListener<CurrentProgressMessage>(message => OnProgressChanged(message.Progress));
            Events.GameStart += ResetAll;
            Events.GameFinish += SetTotalStars;
            
            ResetAll();
            Load();
        }

        private void OnProgressChanged(float currentProgress)
        {
            var progress = (float)currentProgress / HPInitializer.GetMaxProgress;

            progress = Mathf.Clamp(progress, 0, 1);

            if (progress > 0.45f)
            {
                starSession = 1;
            }

            if (progress >= 0.68f)
            {
                starSession = 2;
            }

            if (progress >= 0.89f)
            {
                starSession = 3;
            }
            
            starView.StarReached(starSession);
        }

        private void SetTotalStars()
        {
            totalStar += starSession;
            Save();
        }

        private void ResetAll()
        {
            starSession = 0;
            starView.StarReached(starSession);
        }

        private void Load()
        {
            totalStar = PlayerPrefs.GetInt(PlayerPrefsKeyStorage.STARS, 0);
        }

        private void Save()
        {
            PlayerPrefs.SetInt(PlayerPrefsKeyStorage.STARS, totalStar);
        }
    }
}