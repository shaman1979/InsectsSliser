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
                OnFinish(Movening));
        }

        public void StopMovening()
        {
            KillSequences();
        }

        private void Movening()
        {
            Sequence(
                transform.DOLocalMoveZ(maxPositionZ, speed, false).SetEase(Ease.InSine), 
                transform.DOLocalMoveZ(minPositionZ, speed, false).SetEase(Ease.InSine))
                .SetLoops(-1);
        }
    }
}