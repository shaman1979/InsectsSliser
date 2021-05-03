using DG.Tweening;
using LightDev;
using LightDev.Core;
using LightDev.UI;
using Slicer.Game;
using Slicer.Shop.Events;
using Slicer.UI.Elements;
using UnityEngine;
using Zenject;

namespace Slicer.UI.Windows
{
    public class MenuWindow : CanvasElement
    {
        [Header("Buttons")]
        [SerializeField]
        private ButtonElement shopButton;

        [Header("References")]
        [SerializeField]
        private Base tapToStart;

        [SerializeField]
        private Base logoHolder;

        [Header("Info Text")]
        [SerializeField]
        private Base knob;

        [SerializeField]
        private float knobWidth;

        [SerializeField]
        private BaseText levelText;

        [SerializeField]
        private BaseText hpText;

        [SerializeField]
        private BaseText starText;

        [Inject]
        private LevelsInitializer levelsInitializer;

        [Inject]
        private HPInitializer hpInitializer;

        public override void Subscribe()
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

            ShopButtonInitialize();
        }

        protected override void OnStartHiding()
        {
            HideTapToStart();
            HideLogoHolder();
        }

        private void UpdateStarCount()
        {
            starText.SetText(StarsActivator.GetTotalStar());
        }

        private void UpdateTexts()
        {
            hpText.SetText($"xp: {hpInitializer.GetHP}");
            levelText.SetText($"level: {levelsInitializer.GetLevel()}");

            float hpWidth = hpText.GetTextComponent().preferredWidth;
            float levelWidth = levelText.GetTextComponent().preferredWidth;

            float fullSize = hpWidth + knobWidth + levelWidth;

            UpdateStarCount();

            float hpPos = -fullSize / 2 + hpWidth / 2;
            float knobPos = hpPos + hpWidth / 2 + knobWidth / 2;
            float levelPos = knobPos + knobWidth / 2 + levelWidth / 2;

            hpText.SetPositionX(hpPos);
            knob.SetPositionX(knobPos);
            levelText.SetPositionX(levelPos);
        }

        private void ShowTapToStart()
        {
            tapToStart.SetFade(1);
            tapToStart.Sequence(
                tapToStart.Fade(0, 1).SetEase(Ease.InSine),
                tapToStart.Fade(1, 0.2f).SetEase(Ease.InSine)
            ).SetLoops(-1);
        }

        private void ShowLogoHolder()
        {
            logoHolder.SetPositionY(500);
            logoHolder.Sequence(
                logoHolder.MoveY(-354, 0.5f).SetEase(Ease.OutBack)
            );
        }

        private void HideLogoHolder()
        {
            logoHolder.KillSequences();
            logoHolder.Sequence(
              logoHolder.MoveY(500, 0.3f).SetEase(Ease.OutBack)
          );
        }

        private void ShopButtonInitialize()
        {
            shopButton.ListenerClear();
            shopButton.AddListener(() => ShopEvents.ShopShow.Call());
        }

        private void HideTapToStart()
        {
            tapToStart.KillSequences();
            tapToStart.Sequence(
                tapToStart.Fade(0, 0.2f).SetEase(Ease.InSine)
            );
        }
    }
}
