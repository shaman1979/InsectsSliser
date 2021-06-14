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

        public SequenceHelper SequenceHelper { get; private set; }

        private float speed;

        private void Awake()
        {
            SequenceHelper = new SequenceHelper(transform);
            LevelModifyEvents.SpeedChanged += SpeedUp;
            
            SpeedUp(1);
        }
        
        public void StartMovening()
        {
            SequenceHelper.KillSequences();

            SequenceHelper.Sequence(
                transform.DOLocalMoveZ(minPositionZ, 0.5f).SetEase(Ease.InSine),
                SequenceHelper.OnFinish(MoveningState));

            StartIdleState();
        }

        public void StopMovening()
        {
            SequenceHelper.KillSequences();
        }

        private void SpeedUp(float acceleration)
        {
            speed = defaultSpeed / acceleration;
        }

        private void StartIdleState()
        {
            var upPosition = transform.GetPositionY() + 0.1f;
            var position = transform.GetPositionY();
            SequenceHelper.Sequence(
                SequenceHelper. MoveY(upPosition, 0.6f).SetEase(Ease.InOutQuad),
                SequenceHelper.MoveY(position, 0.6f).SetEase(Ease.InOutQuad)
            ).SetLoops(-1);
        }

        private void MoveningState()
        {
            OnFinished?.Invoke();

            SequenceHelper.Sequence(
                transform.DOLocalMoveZ(maxPositionZ, speed, false).SetEase(Ease.InSine), 
                transform.DOLocalMoveZ(minPositionZ, speed, false).SetEase(Ease.InSine))
                .SetLoops(-1);
        }
    }
}