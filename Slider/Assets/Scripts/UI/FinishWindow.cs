using UnityEngine;

using LightDev;
using LightDev.Core;
using LightDev.UI;

using DG.Tweening;
using Slicer.EventAgregators;
using Zenject;
using Slicer.Game;
using Slicer.HP;
using UI.Elements;
using UnityEngine.Serialization;

namespace MeshSlice.UI
{
    public class FinishWindow : CanvasElement
    {
        [SerializeField]
        private UIFade background;
        
        [SerializeField]
        private UIFade passedText;
        
        [SerializeField]
        private UIFade hpTextFade;
        
        [SerializeField]
        public UIFade tapToReplay;

        [SerializeField] private BaseText hpText;

        [Inject]
        private HpInitializer hpInitializer;

        private bool isHpFilled;

        public override void Subscribe(IEventsAgregator eventAgregator)
        {
            Events.GameFinish += Show;
            Events.PreReset += Hide;
            Events.HpChanged += UpdateHpText;
            Events.HpFilled += OnHpFilled;
            Events.PointerUp += OnPointerUp;
        }

        public override void Unsubscribe()
        {
            Events.GameFinish -= Show;
            Events.PreReset -= Hide;
            Events.HpChanged -= UpdateHpText;
            Events.HpFilled -= OnHpFilled;
            Events.PointerUp -= OnPointerUp;
        }

        private void OnHpFilled()
        {
            isHpFilled = true;
            ShowTapToReplay();
        }

        private void OnPointerUp()
        {
            if (isHpFilled)
            {
                isHpFilled = false;
                Events.RequestSplash.Call();
            }
        }

        private void ShowTapToReplay()
        {
            tapToReplay.StartFade();
        }

        protected override void OnStartShowing()
        {
            background.SetFade(0);
            passedText.SetFade(0);
            hpTextFade.SetFade(0);
            tapToReplay.SetFade(0);
        }

        protected override void OnFinishShowing()
        {
            ShowBackground();
            ShowPassedText();
            ShowHP();
        }

        private void ShowBackground()
        {
            background.Fade(0.4f, 0.2f,Events.RequestHpFill.Call);
        }

        private void ShowPassedText()
        {
            passedText.Fade(1, 0.2f, null);
        }

        private void ShowHP()
        {
            UpdateHpText();
            
            hpTextFade.Fade(1, 0.2f, null);
        }

        private void UpdateHpText()
        {
            hpText.SetText($"XP: {hpInitializer.GetHP}");
        }
    }
}
