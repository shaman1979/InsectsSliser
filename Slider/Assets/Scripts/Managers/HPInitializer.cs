using DG.Tweening;
using LightDev;
using System;
using UnityEngine;
using Zenject;

namespace Slicer.Game
{
    public class HPInitializer : IInitializable, IDisposable
    {
        private const int XP_FOR_SLISED_OBJECT = 50;
        private const string XP_KEY = "xp";

        [Inject]
        private readonly LevelsInitializer levelsInitializer;


        private int currentProgress;
        private int maxProgress;

        public HPInitializer(LevelsInitializer levelsInitializer)
        {
            this.levelsInitializer = levelsInitializer;
        }

        public int GetHP
        {
            get => PlayerPrefs.GetInt(XP_KEY, 0);
        }

        public int GetCurrentProgress
        {
            get => currentProgress;
        }

        public int GetMaxProgress
        {
            get => levelsInitializer.GetMeshesCountOnLevel() * XP_FOR_SLISED_OBJECT;
        }

        public void Initialize()
        {
            Events.PostReset += OnPostReset;
            Events.RequestHpFill += OnRequestHpFill;
        }

        public void Dispose()
        {
            Events.PostReset -= OnPostReset;
            Events.RequestHpFill -= OnRequestHpFill;
        }

        public void IncreaseProgress(float value)
        {
            SetProgress(GetCurrentProgress + (int)(value * XP_FOR_SLISED_OBJECT));
        }

        private void SetHP(int value)
        {
            PlayerPrefs.SetInt(XP_KEY, value);
            Events.HpChanged.Call();
        }
        private void SetProgress(int value)
        {
            currentProgress = value;
            Events.ProgressChanged.Call();
        }

        private void OnPostReset()
        {
            currentProgress = 0;
        }

        private void OnRequestHpFill()
        {
            int progress = currentProgress;
            int hp = GetHP;

            var sequences = DOTween.Sequence();

            sequences.Append(DOTween.Sequence().AppendInterval(0.5f));
            sequences.Append(DOTween.To((value) =>
            {
                int p = (int)Mathf.Lerp(0, progress, value);
                SetProgress(progress - p);
                SetHP(hp + p);
            }, 0, 1, 1));

            sequences.Append(DOTween.Sequence().AppendInterval(0.5f));
            sequences.Append(DOTween.Sequence().AppendCallback(() => Events.HpFilled.Call()));
        }
    }
}
