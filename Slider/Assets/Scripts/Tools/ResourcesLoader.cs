using Slicer.Shop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public static Dictionary<ItemTypes, List<ShopItem>> ShopElementsLoad(string path)
        {
            var itemList = Load<ShopItem>(path);

            var itemDictionary = new Dictionary<ItemTypes, List<ShopItem>>();

            foreach (var item in itemList)
            {
                if(!itemDictionary.ContainsKey(item.Type))
                {
                    itemDictionary.Add(item.Type, new List<ShopItem>());
                }

                itemDictionary[item.Type].Add(item);
            }

            return itemDictionary;
        }
    }
}