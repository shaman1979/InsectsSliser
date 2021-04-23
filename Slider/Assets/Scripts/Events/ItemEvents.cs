using LightDev;
using Slicer.Shop;
using System.Collections;
using System.Collections.Generic;

namespace Slicer.Items.Events
{
    public static class ItemEvents
    {
        public static Event<ShopItem> ItemSelected { get; set; }

        static ItemEvents()
        {
            ItemSelected = new Event<ShopItem>(nameof(ItemSelected));
        }
    }
}