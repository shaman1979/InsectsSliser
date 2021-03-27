using LightDev.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Slicer.UI.Elements
{
    [RequireComponent(typeof(Button))]
    public class ButtonElement : Base
    {
        private Button button;

        private Button Button
        {
            get
            {
                if(button == null)
                {
                    button = GetComponent<Button>();
                }

                return button;
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

        public void ListenerClear()
        {
            Button.onClick.RemoveAllListeners();
        }
    }
}