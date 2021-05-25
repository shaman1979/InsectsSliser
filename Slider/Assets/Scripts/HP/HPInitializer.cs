using DG.Tweening;
using LightDev;
using Slicer.Application.Storages;
using System;
using Slicer.EventAgregators;
using Slicer.Game;
using Slicer.HP.Messages;
using Slicer.Logger;
using Slicer.Tools;
using UnityEngine;
using Zenject;

namespace Slicer.HP
{
    public class HpInitializer : IInitializable, IDisposable
    {
        private const int XP_FOR_SLISED_OBJECT = 50;
        private readonly LevelsInitializer levelsInitializer;
        private readonly IEventsAgregator eventsAgregator;


        private int maxProgress;

        public HpInitializer(LevelsInitializer levelsInitializer, IEventsAgregator eventsAgregator)
        {
            this.levelsInitializer = levelsInitializer;
            this.eventsAgregator = eventsAgregator;
        }

        public int GetHP => PlayerPrefs.GetInt(PlayerPrefsKeyStorage.XP, 0);

        public int CurrentProgress { get; private set; }

        public int GetMaxProgress => levelsInitializer.GetMeshesCountOnLevel() * XP_FOR_SLISED_OBJECT;

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

        public void IncreaseProgress(float progress)
        {
            if (progress.IsNegative().AssertTry($"Значение {nameof(progress)} не может быть отрицательным."))
                return;

            if (progress.IsMore(1).AssertTry($"Значение {nameof(progress)} не может быть больше 1."))
                return;
            
            SetProgress(CurrentProgress + (int)(progress * XP_FOR_SLISED_OBJECT));
        }

        private void SetHP(int value)
        {
            PlayerPrefs.SetInt(PlayerPrefsKeyStorage.XP, value);
            Events.HpChanged.Call();
        }
        
        private void SetProgress(int value)
        {
            CurrentProgress = value;
            eventsAgregator.Invoke(new CurrentProgressMessage(CurrentProgress, GetMaxProgress));
        }

        private void OnPostReset()
        {
            CurrentProgress = 0;
        }

        private void OnRequestHpFill()
        {
            var progress = CurrentProgress;
            var hp = GetHP;

            var sequences = DOTween.Sequence();

            sequences.Append(DOTween.Sequence().AppendInterval(0.5f));
            sequences.Append(DOTween.To((value) =>
            {
                var p = (int)Mathf.Lerp(0, progress, value);
                SetProgress(progress - p);
                SetHP(hp + p);
            }, 0, 1, 1));

            sequences.Append(DOTween.Sequence().AppendInterval(0.5f));
            sequences.Append(DOTween.Sequence().AppendCallback(() => Events.HpFilled.Call()));
        }
    }
}
