using Slicer.Shop;
using System.Collections;
using System.Collections.Generic;
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

        public static Dictionary<ItemTypes, DoublyLinkedList<ShopItem>> ShopElementsLoad(string path)
        {
            var itemList = Load<ShopItem>(path);

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