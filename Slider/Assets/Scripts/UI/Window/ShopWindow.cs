using LightDev;
using LightDev.UI;
using Slicer.Shop.Events;
using Slicer.UI.Elements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer.UI.Windows
{
    public class ShopWindow : CanvasElement
    {
        [SerializeField]
        private ButtonElement backButton;

        [SerializeField]
        private ButtonElement nextButton;

        [SerializeField]
        private ButtonElement buyButton;

        private void Awake()
        {
            nextButton.AddListener(NextElement);
            backButton.AddListener(BackElement);
            buyButton.AddListener(BuyElement);
        }

        public override void Subscribe()
        {
            ShopEvents.ShopShow += Show;
            ShopEvents.ShopHide += Hide;
        }

        public override void Unsubscribe()
        {
            ShopEvents.ShopShow -= Show;
            ShopEvents.ShopHide -= Hide;
        }

        private void NextElement()
        {
            Debug.Log($"Следующий элемент");
        }

        private void BackElement()
        {
            Debug.Log($"Предедущий элемент");
        }

        private void BuyElement()
        {
            Debug.Log($"Купить элемент");
        }
    }
}