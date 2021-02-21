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

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        public void AddListener(Action listener)
        {
            button.onClick.AddListener(() => listener.Invoke());
        }

        public void RemoveListener(Action listener)
        {
            button.onClick.RemoveListener(() => listener.Invoke());
        }

        public void ListenerClear()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}