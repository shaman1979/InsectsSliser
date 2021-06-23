using LightDev;
using LightDev.UI;
using Slicer.Shop;
using Slicer.Shop.Events;
using Slicer.UI.Elements;
using System;
using System.Collections;
using System.Collections.Generic;
using Slicer.EventAgregators;
using Tools;
using UI.Elements;
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

        [SerializeField] private Image block;

        [SerializeField] private UIMove info;
        [SerializeField] private UIMove backToMenuMove;
        [SerializeField] private UIMove navigationContainer;
        [SerializeField] private UIMove typeNavigationContainer;

        public override void Subscribe(IEventsAgregator eventAgregator)
        {
            ShopEvents.ShopShow += Show;
            ShopEvents.ShopHide += Hide;
            
            nextButton.AddListener(NextElement);
            backButton.AddListener(BackElement);
            selectButton.AddListener(SelectedElement);
            backToMenu.AddListener(BackToMenu);

            ShopEvents.ItemChanged += (item, type, position) => UpdateCurrentItem(item, type);

            info.Awake();
            backToMenuMove.Awake();
            navigationContainer.Awake();
            typeNavigationContainer.Awake();
        }

        protected override void OnFinishShowing()
        {
            info.Activate();
            backToMenuMove.Activate();
            navigationContainer.Activate();
            typeNavigationContainer.Activate();
        }

        protected override void OnStartHiding()
        {
            info.Deactivate();
            backToMenuMove.Deactivate();
            navigationContainer.Deactivate();
            typeNavigationContainer.Deactivate();
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
                    UpdateLevelText(item.OpenItem.OpenValue.ToString());
                    selectButton.Unavaliable();
                    block.gameObject.Activate();
                    break;
                case ItemStatus.Available:
                    UpdateLevelText(string.Empty);
                    selectButton.Avaliable();
                    block.gameObject.Deactivate();
                    break;
                case ItemStatus.Selected:
                    UpdateLevelText(string.Empty);
                    selectButton.Select();
                    block.gameObject.Deactivate();
                    break;
                default:
                    break;
            }         
        }

        private void UpdateLevelText(string text)
        {
            level.text = $"Level {text}";
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