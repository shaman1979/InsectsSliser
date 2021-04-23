using Slicer.Items;
using Slicer.Shop.Events;
using Slicer.Tools;
using Slicer.UI.Windows;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Slicer.Shop
{
    public partial class ItemsShop : MonoBehaviour
    {
        [SerializeField]
        private ShopWindow window;

        [SerializeField]
        private SelectedItems selectedItems;

        [SerializeField]
        private string itemPath = "ShopItems/";

        private Dictionary<ItemTypes, DoublyLinkedList<ShopItem>> items;

        private ItemTypes currentType;

        private void Awake()
        {
            items = ResourcesLoader.ShopElementsLoad(itemPath);

            ShopEvents.ItemTypeChanged += ChangeCurrentType;

            window.NextElementSwitched += NextElement;
            window.BackElementSwitched += BackElement;
            window.ElementSelected += SelectItem;
        }

        private void ChangeCurrentType(ItemTypes types)
        {
            currentType = types;
            UpdateItem(items[types].SelectedElement);
        }

        private void NextElement()
        {
            UpdateItem(items[currentType].NextElement());
        }

        private void BackElement()
        {
            UpdateItem(items[currentType].BackElement());
        }

        private void SelectItem()
        {
            items[currentType].Selection();
            selectedItems.SelectedItemChange(items[currentType].CurrentElement);
        }

        private void UpdateItem(ShopItem item)
        {
            items[currentType].ChangeCurrentElement(item);
            ShopEvents.ItemChanged.Call(item, item.GetStatus());
        }
    }
}