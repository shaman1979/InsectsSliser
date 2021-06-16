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
using Tools;
using UI.Elements;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MeshSlice.UI
{
    public class GameWindow : CanvasElement
    {
        [SerializeField] private UIMove holder;
        
        [SerializeField]
        private Text levelText;
        
        [SerializeField]
        private Text progressText;

        [SerializeField]
        public Image progressImage;

        [Inject] private LevelsInitializer levelsInitializer;

        private int maxProgress;
        private int currentProgress;

        public void Setup(Text levelText, Text progressText, Image progressImage, UIMove holder)
        {
            this.levelText = levelText;
            this.progressText = progressText;
            this.progressImage = progressImage;
            this.holder = holder;
        }
        
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
            holder.Deactivate();
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
            if (maxProgress.IsZero().AssertTry($"Значение {maxProgress} не может быть равно нулю"))
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

            holder.transform.SetPositionY(500);
        }

        protected override void OnFinishShowing()
        {
            holder.Activate();
        }
    }
}