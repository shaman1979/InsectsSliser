using Slicer.Shop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Items
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField]
        private Item currentItem;

        [SerializeField]
        private float offset = 7f;

        [SerializeField]
        private float speed = 2f;

        private Dictionary<int, Item> pool = new Dictionary<int, Item>();

        public void UpdateView(Item item, int id)
        {
            if (currentItem != null)
            {        
                currentItem.Move(LastPoint(transform.position), speed, currentItem.Deactivate);
            }

            var transformItem = currentItem.transform;
            var newItem = CreateItem(item, NewPositionCalculate(transform.position), transformItem.rotation, transformItem.parent, id);

            newItem.SetPosition(NewPositionCalculate(transform.position));
            newItem.Move(transform.position, speed);

            currentItem = newItem;
            currentItem.Activate();
        }

        private Vector3 LastPoint(Vector3 itemPosition)
        {
            itemPosition.x -= offset;
            return itemPosition;
        }

        private Vector3 NewPositionCalculate(Vector3 itemPosition)
        {
            itemPosition.x += offset;
            return itemPosition;
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