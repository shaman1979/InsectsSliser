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

        [Inject]
        private IEventsAgregator eventsAgregator;

        public override void Subscribe()
        {
            eventsAgregator.AddListener<DebugModeActiveMessage>(message => Show());
            eventsAgregator.AddListener<DebugModeDeactiveMessage>(message => Hide());
        }

        protected override void OnStartShowing()
        {
            button.AddListener(() => ProgressDataReceiver.RemoveProgress());
        }
    }
}