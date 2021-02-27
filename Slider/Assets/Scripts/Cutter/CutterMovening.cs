using DG.Tweening;
using LightDev.Core;
using UnityEngine;

namespace Slicer.Cutter
{
    public class CutterMovening : Base
    {
        [SerializeField]
        private float speed;

        [SerializeField]
        private float minPositionZ;

        [SerializeField]
        private float maxPositionZ;

        public void StartMovening()
        {
            KillSequences();

            Sequence(
                transform.DOLocalMoveZ(minPositionZ, 0.5f).SetEase(Ease.InSine),
                OnFinish(MoveningState));

            StartIdleState();
        }

        public void StopMovening()
        {
            KillSequences();
        }

        private void StartIdleState()
        {
            float upPosition = GetPositionY() + 0.1f;
            float position = GetPositionY();
            Sequence(
              MoveY(upPosition, 0.6f).SetEase(Ease.InOutQuad),
              MoveY(position, 0.6f).SetEase(Ease.InOutQuad)
            ).SetLoops(-1);
        }

        private void MoveningState()
        {
            Sequence(
                transform.DOLocalMoveZ(maxPositionZ, speed, false).SetEase(Ease.InSine), 
                transform.DOLocalMoveZ(minPositionZ, speed, false).SetEase(Ease.InSine))
                .SetLoops(-1);
        }
    }
}