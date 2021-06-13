using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using LightDev.Core;

namespace LightDev.UI
{
    [RequireComponent(typeof(Image))]
    public class ButtonScale : BaseButton
    {
        private SequenceHelper sequenceHelper;

        protected override void Awake()
        {
            sequenceHelper = new SequenceHelper(transform);
            base.Awake();
        }

        protected override void AnimatePress()
        {
            sequenceHelper.KillSequences();
            sequenceHelper.Sequence(target.transform.DOScale(0.8f, 0.1f));
        }

        protected override void AnimateUnpress()
        {
            sequenceHelper.KillSequences();
            sequenceHelper.Sequence(target.transform.DOScale(1f, 0.1f));
        }
    }
}