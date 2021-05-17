using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Items
{
    public class ItemDefaultPosition : IItemPosition
    {
        public Item ItemPosition(Item prefab, Item lastItem, Vector3 origin, Action onFinished)
        {
            lastItem.Deactivate();

            prefab.SetPosition(origin);
            prefab.Activate();

            onFinished?.Invoke();

            return prefab;
        }
    }
}