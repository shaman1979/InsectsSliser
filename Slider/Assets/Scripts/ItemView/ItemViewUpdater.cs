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
            ShopEvents.ItemChanged += (model, type) => UpdateModel(model, true);
            ItemEvents.ItemInitialize += (model) => UpdateModel(model, false);
        }

        public void UpdateModel(ShopItem item, bool isOutlineDraw = false)
        {
            switch (item.Type)
            {
                case ItemTypes.Khife:
                    knifeView.UpdateView(item.Model, item.Id, isOutlineDraw);
                    break;
                case ItemTypes.Table:
                    tableView.UpdateView(item.Model, item.Id, isOutlineDraw);
                    break;
                default:
                    break;
            }
        }
    }
}