using Slicer.Shop.Events;
using Slicer.Tools;
using Slicer.UI.Windows;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Slicer.Shop
{
    public class ItemsShop : MonoBehaviour
    {
        [SerializeField]
        private ShopWindow window;

        [SerializeField]
        private string itemPath = "ShopItems/";

        private Dictionary<ItemTypes, List<ShopItem>> items;

        private int currentElementNumber;
        private ItemTypes currentType;
        private ShopItem currentElement;

        private void Awake()
        {
            items = ResourcesLoader.ShopElementsLoad(itemPath);

            ShopEvents.ItemTypeChanged += ChangeCurrentType;

            window.NextElementSwitched += NextElement;
            window.BackElementSwitched += BackElement;
        }

        private void ChangeCurrentType(ItemTypes types)
        {
            currentType = types;
            currentElementNumber = 0;
            UpdateItem(items[types].FirstOrDefault());
        }

        private void NextElement()
        {
            var currentItemCollection = items[currentType];

            if (currentElementNumber >= currentItemCollection.Count - 1)
            {
                currentElementNumber = 0;
            }
            else
            {
                currentElementNumber++;
            }

            UpdateItem(currentItemCollection[currentElementNumber]);
        }

        private void BackElement()
        {
            var currentItemCollection = items[currentType];

            if (currentElementNumber <= 0)
            {
                currentElementNumber = currentItemCollection.Count - 1;
            }
            else
            {
                currentElementNumber--;
            }

            UpdateItem(currentItemCollection[currentElementNumber]);
        }

        private void UpdateItem(ShopItem item)
        {
            currentElement = item;
            ShopEvents.ItemChanged.Call(currentElement);
        }
    }
}