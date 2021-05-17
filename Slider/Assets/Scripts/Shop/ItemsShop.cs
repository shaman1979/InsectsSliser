using Slicer.Game;
using Slicer.Items;
using Slicer.Shop.Events;
using Slicer.Tools;
using Slicer.UI.Windows;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Slicer.Shop
{
    public partial class ItemsShop : MonoBehaviour
    {
        private const string knifePath = "ShopItems/Knifes/";
        private const string tablePath = "ShopItems/Tables/";

        [SerializeField]
        private ShopWindow window;

        [SerializeField]
        private SelectedItems selectedItems;

        [SerializeField]
        private float speed = 2f;

        [SerializeField]
        private float xOffset = 7f;

        [Inject]
        private LevelsInitializer levelsInitializer;

        private Dictionary<ItemTypes, DoublyLinkedList<ShopItem>> items;

        private ItemTypes currentType;

        

        private void Awake()
        {
            items = ResourcesLoader.ShopElementsLoad(knifePath, tablePath);

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
            items[type].ChangeSelectedElement(currentItem);
        }

        private void ChangeCurrentType(ItemTypes types)
        {
            currentType = types;
            UpdateItem(items[types].SelectedElement, Direction.Middle);
        }

        private void NextElement()
        {
            UpdateItem(items[currentType].NextElement(), Direction.Left);
        }

        private void BackElement()
        {
            UpdateItem(items[currentType].BackElement(), Direction.Right);
        }

        private void SelectItem()
        {
            items[currentType].Selection();
            selectedItems.SelectedItemChange(items[currentType].CurrentElement);
            UpdateItem(items[currentType].SelectedElement, Direction.Middle);
        }

        private void UpdateItem(ShopItem item, Direction direction)
        {
            items[currentType].ChangeCurrentElement(item);

            switch (direction)
            {
                case Direction.Left:
                    ShopEvents.ItemChanged.Call(item, item.GetStatus(levelsInitializer), new ItemLeftPosition(speed, xOffset));
                    break;
                case Direction.Right:
                    ShopEvents.ItemChanged.Call(item, item.GetStatus(levelsInitializer), new ItemRightPosition(speed, xOffset));
                    break;
                case Direction.Middle:
                    ShopEvents.ItemChanged.Call(item, item.GetStatus(levelsInitializer), new ItemDefaultPosition());
                    break;
                default:
                    break;
            }
        }

        private enum Direction
        {
            Left,
            Right,
            Middle
        }
    }
}