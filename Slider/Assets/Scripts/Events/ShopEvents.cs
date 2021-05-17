using LightDev;
using MeshSlice;
using Slicer.Items;

namespace Slicer.Shop.Events
{
    public static class ShopEvents
    {
        public static Event ShopShow { get; set; }
        public static Event ShopHide { get; set; }

        public static Event<ShopItem, ItemStatus, IItemPosition> ItemChanged { get; set; }
        public static Event<ItemTypes> ItemTypeChanged { get; set; }

        static ShopEvents()
        {
            ShopShow = new Event(nameof(ShopShow));
            ShopHide = new Event(nameof(ShopHide));

            ItemTypeChanged = new Event<ItemTypes>(nameof(ItemTypeChanged));
            ItemChanged = new Event<ShopItem, ItemStatus, IItemPosition>(nameof(ItemChanged));
        }
    }
}