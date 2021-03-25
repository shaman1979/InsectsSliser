using LightDev.Core;
using Slicer.Shop.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Shop
{
    public class SelectItem : Base
    {
        [SerializeField]
        private ItemTypes item;

        private void Awake()
        {
            ShopEvents.ShopShow += Activate;
            ShopEvents.ShopHide += Deactivate;
        }

        private void OnDestroy()
        {
            ShopEvents.ShopShow -= Activate;
            ShopEvents.ShopHide -= Deactivate;
        }

        private void OnMouseDown()
        {
            ShopEvents.ItemTypeChanged.Call(item);
        }
    }
}