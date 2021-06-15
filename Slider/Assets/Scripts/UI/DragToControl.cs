using System.Collections;
using System.Collections.Generic;
using Applications.Messages;
using UnityEngine;
using LightDev;
using LightDev.Core;
using LightDev.UI;
using DG.Tweening;
using Slicer.EventAgregators;
using Tools;
using UI.Elements;

namespace MeshSlice.UI
{
    public class DragToControl : CanvasElement
    {
        [SerializeField] private UIFade tapToSlice;

        public override void Subscribe(IEventsAgregator eventsAgregator)
        {
            Events.GameStart += Show;
            Events.PointerDown += OnPointerUp;
            eventsAgregator.AddListener<GameFinishMessage>(message => Hide());
        }

        public override void Unsubscribe()
        {
            Events.GameStart -= Show;
            Events.PointerDown -= OnPointerUp;
        }

        private void OnPointerUp()
        {
            Hide();
        }

        protected override void OnFinishShowing()
        {
            tapToSlice.StartFade();
        }
    }
}