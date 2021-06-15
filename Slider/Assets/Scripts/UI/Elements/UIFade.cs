using System;
using DG.Tweening;
using LightDev.Core;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class UIFade : MonoBehaviour
    {
        [SerializeField] private Graphic graphic;
        [SerializeField] private float duration = 0.5f;

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
            SetFade(1);
            sequenceHelper.Sequence(
                sequenceHelper.Fade(0, duration).SetEase(Ease.InSine),
                sequenceHelper.Fade(1, duration).SetEase(Ease.InSine)
            ).SetLoops(-1);
        }

        public float GetFade()
        {
            return graphic.GetFade();
        }

        public void OnDestroy()
        {
            sequenceHelper.KillSequences();
        }
    }
}