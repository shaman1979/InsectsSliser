using Slicer.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Shop
{
    public class ItemsShop : MonoBehaviour
    {
        private string itemPath = "ShopItems/";

        private ShopItem[] items;

        private void Awake()
        {
            items = ResourcesLoader.Load<ShopItem>(itemPath);

            foreach (var item in items)
            {
                Debug.Log($"Item name {item.Name}");
            }
        }
    }
}