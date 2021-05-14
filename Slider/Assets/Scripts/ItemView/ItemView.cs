using Slicer.Shop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Items
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField]
        private GameObject currentItem;

        private Dictionary<int, GameObject> pool = new Dictionary<int, GameObject>();

        public void UpdateView(GameObject item, int id)
        {
            if (currentItem != null)
            {
                currentItem?.SetActive(false);
            }

            var transformItem = currentItem.transform;
            var newItem = CreateItem(item, transformItem.position, transformItem.rotation, transformItem.parent, id);

            currentItem = newItem;
            currentItem?.SetActive(true);
        }

        private GameObject CreateItem(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent, int id)
        {
            if (!pool.ContainsKey(id))
            {
                pool.Add(id, Instantiate(prefab, position, rotation, parent));
            }

            return pool[id];
        }
    }
}