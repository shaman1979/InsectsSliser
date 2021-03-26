using LightDev;
using LightDev.UI;
using Slicer.Shop;
using Slicer.Shop.Events;
using Slicer.UI.Elements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Slicer.UI.Windows
{
    public class ShopWindow : CanvasElement
    {
        public event Action NextElementSwitched;

        public event Action BackElementSwitched;

        [SerializeField]
        private ButtonElement backButton;

        [SerializeField]
        private ButtonElement nextButton;

        [SerializeField]
        private ButtonElement buyButton;

        [SerializeField]
        private Text price;

        [SerializeField]
        private Text name;

        private void Awake()
        {
            nextButton.AddListener(NextElement);
            backButton.AddListener(BackElement);
            buyButton.AddListener(BuyElement);

            ShopEvents.ItemChanged += UpdateCurrentItem;
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
            NextElementSwitched?.Invoke();
        }

        private void BackElement()
        {
            BackElementSwitched?.Invoke();
        }

        private void BuyElement()
        {
            Debug.Log($"Купить элемент");
        }

        private void UpdateCurrentItem(ShopItem item)
        {
            UpdateNameText(item.Name);
            UpdatePriceText(item.Price.ToString());
        }

        private void UpdatePriceText(string text)
        {
            price.text = text;
        }

        private void UpdateNameText(string text)
        {
            name.text = text;
        }
    }
}