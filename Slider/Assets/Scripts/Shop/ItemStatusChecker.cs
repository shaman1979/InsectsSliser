using Shop;
using Slicer.Game;
using Slicer.Items;
using Slicer.UI;

namespace Slicer.Shop
{
    public static class ItemStatusChecker
    {
        public static ItemStatus GetStatus(this ShopItem item, LevelsInitializer levelsInitializer)
        {
            if (SelectedItems.IsItemSelected(item))
            {
                return ItemStatus.Selected;
            }

            if (item.OpenItem is LevelOpenItem openItem)
            {
                return GetStatusNotSelectedItem(openItem, levelsInitializer);
            }

            if (item.OpenItem is StarsOpenItem starsOpenItem)
            {
                return GetStatusNotSelectedItem(starsOpenItem);
            }

            return ItemStatus.Unavailable;
        }

        private static ItemStatus GetStatusNotSelectedItem(LevelOpenItem item, LevelsInitializer levelsInitializer)
        {
            return item.OpenValue <= levelsInitializer.GetLevel() ? ItemStatus.Available : ItemStatus.Unavailable;
        }

        private static ItemStatus GetStatusNotSelectedItem(StarsOpenItem item)
        {
            return item.OpenValue <= StarsActivator.GetTotalStar() ? ItemStatus.Available : ItemStatus.Unavailable;
        }
    }
}