using LightDev;
using MeshSlice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Events.ProgressChanged += OnProgressChanged;
            Events.GameStart += ResetAll;
            Events.GameFinish += () => totalStar += starSession;

            Load();
        }

        private void OnProgressChanged()
        {
            var progress = (float)HPManager.GetCurrentProgress() / HPManager.GetMaxProgress();

            progress = Mathf.Clamp(progress, 0, 1);

            if(progress > 0.45f)
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

        private void ResetAll()
        {
            lowStar.ResetState();
            middleStar.ResetState();
            highStar.ResetState();
            starSession = 0;
        }

        private void OnDestroy()
        {
            Save();
        }

        private void Load()
        {
            if(PlayerPrefs.HasKey(starKey))
                totalStar = PlayerPrefs.GetInt(starKey);
        }

        private void Save()
        {
            PlayerPrefs.SetInt(starKey, totalStar);
        }
    }
}