using LightDev;
using LightDev.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Shop
{
    public class ShopWindow : CanvasElement
    {
        public override void Subscribe()
        {
            Events.ShopShow += Show;
            Events.ShopHide += Hide;
        }

        public override void Unsubscribe()
        {
            Events.ShopShow -= Show;
            Events.ShopHide -= Hide;
        }
    }
}