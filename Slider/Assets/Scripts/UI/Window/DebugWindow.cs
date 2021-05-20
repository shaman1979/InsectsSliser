using LightDev.UI;
using Slicer.DebugMode;
using Slicer.DebugMode.Messages;
using Slicer.EventAgregators;
using Slicer.UI.Elements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Slicer.UI.Windows
{
    public class DebugWindow : CanvasElement
    {
        [SerializeField]
        private ButtonElement button;

        public override void Subscribe(IEventsAgregator eventAgregator)
        {
            eventAgregator.AddListener<DebugModeActiveMessage>(message => Show());
            eventAgregator.AddListener<DebugModeDeactiveMessage>(message => Hide());
        }

        protected override void OnStartShowing()
        {
            // button.AddListener(() => ProgressDataReceiver.RemoveProgress());
        }
    }
}