using System;
using LightDev.Core;
using UnityEngine;

namespace UI.Elements
{
    public class UIFade : MonoBehaviour
    {
        private SequenceHelper sequenceHelper = null;

        private void Awake()
        {
            sequenceHelper = new SequenceHelper(transform);
        }

        public void StartFade()
        {
            // tapToStart.SetFade(1);
            // tapToStart.Sequence(
            //     tapToStart.Fade(0, 1).SetEase(Ease.InSine),
            //     tapToStart.Fade(1, 0.5f).SetEase(Ease.InSine)
            // ).SetLoops(-1);
        }
    }
}