using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using LightDev.Core;

namespace LightDev.UI
{
    [RequireComponent(typeof(Image))]
    public class ButtonColor : BaseButton
    {
        [Space] public Color pressedColor = new Color(200 / 255f, 200 / 255f, 200 / 255f, 255 / 255f);

        protected Color normalColor;

        private SequenceHelper sequenceHelper;

        protected override void Awake()
        {
            sequenceHelper = new SequenceHelper(transform);
            base.Awake();

            normalColor = target.color;
        }

        protected override void AnimatePress()
        {
            sequenceHelper.KillSequences();
            sequenceHelper.Sequence(target.DOColor(pressedColor, 0.2f));
        }

        protected override void AnimateUnpress()
        {
            sequenceHelper.KillSequences();
            sequenceHelper.Sequence(target.DOColor(normalColor, 0.2f));
        }
    }
}