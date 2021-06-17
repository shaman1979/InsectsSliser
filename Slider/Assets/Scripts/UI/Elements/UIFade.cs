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
        [SerializeField] private float showDuration = 0.5f;
        [SerializeField] private float hideDuration = 0.5f;

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
                sequenceHelper.Fade(0, showDuration).SetEase(Ease.InSine),
                sequenceHelper.Fade(1, hideDuration).SetEase(Ease.InSine)
            ).SetLoops(-1);
        }

        public void StopFade()
        {
            sequenceHelper.KillSequences();
            sequenceHelper.Sequence(
                sequenceHelper.Fade(0, hideDuration).SetEase(Ease.InSine)
            );
        }

        public void DelayFade()
        {
            sequenceHelper.Sequence(
                sequenceHelper.Fade(1, showDuration).SetEase(Ease.InSine),
                sequenceHelper.Delay(1),
                sequenceHelper.Fade(0, hideDuration).SetEase(Ease.InSine)
            );
        }

        public void Fade(float endValue, float duration, Action onFinish)
        {
            sequenceHelper.Sequence(
                sequenceHelper.Fade(endValue: endValue, duration).SetEase(Ease.InSine),
                sequenceHelper.OnFinish(() => onFinish?.Invoke())
            );
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