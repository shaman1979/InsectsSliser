using Slicer.Shop.Select;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField]
        private OutlineDrawer outlineDrawer;

        public void OutlineDraw()
        {
            outlineDrawer.Draw();
        }

        public void OutlineClear()
        {
            outlineDrawer.Clear();
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}