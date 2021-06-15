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
        private StarView lowStar;

        [SerializeField]
        private StarView middleStar;

        [SerializeField]
        private StarView highStar;

        [SerializeField]
        private string starKey = "Stars";

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

            Load();
        }

        private void OnProgressChanged(float currentProgress)
        {
            return;
            var progress = (float)currentProgress / HPInitializer.GetMaxProgress;

            progress = Mathf.Clamp(progress, 0, 1);

            if (progress > 0.45f)
            {
                StarActivator(lowStar);
                starSession = 1;
            }

            if (progress >= 0.68f)
            {
                StarActivator(middleStar);
                starSession = 2;
            }

            if (progress >= 0.89f)
            {
                StarActivator(highStar);
                starSession = 3;
            }
        }

        private void StarActivator(StarView star)
        {
            star.StarReached();
        }

        private void SetTotalStars()
        {
            totalStar += starSession;
            Save();
        }

        private void ResetAll()
        {
            lowStar.ResetState();
            middleStar.ResetState();
            highStar.ResetState();
            starSession = 0;
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