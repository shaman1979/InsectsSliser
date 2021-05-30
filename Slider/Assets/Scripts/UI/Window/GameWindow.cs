using DG.Tweening;
using LightDev;
using LightDev.Core;
using LightDev.UI;
using Slicer.EventAgregators;
using Slicer.Game;
using Slicer.HP;
using Slicer.HP.Messages;
using Slicer.Logger;
using Slicer.Tools;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MeshSlice.UI
{
    public class GameWindow : CanvasElement
    {
        [Header("References")] public Base holder;
        public Base progress;

        [SerializeField]
        private Text levelText;
        
        [SerializeField]
        private Text progressText;

        [Header("Progress")] public Image progressImage;

        [Inject] private LevelsInitializer levelsInitializer;

        private int maxProgress;
        private int currentProgress;

        public override void Subscribe(IEventsAgregator eventAgregator)
        {
            Events.GameStart += Show;
            Events.PreReset += Hide;
            Events.GameFinish += OnGameFinish;

            eventAgregator.AddListener<CurrentProgressMessage>(message => UpdateCurrentProgress(message.Progress));
            eventAgregator.AddListener<MaxProgressMessage>(message => UpdateMaxProgress(message.MaxProgress));
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

        private void UpdateCurrentProgress(int progress)
        {
            currentProgress = progress;
            UpdateProgress();
        }

        private void UpdateMaxProgress(int progress)
        {
            maxProgress = progress;
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            if (maxProgress.IsZero().AssertTry($"Значение {progress} не может быть равно нулю"))
            {
                progressImage.fillAmount = 0f;
            }
            else
            {
                progressImage.fillAmount = (float) currentProgress / maxProgress;
            }

            progressText.SetText($"{currentProgress}/{maxProgress}");
        }

        protected override void OnStartShowing()
        {
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