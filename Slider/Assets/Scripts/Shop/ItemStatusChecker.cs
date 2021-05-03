using MeshSlice;
using Slice.Game;
using Slicer.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            if(item.LevelOpen <= levelsInitializer.GetLevel())
            {
                return ItemStatus.Available;
            }

            return ItemStatus.Unavailable;
        }
    }
}