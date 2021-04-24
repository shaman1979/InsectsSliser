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
            ShopEvents.ShopShow += ShopEnable;

            window.NextElementSwitched += NextElement;
            window.BackElementSwitched += BackElement;
            window.ElementSelected += SelectItem;
        }

        private void ShopEnable()
        {
            InitializeSelectedItem(ItemTypes.Khife);
            InitializeSelectedItem(ItemTypes.Table);
            ChangeCurrentType(ItemTypes.Table);
        }

        private void InitializeSelectedItem(ItemTypes type)
        {
            var selectedItem = SelectedItems.GetItemForType(type);
            var currentItem = items[type].FindElementByID(selectedItem.Id);
            items[type].ChangeCurrentElement(currentItem);
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
            UpdateItem(items[currentType].SelectedElement);
        }

        private void UpdateItem(ShopItem item)
        {
            items[currentType].ChangeCurrentElement(item);
            ShopEvents.ItemChanged.Call(item, item.GetStatus());
        }
    }
}