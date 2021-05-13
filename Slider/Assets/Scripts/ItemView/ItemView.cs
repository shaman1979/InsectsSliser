using Slicer.Shop;
using Slicer.Shop.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Items
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField]
        private Item currentItem;

        private Dictionary<int, Item> pool = new Dictionary<int, Item>();

        public void UpdateView(Item item, int id, bool isOutlineDraw = false)
        {
            if (currentItem != null)
            {
                currentItem?.Deactivate();
                currentItem.OutlineClear();
            }

            var transformItem = currentItem.transform;
            var newItem = CreateItem(item, transformItem.position, transformItem.rotation, transformItem.parent, id);

            currentItem = newItem;
            currentItem?.Activate();

            if(isOutlineDraw)
            {
                currentItem.OutlineDraw();
            }
        }

        private void Start()
        {
            ShopEvents.ShopShow += () => currentItem.OutlineDraw();
            ShopEvents.ShopHide += () => currentItem.OutlineClear();
        }

        private Item CreateItem(Item prefab, Vector3 position, Quaternion rotation, Transform parent, int id)
        {
            if (!pool.ContainsKey(id))
            {
                pool.Add(id, Instantiate(prefab, position, rotation, parent));
            }

            return pool[id];
        }
    }
}