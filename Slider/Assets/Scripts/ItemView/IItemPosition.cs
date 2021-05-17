using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Items
{
    public interface IItemPosition
    {
        Item ItemPosition(Item prefab, Item lastItem, Vector3 origin, Action onFinished);
    }
}