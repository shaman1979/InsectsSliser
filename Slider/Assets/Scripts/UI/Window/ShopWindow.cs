using LightDev;
using LightDev.UI;
using Slicer.Shop;
using Slicer.Shop.Events;
using Slicer.UI.Elements;
using System;
using System.Collections;
using System.Collections.Generic;
using Slicer.EventAgregators;
using UI.UI.Button;
using UnityEngine;
using UnityEngine.UI;

namespace Slicer.UI.Windows
{
    public class ShopWindow : CanvasElement
    {
        public event Action NextElementSwitched;

        public event Action BackElementSwitched;

        public event Action ElementSelected;

        [SerializeField]
        private ButtonElement backButton;

        [SerializeField]
        private ButtonElement nextButton;

        [SerializeField]
        private ShopButton selectButton;

        [SerializeField]
        private ButtonElement backToMenu;

        [SerializeField]
        private Text level;

        [SerializeField]
        private Text name;

        public override void Subscribe(IEventsAgregator eventAgregator)
        {
            ShopEvents.ShopShow += Show;
            ShopEvents.ShopHide += Hide;
            
            nextButton.AddListener(NextElement);
            backButton.AddListener(BackElement);
            selectButton.AddListener(SelectedElement);
            backToMenu.AddListener(BackToMenu);

            ShopEvents.ItemChanged += (item, type, position) => UpdateCurrentItem(item, type);
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

        private void SelectedElement()
        {
            ElementSelected?.Invoke();
        }

        private void UpdateCurrentItem(ShopItem item, ItemStatus status)
        {
            UpdateNameText(item.Name);

            switch (status)
            {
                case ItemStatus.Unavailable:
                    UpdateLevelText(item.LevelOpen.ToString());
                    selectButton.Unavaliable();
                    break;
                case ItemStatus.Available:
                    UpdateLevelText(string.Empty);
                    selectButton.Avaliable();
                    break;
                case ItemStatus.Selected:
                    UpdateLevelText(string.Empty);
                    selectButton.Select();
                    break;
                default:
                    break;
            }         
        }

        private void UpdateLevelText(string text)
        {
            level.text = text;
        }

        private void BackToMenu()
        {
            ShopEvents.ShopHide?.Call();
        }

        private void UpdateNameText(string text)
        {
            name.text = text;
        }
    }
}