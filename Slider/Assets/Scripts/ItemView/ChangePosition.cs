namespace Slicer.Items
{
    public partial class ItemView
    {
        private class ChangePosition
        {
            public Item CurrentItem;
            public Item Item;
            public int Id;
            public IItemPosition ItemCreator;

            public ChangePosition(Item currentItem, Item item, int id, IItemPosition itemCreator)
            {
                CurrentItem = currentItem;
                Item = item;
                Id = id;
                ItemCreator = itemCreator;
            }
        }
    }
}