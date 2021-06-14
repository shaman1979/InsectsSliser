using System;
using LightDev.Core;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class UIFade : MonoBehaviour
    {
        [SerializeField]
        private Graphic graphic;

        private SequenceHelper sequenceHelper = null;

        public void Awake()
        {
            sequenceHelper = new SequenceHelper(transform);
        }

        public void Setup(Graphic graphic)
        {
            this.graphic = graphic;
        }
        
        public void SetFade(float fade)
        {
            graphic.SetFade(fade);
        }

        public void StartFade()
        {
            // tapToStart.SetFade(1);
            // tapToStart.Sequence(
            //     tapToStart.Fade(0, 1).SetEase(Ease.InSine),
            //     tapToStart.Fade(1, 0.5f).SetEase(Ease.InSine)
            // ).SetLoops(-1);
        }

        public float GetFade()
        {
            return graphic.GetFade();
        }
    }
}