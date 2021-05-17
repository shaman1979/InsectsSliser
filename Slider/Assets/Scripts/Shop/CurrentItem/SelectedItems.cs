using Slicer.Items.Events;
using Slicer.Shop;
using Slicer.Shop.Events;
using Slicer.Tools;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Slicer.Items
{
    public class SelectedItems : MonoBehaviour
    {
        private const string CurrentKnifeSaveKey = "Knife";
        private const string CurrentTableSaveKey = "Table";
        private const string knifesPath = "ShopItems/Knifes/";
        private const string tablesPath = "ShopItems/Tables/";

        private static Dictionary<ItemTypes, ShopItem> selectedItems = new Dictionary<ItemTypes, ShopItem>();

        public static bool IsItemSelected(ShopItem item)
        {
            if (item.Id.Equals(GetCurrentKnifeID()) || item.Id.Equals(GetCurrentTableID()))
            {
                return true;
            }

            return false;
        }

        public static ShopItem GetItemForType(ItemTypes itemTypes)
        {
            return selectedItems[itemTypes];
        }

        public static int GetCurrentKnifeID()
        {
            return selectedItems[ItemTypes.Khife].Id;
        }

        public static int GetCurrentTableID()
        {
            return selectedItems[ItemTypes.Table].Id;
        }

        private void Awake()
        {
            ItemEvents.ItemSelected += SelectedItemChange;
            ShopEvents.ShopHide += ResetItems;

            LoadItems();

            if (selectedItems.Count.Equals(0))
            {
                LoadDefaultItem();
            }
        }

        private void ResetItems()
        {
            ItemEvents.ItemInitialize.Call(selectedItems[ItemTypes.Khife], new ItemDefaultPosition());
            ItemEvents.ItemInitialize.Call(selectedItems[ItemTypes.Table], new ItemDefaultPosition());
        }

        private void Start()
        {
            ResetItems();
        }

        private void LoadDefaultItem()
        {
            var items = ResourcesLoader.ShopElementsLoad(knifesPath, tablesPath);

            SelectedItemChange(items[ItemTypes.Khife].CurrentElement);
            SelectedItemChange(items[ItemTypes.Table].CurrentElement);
        }

        public void SelectedItemChange(ShopItem item)
        {
            if (selectedItems.ContainsKey(item.Type))
            {
                selectedItems[item.Type] = item;
            }
            else
            {
                selectedItems.Add(item.Type, item);
            }

            SaveItem(item.Type);
        }

        private void LoadItems()
        {
            var items = ResourcesLoader.Load<ShopItem>(knifesPath).ToList();
            items.AddRange(ResourcesLoader.Load<ShopItem>(tablesPath));
            var knifeId = PlayerPrefs.GetInt(CurrentKnifeSaveKey);
            var tableId = PlayerPrefs.GetInt(CurrentTableSaveKey);

            foreach (var item in items)
            {
                if (knifeId.Equals(item.Id) || tableId.Equals(item.Id))
                {
                    SelectedItemChange(item);
                }
            }
        }

        private void SaveItem(ItemTypes types)
        {
            var key = string.Empty;
            var value = int.MinValue;

            switch (types)
            {
                case ItemTypes.Khife:
                    key = CurrentKnifeSaveKey;
                    value = GetCurrentKnifeID();
                    break;
                case ItemTypes.Table:
                    key = CurrentTableSaveKey;
                    value = GetCurrentTableID();
                    break;
                default:
                    break;
            }

            if (string.IsNullOrEmpty(key))
            {
                Debug.LogError($"Ключа для сохранение значение элемента с типом {types} не существует!!!");
                return;
            }

            PlayerPrefs.SetInt(key, value);
        }
    }
}