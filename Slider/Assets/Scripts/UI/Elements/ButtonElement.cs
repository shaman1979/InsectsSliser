using LightDev.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Slicer.UI.Elements
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class ButtonElement : Base
    {
        private Button button;
        private Image image;

        private Button Button
        {
            get
            {
                if(button == null)
                {
                    button = GetComponent<Button>();
                    button.image = Image;
                }

                return button;
            }
        }
        
        public Image Image
        {
            get
            {
                if(image == null)
                {
                    image = GetComponent<Image>();
                }

                return image;
            }
        }

        public void SetInteractable(bool isInterectable)
        {
            button.interactable = isInterectable;
        }

        public void AddListener(Action listener)
        {
            Button.onClick.AddListener(() => listener.Invoke());
        }

        public void RemoveListener(Action listener)
        {
            Button.onClick.RemoveListener(() => listener.Invoke());
        }

        public void Click()
        {
            Button.onClick?.Invoke();
        }
        
        public void ListenerClear()
        {
            Button.onClick.RemoveAllListeners();
        }

        public Sprite GetImage()
        {
            return Image.sprite;
        }

        public void SetImage(Sprite image)
        {
            Image.sprite = image;
        }
    }
}