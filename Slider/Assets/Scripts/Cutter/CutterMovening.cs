using DG.Tweening;
using LightDev.Core;
using System.Collections;
using System.Collections.Generic;
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

        private Vector3 startPosition = new Vector3(0, 2, -1);

        public void StartMovening()
        {
            KillSequences();

            this.Sequence(
                transform.DOLocalMoveZ(minPositionZ, 0.5f).SetEase(Ease.InSine),
                OnFinish(Movening));
        }

        public void StopMovening()
        {
            KillSequences();
        }

        private void Movening()
        {
            KillSequences();
            this.Sequence(
                transform.DOLocalMoveZ(maxPositionZ, speed).SetEase(Ease.InSine)
              /*  OnFinish(() => transform.DOLocalMoveZ(minPositionZ, speed).SetEase(Ease.InSine))*/
              ).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InSine);
        }
    }
}