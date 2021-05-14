using LightDev.Core;
using Slicer.Shop.Events;
using Slicer.UI.Elements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Slicer.Shop
{
    [RequireComponent(typeof(ButtonElement))]
    public class SelectItem : Base
    {
        [SerializeField]
        private ItemTypes item;

        private void Awake()
        {
            GetComponent<ButtonElement>().AddListener(ChangeType);
        }

        private void ChangeType()
        {
            ShopEvents.ItemTypeChanged.Call(item);
        }
    }
}