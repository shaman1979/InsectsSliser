using Slicer.Game;
using Slicer.Items;

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

            if (item.LevelOpen <= levelsInitializer.GetLevel())
            {
                return ItemStatus.Available;
            }

            return ItemStatus.Unavailable;
        }
    }
}