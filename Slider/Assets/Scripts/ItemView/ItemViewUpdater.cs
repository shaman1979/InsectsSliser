using Slicer.Items.Events;
using Slicer.Shop;
using Slicer.Shop.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Items
{
    public class ItemViewUpdater : MonoBehaviour
    {
        [SerializeField]
        private ItemView knifeView;

        [SerializeField]
        private ItemView tableView;

        private void Awake()
        {
            ItemEvents.ItemInitialize += UpdateModel;
            ShopEvents.ItemChanged += (item, type, position) => UpdateModel(item, position);
        }

        public void UpdateModel(ShopItem item, IItemPosition position)
        {
            switch (item.Type)
            {
                case ItemTypes.Khife:
                    knifeView.UpdateView(item.Model, item.Id, position);
                    break;
                case ItemTypes.Table:
                    tableView.UpdateView(item.Model, item.Id, position);
                    break;
                default:
                    break;
            }
        }
    }
}