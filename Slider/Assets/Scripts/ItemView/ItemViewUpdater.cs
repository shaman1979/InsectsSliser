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
            ShopEvents.ItemChanged += (model, type) => UpdateModel(model);
            ItemEvents.ItemInitialize += UpdateModel;
        }

        public void UpdateModel(ShopItem item)
        {
            switch (item.Type)
            {
                case ItemTypes.Khife:
                    knifeView.UpdateView(item.Model, item.Id);
                    break;
                case ItemTypes.Table:
                    tableView.UpdateView(item.Model, item.Id);
                    break;
                default:
                    break;
            }
        }
    }
}