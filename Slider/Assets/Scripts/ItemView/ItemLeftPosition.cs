using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Items
{
    public class ItemLeftPosition : IItemPosition
    {
        private float speed;
        private float xOffset;

        public ItemLeftPosition(float speed, float xOffset)
        {
            this.speed = speed;
            this.xOffset = xOffset;
        }

        public Item ItemPosition(Item prefab, Item lastItem, Vector3 origin, Action onFinished)
        {
            prefab.SetPosition(NewPositionCalculate(origin));

            prefab.Activate();
            prefab.Move(origin, speed, () => onFinished?.Invoke());

            if (prefab.Id.Equals(lastItem.Id))
                return prefab;

            lastItem.Move(LastPoint(origin), speed, () =>
            {
                lastItem.gameObject.SetActive(false);
            });

            return prefab;
        }

        private Vector3 LastPoint(Vector3 itemPosition)
        {
            itemPosition.x -= xOffset;
            return itemPosition;
        }

        private Vector3 NewPositionCalculate(Vector3 itemPosition)
        {
            itemPosition.x += xOffset;
            return itemPosition;
        }
    }
}