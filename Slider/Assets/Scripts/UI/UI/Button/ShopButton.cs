using Slicer.UI.Elements;
using UnityEngine;

namespace UI.UI.Button
{
    public class ShopButton : ButtonElement
    {
        [SerializeField]
        private Sprite selected;
        
        [SerializeField]
        private Sprite avalible;
        
        [SerializeField]
        private Sprite unavalible;

        public void Setup(Sprite avaliableItem, Sprite unavailableItem, Sprite selectedItem)
        {
            avalible = avaliableItem;
            unavalible = avaliableItem;
            selected = avaliableItem;
        }

        public void Avaliable()
        {
            SetImage(avalible);
        }
    }
}
