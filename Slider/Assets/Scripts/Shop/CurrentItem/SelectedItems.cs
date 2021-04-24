using Slicer.Items.Events;
using Slicer.Shop;
using Slicer.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Items
{
    public class SelectedItems : MonoBehaviour
    {
        private const string CurrentKnifeSaveKey = "Knife";
        private const string CurrentTableSaveKey = "Table";
        private const string itemPath = "ShopItems/";

        private static Dictionary<ItemTypes, ShopItem> selectedItems = new Dictionary<ItemTypes, ShopItem>();

        public static bool IsItemSelected(ShopItem item)
        {
            if(item.Id.Equals(GetCurrentKnifeID()) || item.Id.Equals(GetCurrentTableID()))
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
            LoadItems();

            if (selectedItems.Count.Equals(0))
            {
                LoadDefaultItem();
            }
        }

        private void Start()
        {
            ItemEvents.ItemInitialize.Call(selectedItems[ItemTypes.Khife]);
            ItemEvents.ItemInitialize.Call(selectedItems[ItemTypes.Table]);
        }

        private void LoadDefaultItem()
        {
            var items = ResourcesLoader.ShopElementsLoad(itemPath);

            SelectedItemChange(items[ItemTypes.Khife].CurrentElement);
            SelectedItemChange(items[ItemTypes.Table].CurrentElement);
        }

        public void SelectedItemChange(ShopItem item)
        {
            if(selectedItems.ContainsKey(item.Type))
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
            var items = ResourcesLoader.Load<ShopItem>(itemPath);
            var knifeId = PlayerPrefs.GetInt(CurrentKnifeSaveKey);
            var tableId = PlayerPrefs.GetInt(CurrentTableSaveKey);

            foreach (var item in items)
            {
                if(knifeId.Equals(item.Id) || tableId.Equals(item.Id))
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