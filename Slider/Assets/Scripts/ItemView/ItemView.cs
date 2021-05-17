using Slicer.Shop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Items
{
    public partial class ItemView : MonoBehaviour
    {
        [SerializeField]
        private Item currentItem;

        [SerializeField]
        private float offset = 7f;

        [SerializeField]
        private float speed = 2f;

        private Dictionary<int, Item> pool = new Dictionary<int, Item>();

        private Queue<ChangePosition> changePositions = new Queue<ChangePosition>();

        private bool isChange = true;

        private IItemPosition itemPosition;

        public void UpdateView(Item item, int id, IItemPosition itemCreator)
        {
            if (itemPosition != null && !itemPosition.GetType().Equals(itemCreator.GetType()))
            {
                changePositions.Clear();
            }

            itemPosition = itemCreator;

            changePositions.Enqueue(new ChangePosition(currentItem, item, id, itemCreator));
        }

        private void Update()
        {
            if (changePositions.Count > 0 && isChange)
            {
                UpdateView(changePositions.Dequeue());
            }
        }

        private void UpdateView(ChangePosition changePosition)
        {

            var transformItem = currentItem.transform;
            var newItem = CreateItem(changePosition.Item,
                                     transform.position,
                                     transformItem.rotation,
                                     transformItem.parent,
                                     changePosition.Id);

            isChange = false;

            currentItem = changePosition.ItemCreator.ItemPosition(newItem, currentItem, transform.position, () => isChange = true);
        }

        private Item CreateItem(Item prefab, Vector3 position, Quaternion rotation, Transform parent, int id)
        {
            if (!pool.ContainsKey(id))
            {
                pool.Add(id, Instantiate(prefab, position, rotation, parent));
                pool[id].Id = id;
            }

            return pool[id];
        }
    }
}