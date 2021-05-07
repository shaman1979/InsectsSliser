using Slicer.Shop;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Slicer.Shop.ItemsShop;

namespace Slicer.Tools
{
    public static class ResourcesLoader
    {
        public static T[] Load<T>(string path) where T : ScriptableObject
        {
            var items = Resources.LoadAll<T>(path);

            if(items.Length == 0)
            {
                Debug.LogWarning($"Элементы по пути {path} отсутствуют!!!");
            }

            return items;
        }

        public static Dictionary<ItemTypes, DoublyLinkedList<ShopItem>> ShopElementsLoad(string knifesPath, string tablesPath)
        {
            var knifeList = Load<ShopItem>(knifesPath);
            var tableList = Load<ShopItem>(tablesPath);

            var itemList = new List<ShopItem>(knifeList.Length + tableList.Length);

            itemList.AddRange(knifeList);
            itemList.AddRange(tableList);

            if(itemList.Count().Equals(0))
            {
                Debug.LogError($"Список элементов пуст!!!");
                return null;
            }

            var itemDictionary = new Dictionary<ItemTypes, DoublyLinkedList<ShopItem>>();

            foreach (var item in itemList)
            {
                if(!itemDictionary.ContainsKey(item.Type))
                {
                    itemDictionary.Add(item.Type, new DoublyLinkedList<ShopItem>());
                }

                itemDictionary[item.Type].AddElement(item);
            }

            return itemDictionary;
        }
    }
}