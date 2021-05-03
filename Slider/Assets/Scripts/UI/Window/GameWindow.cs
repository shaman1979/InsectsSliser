using DG.Tweening;
using LightDev;
using LightDev.Core;
using LightDev.UI;
using Slicer.Game;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MeshSlice.UI
{
    public class GameWindow : CanvasElement
    {
        [Header("References")]
        public Base holder;
        public Base progress;

        [Header("Text")]
        public BaseText levelText;
        public BaseText progressText;

        [Header("Progress")]
        public Image progressImage;

        [Inject]
        private LevelsInitializer levelsInitializer;

        [Inject]
        private HPInitializer hpInitializer;

        public override void Subscribe()
        {
            Events.GameStart += Show;
            Events.PreReset += Hide;
            Events.ProgressChanged += OnProgressChanged;
            Events.GameFinish += OnGameFinish;
        }

        public override void Unsubscribe()
        {
            Events.GameStart -= Show;
            Events.PreReset -= Hide;
            Events.ProgressChanged -= OnProgressChanged;
            Events.GameFinish -= OnGameFinish;
        }

        private void OnGameFinish()
        {
            progress.Sequence(
              progress.MoveY(-1000, 0.4f).SetEase(Ease.InOutSine)
            );
        }

        private void OnProgressChanged()
        {
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            progressImage.fillAmount = (float)hpInitializer.GetCurrentProgress / hpInitializer.GetMaxProgress;
            progressText.SetText($"{hpInitializer.GetCurrentProgress}/{hpInitializer.GetMaxProgress}");
        }

        protected override void OnStartShowing()
        {
            UpdateProgress();
            levelText.SetText($"{levelsInitializer.GetLevelName()}");

            holder.SetPositionY(500);
            progress.SetPositionY(-400);
        }

        protected override void OnFinishShowing()
        {
            holder.Sequence(
              holder.MoveY(0, 0.4f).SetEase(Ease.OutBack)
            );
        }
    }
}
