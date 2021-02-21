using UnityEngine;

using LightDev;
using LightDev.Core;
using LightDev.UI;

using DG.Tweening;

namespace MeshSlice.UI
{
    public class Start : CanvasElement
    {
        [Header("References")]
        public Base TapToStart;
        public Base LogoHolder;

        [Header("Info Text")]
        public Base Knob;
        public float KnobWidth;
        public BaseText LevelText;
        public BaseText HpText;

        public override void Subscribe()
        {
            Events.PreReset += Show;
            Events.GameStart += Hide;
        }

        public override void Unsubscribe()
        {
            Events.PreReset -= Show;
            Events.GameStart -= Hide;
        }

        protected override void OnStartShowing()
        {
            UpdateTexts();

            ShowTapToStart();
            ShowLogoHolder();
        }

        protected override void OnStartHiding()
        {
            HideTapToStart();
            HideLogoHolder();
        }

        private void UpdateTexts()
        {
            HpText.SetText($"xp: {HPManager.GetHP()}");
            LevelText.SetText($"level: {LevelsManager.GetLevel()}");

            float hpWidth = HpText.GetTextComponent().preferredWidth;
            float levelWidth = LevelText.GetTextComponent().preferredWidth;

            float fullSize = hpWidth + KnobWidth + levelWidth;

            float hpPos = -fullSize / 2 + hpWidth / 2;
            float knobPos = hpPos + hpWidth / 2 + KnobWidth / 2;
            float levelPos = knobPos + KnobWidth / 2 + levelWidth / 2;

            HpText.SetPositionX(hpPos);
            Knob.SetPositionX(knobPos);
            LevelText.SetPositionX(levelPos);
        }

        private void ShowTapToStart()
        {
            TapToStart.SetFade(1);
            TapToStart.Sequence(
                TapToStart.Fade(0, 1).SetEase(Ease.InSine),
                TapToStart.Fade(1, 0.2f).SetEase(Ease.InSine)
            ).SetLoops(-1);
        }

        private void ShowLogoHolder()
        {
            LogoHolder.SetPositionY(500);
            LogoHolder.Sequence(
                LogoHolder.MoveY(-354, 0.5f).SetEase(Ease.OutBack)
            );
        }

        private void HideLogoHolder()
        {
            LogoHolder.KillSequences();
            LogoHolder.Sequence(
              LogoHolder.MoveY(500, 0.3f).SetEase(Ease.OutBack)
          );
        }

        private void HideTapToStart()
        {
            TapToStart.KillSequences();
            TapToStart.Sequence(
                TapToStart.Fade(0, 0.2f).SetEase(Ease.InSine)
            );
        }
    }
}
