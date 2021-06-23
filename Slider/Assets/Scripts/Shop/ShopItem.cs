using Slicer.Items;
using System.Collections;
using System.Collections.Generic;
using Shop;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Slicer.Shop
{
    [CreateAssetMenu(fileName = "ShopItem", menuName = "Shop/Item")]
    public class ShopItem : SerializedScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public ItemTypes Type { get; private set; }

        [field:SerializeField]
        public OpenItem OpenItem { get; private set; }

        [field: SerializeField]
        public Item Model { get; private set; }

        public int Id
        {
            get
            {
                return GetInstanceID();
            }
        }
    }
}