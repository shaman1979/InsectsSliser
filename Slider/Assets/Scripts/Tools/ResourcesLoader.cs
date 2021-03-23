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
    }
}