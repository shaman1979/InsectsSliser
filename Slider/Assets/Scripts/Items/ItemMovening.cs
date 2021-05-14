using DG.Tweening;
using System;
using UnityEngine;

namespace Slicer.Items
{
    public class ItemMovening
    {
        private Transform selfTransfrom;
        private float speed;

        public ItemMovening(Transform selfTransfrom, float speed)
        {
            this.selfTransfrom = selfTransfrom;
            this.speed = speed;
        }

        public void Move(Vector3 from, Action onFinished)
        {
            selfTransfrom.DOMove(from, speed).OnComplete(() => onFinished?.Invoke());
        }
    }
}