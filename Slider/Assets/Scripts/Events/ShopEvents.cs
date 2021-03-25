using LightDev;

namespace Slicer.Shop.Events
{
    public static class ShopEvents
    {
        public static Event ShopShow { get; set; }
        public static Event ShopHide { get; set; }

        public static Event<ShopItem> ItemChanged { get; set; }

        static ShopEvents()
        {
            ShopShow = new Event(nameof(ShopShow));
            ShopHide = new Event(nameof(ShopHide));
            ItemChanged = new Event<ShopItem>(nameof(ItemChanged));
        }
    }
}