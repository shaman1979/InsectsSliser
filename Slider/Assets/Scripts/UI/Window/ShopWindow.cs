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

        public event Action ElementSelected;

        [SerializeField]
        private ButtonElement backButton;

        [SerializeField]
        private ButtonElement nextButton;

        [SerializeField]
        private ButtonElement selectButton;

        [SerializeField]
        private ButtonElement backToMenu;

        [SerializeField]
        private Text level;

        [SerializeField]
        private Text name;

        private void Awake()
        {
            nextButton.AddListener(NextElement);
            backButton.AddListener(BackElement);
            selectButton.AddListener(SelectedElement);
            backToMenu.AddListener(BackToMenu);

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
                    SetSelectedButtonInterectable(false);
                    break;
                case ItemStatus.Available:
                    UpdateLevelText(string.Empty);
                    SetSelectedButtonInterectable(true);
                    break;
                case ItemStatus.Selected:
                    UpdateLevelText(string.Empty);
                    SetSelectedButtonInterectable(false);
                    break;
                default:
                    break;
            }         
        }

        private void UpdateLevelText(string text)
        {
            level.text = text;
        }

        private void SetSelectedButtonInterectable(bool isInterectable)
        {
            selectButton.SetInteractable(isInterectable);
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