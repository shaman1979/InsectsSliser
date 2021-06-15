using DG.Tweening;
using LightDev;
using LightDev.Core;
using LightDev.UI;
using Slicer.EventAgregators;
using Slicer.Game;
using Slicer.HP;
using Slicer.Shop.Events;
using Slicer.UI.Elements;
using UI.Elements;
using UnityEngine;
using Zenject;

namespace Slicer.UI.Windows
{
    public class MenuWindow : CanvasElement
    {
        [SerializeField] private ButtonElement shopButton;

        [SerializeField] private UIFade tapToStart;

        [SerializeField] private UIMove logoHolder;

        [SerializeField] private Base knob;

        [SerializeField] private float knobWidth;

        [SerializeField] private BaseText levelText;

        [SerializeField] private BaseText hpText;

        [SerializeField] private BaseText starText;

        [SerializeField] private UIMove buttonContainer;

        [Inject] private LevelsInitializer levelsInitializer;

        [Inject] private HpInitializer hpInitializer;

        public override void Subscribe(IEventsAgregator eventAgregator)
        {
            Events.PreReset += Show;
            Events.GameStart += Hide;
            ShopEvents.ShopShow += Hide;
            ShopEvents.ShopHide += Show;
        }

        public override void Unsubscribe()
        {
            Events.PreReset -= Show;
            Events.GameStart -= Hide;
            ShopEvents.ShopShow -= Hide;
            ShopEvents.ShopHide -= Show;
        }

        protected override void OnStartShowing()
        {
            UpdateTexts();

            ShowTapToStart();
            ShowLogoHolder();
            ShowButtonContainer();
            ShopButtonInitialize();
        }

        protected override void OnStartHiding()
        {
            HideTapToStart();
            HideLogoHolder();
            HideButtonContainer();
        }

        private void HideButtonContainer()
        {
            buttonContainer.Deactivate();
        }

        private void UpdateStarCount()
        {
            starText.SetText(StarsActivator.GetTotalStar().ToString());
        }

        private void UpdateTexts()
        {
            hpText.SetText($"xp: {hpInitializer.GetHP}");
            levelText.SetText($"level: {levelsInitializer.GetLevel()}");

            var hpWidth = hpText.GetTextComponent().preferredWidth;
            var levelWidth = levelText.GetTextComponent().preferredWidth;

            var fullSize = hpWidth + knobWidth + levelWidth;

            UpdateStarCount();

            var hpPos = -fullSize / 2 + hpWidth / 2;
            var knobPos = hpPos + hpWidth / 2 + knobWidth / 2;
            var levelPos = knobPos + knobWidth / 2 + levelWidth / 2;

            hpText.SetPositionX(hpPos);
            knob.SetPositionX(knobPos);
            levelText.SetPositionX(levelPos);
        }

        private void ShowTapToStart()
        {
            tapToStart.StartFade();
        }

        private void ShowLogoHolder()
        {
            logoHolder.Activate();
        }

        private void HideLogoHolder()
        {
            logoHolder.Deactivate();
        }

        private void ShopButtonInitialize()
        {
            shopButton.ListenerClear();
            shopButton.AddListener(() => ShopEvents.ShopShow.Call());
        }

        private void HideTapToStart()
        {
            //TODO: Доделать потом
            // tapToStart.KillSequences();
            // tapToStart.Sequence(
            //     tapToStart.Fade(0, 0.2f).SetEase(Ease.InSine)
            // );
        }

        private void ShowButtonContainer()
        {
            buttonContainer.Activate();
        }
    }
}