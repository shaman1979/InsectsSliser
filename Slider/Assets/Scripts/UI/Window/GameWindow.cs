using DG.Tweening;
using LightDev;
using LightDev.Core;
using LightDev.UI;
using Slicer.EventAgregators;
using Slicer.Game;
using Slicer.HP;
using Slicer.HP.Messages;
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
        private HpInitializer hpInitializer;

        private int maxProgress;

        public override void Subscribe(IEventsAgregator eventAgregator)
        {
            Events.GameStart += Show;
            Events.PreReset += Hide;
            eventAgregator.AddListener<CurrentProgressMessage>(message => UpdateProgress(message.Progress, message.LevelProgress));
            Events.GameFinish += OnGameFinish;
        }

        public override void Unsubscribe()
        {
            Events.GameStart -= Show;
            Events.PreReset -= Hide;
            Events.GameFinish -= OnGameFinish;
        }

        private void OnGameFinish()
        {
            progress.Sequence(
              progress.MoveY(-1000, 0.4f).SetEase(Ease.InOutSine)
            );
        }

        private void UpdateProgress(int currentProgress, int maxProgress)
        {
            progressImage.fillAmount = (float)currentProgress / maxProgress;
            progressText.SetText($"{currentProgress}/{maxProgress}");
        }

        private void UpdateMaxProgress(int maxProgress)
        {
            this.maxProgress = maxProgress;
        }
        

        protected override void OnStartShowing()
        {
            UpdateProgress(hpInitializer.CurrentProgress, hpInitializer.GetMaxProgress);
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
