using DG.Tweening;
using LightDev.Core;
using Slicer.Levels;
using System;
using Tools;
using UnityEngine;

namespace Slicer.Cutter
{
    public class CutterMovening : Base
    {
        public event Action OnFinished;
        
        [SerializeField]
        private float defaultSpeed;

        [SerializeField]
        private float minPositionZ;

        [SerializeField]
        private float maxPositionZ;

        private float speed;

        private void Start()
        {     
            LevelModifyEvents.SpeedChanged += SpeedUp;
            
            SpeedUp(1);
        }

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

        private void SpeedUp(float acceleration)
        {
            speed = defaultSpeed / acceleration;
        }

        private void StartIdleState()
        {
            float upPosition = transform.GetPositionY() + 0.1f;
            float position = transform.GetPositionY();
            Sequence(
              MoveY(upPosition, 0.6f).SetEase(Ease.InOutQuad),
              MoveY(position, 0.6f).SetEase(Ease.InOutQuad)
            ).SetLoops(-1);
        }

        private void MoveningState()
        {
            OnFinished?.Invoke();

            Sequence(
                transform.DOLocalMoveZ(maxPositionZ, speed, false).SetEase(Ease.InSine), 
                transform.DOLocalMoveZ(minPositionZ, speed, false).SetEase(Ease.InSine))
                .SetLoops(-1);
        }
    }
}