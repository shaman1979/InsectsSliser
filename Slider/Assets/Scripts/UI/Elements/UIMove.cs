using System;
using DG.Tweening;
using LightDev.Core;
using Tools;
using UnityEngine;

namespace UI.Elements
{
    public class UIMove : MonoBehaviour
    {
        [SerializeField] private Vector3 activePosition;
        [SerializeField] private Vector3 deActivePosition;
        [SerializeField] private float duration;

        private SequenceHelper sequenceHelper;

        public void Setup(Vector3 activePosition, Vector3 deActivePosition)
        {
            this.activePosition = activePosition;
            this.deActivePosition = deActivePosition;
            sequenceHelper = new SequenceHelper(transform);
        }
        
        private void Awake()
        {
            sequenceHelper = new SequenceHelper(transform);
        }

        public void Activate()
        {
            Move(activePosition);
        }

        public void Deactivate()
        {
            Move(deActivePosition);
        }

        private void Move(Vector3 position)
        {
            sequenceHelper.Sequence(
                sequenceHelper.Move(position, duration).SetEase(Ease.OutBack)
            );
        }

        private void OnDestroy()
        {
            sequenceHelper = null;
        }
    }
}