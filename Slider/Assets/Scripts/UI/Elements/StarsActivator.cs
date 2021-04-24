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

        private void Awake()
        {
            Events.ProgressChanged += OnProgressChanged;
            Events.GameStart += ResetAll;
        }

        private void OnProgressChanged()
        {
            var progress = (float)HPManager.GetCurrentProgress() / HPManager.GetMaxProgress();

            progress = Mathf.Clamp(progress, 0, 1);

            if(progress >= 0.45f)
            {
                StarActivator(lowStar);
            }

            if (progress >= 0.68f)
            {
                StarActivator(middleStar);
            }

            if (progress >= 0.89f)
            {
                StarActivator(highStar);
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
        }
    }
}