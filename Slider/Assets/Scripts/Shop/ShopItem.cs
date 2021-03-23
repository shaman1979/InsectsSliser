using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Shop
{
    [CreateAssetMenu(fileName = "ShopItem", menuName = "Shop/Item")]
    public class ShopItem : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }
    }
}